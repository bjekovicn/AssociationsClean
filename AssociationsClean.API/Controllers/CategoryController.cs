using MediatR;
using Microsoft.AspNetCore.Mvc;

using Associations.Application.Features.Categories.GetCategories;
using AssociationsClean.Application.Features.Categories.GetAllCategories;
using AssociationsClean.Application.Features.Categories.CreateCategory;

namespace AssociationsClean.API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllCategories()
        {
            var query = new GetAllCategoriesQuery();
            var result = await _mediator.Send(query);

            return Ok(result.Value); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var query = new GetCategoryByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result.IsFailure) return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure) return BadRequest(result.Error);

            return CreatedAtAction(nameof(CreateCategory), new { name = command.categoryName, photo = command.categoryPhoto }, command);
        }
    }
}
