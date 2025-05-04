
using FluentValidation;

namespace AssociationsClean.Application.Features.Associations.GetRandomAssociationsByCategoryIds
{
    public class GetRandomAssociationsByCategoriesQueryValidator
         : AbstractValidator<GetRandomAssociationsByCategoriesQuery>
    {
        public GetRandomAssociationsByCategoriesQueryValidator()
        {
            RuleFor(q => q.count)
                .GreaterThan(0)
                .WithMessage("count must be greater than 0.")
                .LessThan(100)
                .WithMessage("count must be less than 100.");


            RuleForEach(q => q.categoryIds)
                .GreaterThan(0)
                .WithMessage("categoryIds must be positive integers.");
        }
    }
}
