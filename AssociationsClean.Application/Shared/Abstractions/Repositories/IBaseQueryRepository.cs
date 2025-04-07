
namespace AssociationsClean.Application.Shared.Abstractions.Repositories
{
    public interface IBaseQueryRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
    }
}
