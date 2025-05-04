
using FluentValidation;

namespace AssociationsClean.Application.Features.Associations.GetUnansweredAssociationsByCategoryIds
{
    public class GetUnansweredAssociationsByCategoriesValidator
      : AbstractValidator<GetUnansweredAssociationsByCategoriesQuery>
    {
        public GetUnansweredAssociationsByCategoriesValidator()
        {
            RuleFor(q => q.UserUuid)
                .NotEmpty()
                .WithMessage("userUuid is required.");

            RuleForEach(q => q.CategoryIds)
                .GreaterThan(0)
                .WithMessage("categoryIds must be positive integers.");
        }
    }
}
