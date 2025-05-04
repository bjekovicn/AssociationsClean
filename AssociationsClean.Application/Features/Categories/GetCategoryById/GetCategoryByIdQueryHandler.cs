using AssociationsClean.Domain.Bookings;
using AssociationsClean.Domain.Shared.Abstractions;
using AssociationsClean.Domain.Features.Categories;
using AssociationsClean.Application.Features.Categories;
using AssociationsClean.Application.Shared.Abstractions.Messaging;

namespace Associations.Application.Features.Categories.GetCategories
{

    internal sealed class GetCategoryByIdQueryHandler:IQueryHandler<GetCategoryByIdQuery,Category>
    {
        private readonly ICategoryQueryRepository categoryQueryRepository;

        public GetCategoryByIdQueryHandler(ICategoryQueryRepository categoryQueryRepository)
        {
            this.categoryQueryRepository = categoryQueryRepository;
        }

        public async Task<Result<Category>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await categoryQueryRepository.GetByIdAsync(request.Id);

            if (category == null)
            {
                return Result.Failure<Category>(CategoryErrors.NotFound);
            }

            return Result.Success(category);
        }
    }
}
