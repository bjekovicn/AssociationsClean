using AssociationsClean.Application.Shared.Abstractions.Repositories;
using AssociationsClean.Domain.Features.Associations;

namespace AssociationsClean.Application.Features.Associations
{
    public interface IAssociationQueryRepository: IBaseQueryRepository<Association>
    {
        Task<bool> ExistsAsync(int categoryId,String associationName);
        Task<IReadOnlyList<Association>> GetByCategoryIdAsync(int categoryId);
        Task<IReadOnlyList<Association>> GetRandomByCategoryIdsAsync(int count, List<int> categoryIds);


    }
}
