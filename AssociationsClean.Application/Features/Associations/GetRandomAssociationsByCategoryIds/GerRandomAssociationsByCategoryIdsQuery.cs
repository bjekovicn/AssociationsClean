
using AssociationsClean.Application.Shared.Abstractions.Messaging;
using AssociationsClean.Domain.Features.Associations;

namespace AssociationsClean.Application.Features.Associations.GetRandomAssociationsByCategoryIds
{
    public sealed record GerRandomAssociationsByCategoryIdsQuery(int count, List<int> categoryIds):IQuery<IReadOnlyList<AssociationWithCategory>>;
}
