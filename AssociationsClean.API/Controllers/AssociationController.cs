using MediatR;
using Microsoft.AspNetCore.Mvc;

using Associations.Application.Features.Associations.GetAssociations;
using AssociationsClean.Application.Features.Associations.GetAllAssociations;
using AssociationsClean.Application.Features.Associations.CreateAssociation;
using AssociationsClean.Application.Features.Associations.DeleteAssociation;
using AssociationsClean.Application.Features.Associations.UpdateAssociation;
using AssociationsClean.Application.Features.Associations.GetRandomAssociationsByCategoryIds;
using AssociationsClean.Application.Features.Associations.GetUnansweredAssociationsByCategoryIds;
using AssociationsClean.Application.Features.AssociationsHistory.AddManyAssociationHistories;
using AssociationsClean.Application.Features.AssociationsHistory.GetAnsweredAssociations;

namespace AssociationsClean.API.Controllers
{
    /// <summary>
    /// API controller for managing associations.
    /// </summary>
    [ApiController]
    [Route("api/associations")]
    public class AssociationController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssociationController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator instance used for handling requests.</param>
        public AssociationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets all associations
        /// </summary>
        /// <returns>A collection of all associations</returns>
        /// <response code="200">Returns the list of associations</response>
        [HttpGet()]
        public async Task<IActionResult> GetAllAssociations()
        {
            var query = new GetAllAssociationsQuery();
            var result = await _mediator.Send(query);

            return Ok(result.Value);
        }



        /// <summary>
        /// Gets a specific association by ID
        /// </summary>
        /// <param name="id">The ID of the association to retrieve</param>
        /// <returns>The requested association</returns>
        /// <response code="200">Returns the requested association</response>
        /// <response code="404">If the association is not found</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssociationById(int id)
        {
            var query = new GetAssociationByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result.IsFailure) return NotFound(result.Error);

            return Ok(result.Value);
        }

        /// <summary>
        /// Creates a new association
        /// </summary>
        /// <param name="command">The data needed to create an association</param>
        /// <returns>A created response with the new association information</returns>
        /// <response code="201">Returns the newly created association</response>
        /// <response code="400">If the association data is invalid</response>
        [HttpPost]
        public async Task<IActionResult> CreateAssociation(CreateAssociationCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure) return BadRequest(result.Error);

            return Created();
        }

        /// <summary>
        /// Updates an existing association
        /// </summary>
        /// <param name="id">The ID of the association to update</param>
        /// <param name="command">The data needed to update the association</param>
        /// <returns>A success response if the update was successful</returns>
        /// <response code="204">If the association was successfully updated</response>
        /// <response code="400">If the association data is invalid or IDs don't match</response>
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAssociation(int id, UpdateAssociationCommand command)
        {
            if (id != command.Id) return BadRequest("Association ID mismatch.");

            var result = await _mediator.Send(command);

            if (result.IsFailure) return BadRequest(result.Error);

            return NoContent();
        }

        /// <summary>
        /// Deletes a specific association
        /// </summary>
        /// <param name="id">The ID of the association to delete</param>
        /// <returns>A no content response if the deletion was successful</returns>
        /// <response code="204">If the association was successfully deleted</response>
        /// <response code="404">If the association is not found</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssociation(int id)
        {
            var command = new DeleteAssociationCommand(id);
            var result = await _mediator.Send(command);

            if (result.IsFailure) return NotFound(result.Error);

            return NoContent();
        }

        /// <summary>
        /// Gets a random list of associations filtered by category IDs.
        /// </summary>
        /// <param name="count">The number of associations to retrieve.</param>
        /// <param name="categoryId">The list of category IDs to filter associations by.</param>
        /// <param name="userUuid">The unique identifier of the user to filter the associations by.</param>
        /// <returns>A list of randomly selected associations.</returns>
        /// <response code="200">Returns the random associations.</response>
        /// <response code="400">If the request parameters are invalid.</response>
        [HttpGet("random")]
        public async Task<IActionResult> GetRandomAssociations([FromQuery] int count, [FromQuery] List<int> categoryId, [FromHeader] Guid userUuid)
        {
            var query = new GetUnansweredAssociationsByCategoriesQuery(userUuid,count, categoryId);

 
            var validator = new GetUnansweredAssociationsByCategoriesValidator();
            var validationResult = await validator.ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var result = await _mediator.Send(query);

            return Ok(result.Value);
        }

        /// <summary>
        /// Gets a list of answered associations for a specific user.
        /// </summary>
        /// <param name="userUuid">The unique identifier of the user</param>
        /// <returns>A list of answered associations including the association name and whether it was answered correctly.</returns>
        /// <response code="200">Returns the answered associations</response>
        /// <response code="404">If the user has not answered any associations</response>
        [HttpGet("answered")]
        public async Task<IActionResult> GetAnsweredAssociations([FromHeader] Guid userUuid)
        {
            var query = new GetAnsweredAssociationsQuery(userUuid);
            var result = await _mediator.Send(query);

            if (result.IsFailure) return NotFound(result.Error);

            return Ok(result.Value);
        }

        /// <summary>
        /// Adds multiple association history records for a user.
        /// </summary>
        /// <param name="command">The command containing user UUID and association-answer pairs.</param>
        /// <returns>Returns HTTP 201 if records are added successfully, or 400 for invalid input.</returns>
        /// <response code="201">History entries successfully created.</response>
        /// <response code="400">Invalid input or command failed.</response>
        [HttpPost("answered/bulk")]
        public async Task<IActionResult> AddManyHistoriesAsync([FromBody] AddManyAssociationHistoriesCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure) return BadRequest(result.Error);

            return Created();
        }



    }
}