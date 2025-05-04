
using AssociationsClean.Application.Shared.Abstractions.Messaging;
using AssociationsClean.Application.Features.Associations.GetRandomAssociationsByCategoryIds;

namespace AssociationsClean.Application.Features.Associations.GetUnansweredAssociationsByCategoryIds
{
    public sealed record GetUnansweredAssociationsByCategoriesQuery(
        Guid UserUuid,
        List<int> CategoryIds
    ) : IQuery<IReadOnlyList<AssociationWithCategory>>;
}
