
using FluentValidation;

namespace AssociationsClean.Application.Features.Associations.GetRandomAssociationsByCategoryIds
{
    public class GetRandomAssociationsByCategoryIdsQueryValidator
         : AbstractValidator<GerRandomAssociationsByCategoryIdsQuery>
    {
        public GetRandomAssociationsByCategoryIdsQueryValidator()
        {
            RuleFor(q => q.count)
                .GreaterThan(0)
                .WithMessage("Count must be greater than 0.")
                .LessThan(100)
                .WithMessage("Count must be less than 100.");

            RuleForEach(q => q.categoryIds)
                .GreaterThan(0)
                .WithMessage("Category IDs must be positive integers.");
        }
    }
}
