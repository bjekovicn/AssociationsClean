
using AssociationsClean.Domain.Features.Categories;
using AssociationsClean.Infrastructure.Persistence;
using AssociationsClean.Application.Features.Categories;

namespace AssociationsClean.Infrastructure.Features.Categories
{
    internal sealed class CategoryCommandRepository : ICategoryCommandRepository
    {
       private readonly AppDbContext dbContext;

        public CategoryCommandRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(Category entity)
        {
            await dbContext.Set<Category>().AddAsync(entity);
        }

        public Task UpdateAsync(Category entity)
        {
            dbContext.Set<Category>().Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Category entity)
        {
            dbContext.Set<Category>().Remove(entity);
            return Task.CompletedTask;
        }
    }
}
