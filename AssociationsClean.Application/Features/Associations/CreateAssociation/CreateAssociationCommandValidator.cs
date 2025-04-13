

using FluentValidation;

namespace AssociationsClean.Application.Features.Associations.CreateAssociation
{
    internal class CreateAssociationCommandValidator : AbstractValidator<CreateAssociationCommand>
    {
        public CreateAssociationCommandValidator()
        {
            RuleFor(c => c.AssociationName).NotEmpty().WithMessage("Association name cannot be empty.");
            RuleFor(c => c.AssociationName).MinimumLength(3).WithMessage("Association name must be at least 3 characters long.");
            RuleFor(c => c.Description).NotEmpty().When(c => c.Description != null).WithMessage("Association description cannot be empty.");
        }
    }
}
