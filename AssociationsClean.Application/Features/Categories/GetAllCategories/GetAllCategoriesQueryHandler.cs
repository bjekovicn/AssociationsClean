

using AssociationsClean.Domain.Features.Categories;
using AssociationsClean.Domain.Shared.Abstractions;
using AssociationsClean.Application.Shared.Abstractions.Messaging;

namespace AssociationsClean.Application.Features.Categories.GetAllCategories
{
    internal sealed class GetAllCategoriesQueryHandler : IQueryHandler<GetAllCategoriesQuery, IReadOnlyList<Category>>
    {
        private readonly ICategoryQueryRepository categoryQueryRepository;

        public GetAllCategoriesQueryHandler(ICategoryQueryRepository categoryQueryRepository)
        {
            this.categoryQueryRepository = categoryQueryRepository;
        }

        public async Task<Result<IReadOnlyList<Category>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await categoryQueryRepository.GetAllAsync();
            return Result.Success<IReadOnlyList<Category>>(categories);
        }
    }
}
