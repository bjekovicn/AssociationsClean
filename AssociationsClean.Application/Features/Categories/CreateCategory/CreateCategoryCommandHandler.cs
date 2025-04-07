

using AssociationsClean.Domain.Shared.Abstractions;
using AssociationsClean.Domain.Features.Categories;
using AssociationsClean.Application.Shared.Abstractions.Messaging;

namespace AssociationsClean.Application.Features.Categories.CreateCategory
{
    internal sealed class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryCommandRepository _categoryRepository;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, ICategoryCommandRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;   
            _categoryRepository = categoryRepository;
        }

        public async Task<Result> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category(request.categoryName, request.categoryPhoto);
            await _categoryRepository.AddAsync(category);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
