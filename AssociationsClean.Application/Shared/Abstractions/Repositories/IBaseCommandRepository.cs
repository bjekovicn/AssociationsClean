

namespace AssociationsClean.Application.Shared.Abstractions.Repositories
{
    public interface IBaseCommandRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
