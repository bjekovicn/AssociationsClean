


using AssociationsClean.Application.Features.Associations.GetRandomAssociationsByCategoryIds;
using AssociationsClean.Application.Features.AssociationsHistory.GetAnsweredAssociations;

namespace AssociationsClean.Application.Features.AssociationsHistory
{
    public interface IAssociationHistoryQueryRepository
    {
        Task<IReadOnlyList<int>> GetAnsweredAssociationIdsAsync(Guid userUuid);

        Task<IReadOnlyList<AssociationWithCategory>> GetOldestAnsweredAssociationsByCategoryIdsAsync(
        Guid userUuid,
        IEnumerable<int> categoryIds,
        int count);

        Task<List<AnsweredAssociation>> GetAnsweredAssociationsWithDetailsAsync(Guid userUuid);

    }
}
