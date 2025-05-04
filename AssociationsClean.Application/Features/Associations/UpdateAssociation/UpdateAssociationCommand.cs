using AssociationsClean.Application.Shared.Abstractions.Messaging;

namespace AssociationsClean.Application.Features.Associations.UpdateAssociation
{
    public sealed record UpdateAssociationCommand(int Id, string? Name, string? Description, int? CategoryId) : ICommand;
}