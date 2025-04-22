


namespace AssociationsClean.Application.Features.AssociationsHistory
{
    public interface IAssociationHistoryQueryRepository
    {
        Task<IReadOnlyList<int>> GetAnsweredAssociationIdsAsync(Guid userUuid);


    }
}
