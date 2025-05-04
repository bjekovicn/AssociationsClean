using AssociationsClean.Application.Features.Associations.GetRandomAssociationsByCategoryIds;
using AssociationsClean.Application.Shared.Abstractions.Repositories;
using AssociationsClean.Domain.Features.Associations;

namespace AssociationsClean.Application.Features.Associations
{
    public interface IAssociationQueryRepository: IBaseQueryRepository<Association>
    {
        Task<bool> ExistsAsync(int categoryId,String associationName);
        Task<IReadOnlyList<Association>> GetByCategoryIdAsync(int categoryId);
        Task<IReadOnlyList<AssociationWithCategory>> GetRandomByCategoryIdsAsync(int count, List<int> categoryIds);
        Task<IReadOnlyList<AssociationWithCategory>> GetRandomUnansweredByCategoryIdsAsync(int count, List<int>? categoryIds, IReadOnlyList<int> answeredIds);



    }
}
