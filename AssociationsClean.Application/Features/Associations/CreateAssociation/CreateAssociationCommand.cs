

using AssociationsClean.Application.Shared.Abstractions.Messaging;

namespace AssociationsClean.Application.Features.Associations.CreateAssociation
{
    public sealed record CreateAssociationCommand(string AssociationName, string? Description, int CategoryId):ICommand;
}
