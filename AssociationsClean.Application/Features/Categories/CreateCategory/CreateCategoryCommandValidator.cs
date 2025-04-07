
using FluentValidation;

namespace AssociationsClean.Application.Features.Categories.CreateCategory
{
    internal class CreateCategoryCommandValidator:AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(c => c.categoryName).NotEmpty();

        }
    }
}
