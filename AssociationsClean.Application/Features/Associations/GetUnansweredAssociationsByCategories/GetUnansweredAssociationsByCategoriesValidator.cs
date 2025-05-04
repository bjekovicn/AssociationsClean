
using FluentValidation;

namespace AssociationsClean.Application.Features.Associations.GetUnansweredAssociationsByCategoryIds
{
    public class GetUnansweredAssociationsByCategoriesValidator
      : AbstractValidator<GetUnansweredAssociationsByCategoriesQuery>
    {
        public GetUnansweredAssociationsByCategoriesValidator()
        {
            RuleFor(q => q.Count)
                .GreaterThan(0)
                .WithMessage("count must be greater than 0.")
                .LessThanOrEqualTo(100)
                .WithMessage("count must be less than or equal to 100.");

            RuleFor(q => q.UserUuid)
                .NotEmpty()
                .WithMessage("userUuid is required.");

            RuleForEach(q => q.CategoryIds)
                .GreaterThan(0)
                .WithMessage("categoryIds must be positive integers.");
        }
    }
}
