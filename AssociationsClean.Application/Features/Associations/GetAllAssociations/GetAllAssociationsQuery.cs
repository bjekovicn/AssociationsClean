using AssociationsClean.Application.Shared.Abstractions.Messaging;
using AssociationsClean.Domain.Features.Associations;

namespace AssociationsClean.Application.Features.Associations.GetAllAssociations
{
    public sealed record GetAllAssociationsQuery : IQuery<IReadOnlyList<Association>>;
}