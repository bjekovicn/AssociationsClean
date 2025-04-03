
using AssociationsClean.Domain.Features.Categories;
using AssociationsClean.Infrastructure.Persistence;

namespace AssociationsClean.Infrastructure.Features.Categories
{
    internal sealed class CategoryRepository : ICategoryRepository
    {
       private readonly AppDbContext dbContext;

        public CategoryRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    
        public void Create(Category category)
        {
            dbContext.Add(category);
        }
    }
}
