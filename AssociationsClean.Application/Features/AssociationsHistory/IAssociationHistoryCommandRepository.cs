
using AssociationsClean.Application.Shared.Abstractions.Repositories;

namespace AssociationsClean.Application.Features.AssociationsHistory
{
    public interface IAssociationHistoryCommandRepository:IBaseCommandRepository<AssociationHistory>
    {
        Task AddManyAsync(Guid userUuid, IEnumerable<int> associationIds);
        Task DeleteManyAsync(Guid userUuid, IEnumerable<int> associationIds);
    }
}
