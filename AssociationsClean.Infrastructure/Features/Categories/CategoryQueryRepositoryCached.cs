using AssociationsClean.Application.Features.Categories;
using AssociationsClean.Domain.Features.Categories;
using Microsoft.Extensions.Caching.Memory;


namespace AssociationsClean.Infrastructure.Features.Categories
{
    internal sealed class CategoryQueryRepositoryCached : ICategoryQueryRepository
    {
        private readonly ICategoryQueryRepository _decorated;
        private readonly IMemoryCache _memoryCache;
        private const string AllCategoriesCacheKey = "all_categories";
        private const string CategoryByIdCacheKeyPrefix = "category_";

        public CategoryQueryRepositoryCached(ICategoryQueryRepository decorated, IMemoryCache memoryCache)
        {
            _decorated = decorated;
            _memoryCache = memoryCache;
        }

        public Task<bool> ExistsAsync(int id)
        {
            return _decorated.ExistsAsync(id);
        }

        public async Task<IReadOnlyList<Category>> GetAllAsync()
        {
            if (_memoryCache.TryGetValue(AllCategoriesCacheKey, out IReadOnlyList<Category>? cachedCategories) && cachedCategories != null)
            {
                return cachedCategories;
            }

            var categories = await _decorated.GetAllAsync();

            _memoryCache.Set(AllCategoriesCacheKey, categories);

            return categories;
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            string cacheKey = $"{CategoryByIdCacheKeyPrefix}{id}";

            if (_memoryCache.TryGetValue(cacheKey, out Category? cachedCategory))
            {
                return cachedCategory;
            }

            var category = await _decorated.GetByIdAsync(id);

            if (category != null)
            {
                _memoryCache.Set(cacheKey, category);
            }

            return category;
        }
    }
}
