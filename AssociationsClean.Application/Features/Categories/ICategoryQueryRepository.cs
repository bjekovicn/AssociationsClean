
using AssociationsClean.Domain.Features.Categories;
using AssociationsClean.Application.Shared.Abstractions.Repositories;

namespace AssociationsClean.Application.Features.Categories
{
    public interface ICategoryQueryRepository:IBaseQueryRepository<Category>
    {
    }
}
