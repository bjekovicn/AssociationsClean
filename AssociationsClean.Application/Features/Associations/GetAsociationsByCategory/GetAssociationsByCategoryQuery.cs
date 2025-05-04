
using AssociationsClean.Application.Shared.Abstractions.Messaging;
using AssociationsClean.Domain.Features.Associations;

namespace AssociationsClean.Application.Features.Associations.GetAsociationsByCategory
{

    public sealed record GetAssociationsByCategoryQuery(int CategoryId) : IQuery<IReadOnlyList<Association>>;
}
