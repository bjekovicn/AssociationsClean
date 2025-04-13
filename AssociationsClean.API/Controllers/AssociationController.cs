using AssociationsClean.Application.Features.Associations.CreateAssociation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        /// Creates a new association
        /// </summary>
        /// <param name="command">The data needed to create a association</param>
        /// <returns>A created response with the new association information</returns>
        /// <response code="201">Returns the newly created association</response>
        /// <response code="400">If the association data is invalid</response>
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateAssociationCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure) return BadRequest(result.Error);

            return Created();
        }

    }
}
