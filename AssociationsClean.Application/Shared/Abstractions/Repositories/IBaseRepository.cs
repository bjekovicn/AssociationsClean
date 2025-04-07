

namespace AssociationsClean.Application.Shared.Abstractions.Repositories
{
    public interface IBaseRepository<T> : IBaseQueryRepository<T>, IBaseCommandRepository<T> where T : class
    {
    }
}
