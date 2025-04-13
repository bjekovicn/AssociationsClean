using AssociationsClean.Application.Shared.Abstractions.Messaging;

namespace AssociationsClean.Application.Features.Associations.DeleteAssociation
{
    public sealed record DeleteAssociationCommand(int Id) : ICommand;
}