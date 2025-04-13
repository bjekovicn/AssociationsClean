using AssociationsClean.Application.Shared.Abstractions.Messaging;
using AssociationsClean.Domain.Features.Associations;

namespace Associations.Application.Features.Associations.GetAssociations
{
    public sealed record GetAssociationByIdQuery(int Id) : IQuery<Association>;
}