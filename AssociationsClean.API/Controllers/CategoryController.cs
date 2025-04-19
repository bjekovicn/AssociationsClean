using MediatR;
using Microsoft.AspNetCore.Mvc;

using Associations.Application.Features.Categories.GetCategories;
using AssociationsClean.Application.Features.Categories.GetAllCategories;
using AssociationsClean.Application.Features.Categories.CreateCategory;
using AssociationsClean.Application.Features.Categories.DeleteCategory;
using AssociationsClean.Application.Features.Categories.UpdateCategory;
using AssociationsClean.Application.Features.Associations.GetAsociationsByCategory;

namespace AssociationsClean.API.Controllers
{
    /// <summary>
    /// API controller for managing categories.
    /// </summary>
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator instance used for handling requests.</param>
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <returns>A collection of all categories</returns>
        /// <response code="200">Returns the list of categories</response>
        [HttpGet()]
        public async Task<IActionResult> GetAllCategories()
        {
            var query = new GetAllCategoriesQuery();
            var result = await _mediator.Send(query);

            return Ok(result.Value); 
        }

        /// <summary>
        /// Gets a specific category by ID
        /// </summary>
        /// <param name="id">The ID of the category to retrieve</param>
        /// <returns>The requested category</returns>
        /// <response code="200">Returns the requested category</response>
        /// <response code="404">If the category is not found</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var query = new GetCategoryByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result.IsFailure) return NotFound(result.Error);

            return Ok(result.Value);
        }

        /// <summary>
        /// Creates a new category
        /// </summary>
        /// <param name="command">The data needed to create a category</param>
        /// <returns>A created response with the new category information</returns>
        /// <response code="201">Returns the newly created category</response>
        /// <response code="400">If the category data is invalid</response>
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateCategory([FromForm] CreateCategoryCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure) return BadRequest(result.Error);

            return Created();
        }


        /// <summary>
        /// Updates an existing category
        /// </summary>
        /// <param name="id">The ID of the category to update</param>
        /// <param name="command">The data needed to update the category</param>
        /// <returns>A success response if the update was successful</returns>
        /// <response code="204">If the category was successfully updated</response>
        /// <response code="400">If the category data is invalid or IDs don't match</response>
        [HttpPatch("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> PatchCategory(int id, [FromForm] UpdateCategoryCommand command)
        {
            if (id != command.Id) return BadRequest("Category ID mismatch.");

            var result = await _mediator.Send(command);

            if (result.IsFailure) return BadRequest(result.Error);

            return NoContent();
        }


        /// <summary>
        /// Deletes a specific category
        /// </summary>
        /// <param name="id">The ID of the category to delete</param>
        /// <returns>A no content response if the deletion was successful</returns>
        /// <response code="204">If the category was successfully deleted</response>
        /// <response code="404">If the category is not found</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var command = new DeleteCategoryCommand(id);
            var result = await _mediator.Send(command);

            if (result.IsFailure) return NotFound(result.Error);

            return NoContent(); 
        }

        /// <summary>
        /// Gets all associations by a specific category ID.
        /// </summary>
        /// <param name="id">The ID of the category for which to retrieve associations.</param>
        /// <returns>A list of associations.</returns>
        /// <response code="200">Returns the list of associations</response>
        /// <response code="400">If the category ID is invalid or associations cannot be retrieved</response>

        [HttpGet("{id}/associations")]
        public async Task<IActionResult> GetByCategoryId(int id)
        {
            var result = await _mediator.Send(new GetAssociationsByCategoryQuery(id));
            return Ok(result.Value);
        }
    }
}
