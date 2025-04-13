

using AssociationsClean.Domain.Shared.Abstractions;
using AssociationsClean.Domain.Features.Categories;
using AssociationsClean.Application.Shared.Abstractions.Messaging;
using AssociationsClean.Application.Shared.Abstractions.Storage;

namespace AssociationsClean.Application.Features.Categories.CreateCategory
{
    internal sealed class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryCommandRepository _categoryRepository;
        private readonly IStorageService _storageService;

        public CreateCategoryCommandHandler(
            IUnitOfWork unitOfWork,
            ICategoryCommandRepository categoryRepository,
            IStorageService storageService)
        {
            _unitOfWork = unitOfWork;   
            _categoryRepository = categoryRepository;
            _storageService = storageService;
        }

        public async Task<Result> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            string? photoUrl = null;

            if (request.categoryPhoto != null)
            {
                var file = request.categoryPhoto;
                var fileName = $"{Guid.NewGuid()}_{file.FileName}";

                using var stream = file.OpenReadStream();
                photoUrl = await _storageService.UploadFileAsync(stream, fileName, file.ContentType);
            }

            var category = new Category(request.categoryName, photoUrl);
            await _categoryRepository.AddAsync(category);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
