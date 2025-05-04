using AssociationsClean.Domain.Shared.Abstractions;
using AssociationsClean.Application.Shared.Abstractions.Messaging;
using AssociationsClean.Application.Shared.Abstractions.Storage;
using AssociationsClean.Domain.Bookings;

namespace AssociationsClean.Application.Features.Categories.UpdateCategory
{
    internal sealed class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryQueryRepository _queryRepository;
        private readonly ICategoryCommandRepository _commandRepository;
        private readonly IStorageService _storageService;

        public UpdateCategoryCommandHandler(
            IUnitOfWork unitOfWork,
            ICategoryQueryRepository queryRepository,
            ICategoryCommandRepository commandRepository,
            IStorageService storageService)
        {
            _unitOfWork = unitOfWork;
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _storageService = storageService;
        }

        public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _queryRepository.GetByIdAsync(request.Id);

            if (category is null)
            {
                return Result.Failure(CategoryErrors.NotFound);
            }

            // Update name only if it's not null/empty
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                category.ChangeName(request.Name);
            }

            if (request.Photo is not null)
            {
                var file = request.Photo;
                var fileName = $"{Guid.NewGuid()}_{file.FileName}";

                using var stream = file.OpenReadStream();
                var newPhotoUrl = await _storageService.UploadFileAsync(stream, fileName, file.ContentType);

                category.ChangePhoto(newPhotoUrl); 
            }

            await _commandRepository.UpdateAsync(category);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
