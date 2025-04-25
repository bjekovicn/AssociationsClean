using FluentValidation;

namespace AssociationsClean.Application.Features.AssociationsHistory.AddManyAssociationHistories
{
    public sealed class AddManyAssociationHistoriesCommandValidator
        : AbstractValidator<AddManyAssociationHistoriesCommand>
    {
        public AddManyAssociationHistoriesCommandValidator()
        {
            RuleFor(x => x.UserUuid)
                .NotEmpty().WithMessage("User UUID is required.");

            RuleFor(x => x.Associations)
                .NotEmpty().WithMessage("At least one association must be provided.");

            RuleForEach(x => x.Associations)
                .Must(kvp => kvp.Key > 0)
                .WithMessage("Association ID must be greater than zero.");
        }
    }
}
