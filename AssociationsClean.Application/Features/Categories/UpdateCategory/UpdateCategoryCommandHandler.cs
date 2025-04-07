

using AssociationsClean.Domain.Bookings;
using AssociationsClean.Domain.Shared.Abstractions;
using AssociationsClean.Application.Shared.Abstractions.Messaging;


namespace AssociationsClean.Application.Features.Categories.UpdateCategory
{
    internal sealed class UpdateCategoryCommandHandler:ICommandHandler<UpdateCategoryCommand>
      
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryQueryRepository _queryRepository;
        private readonly ICategoryCommandRepository _commandRepository;

        public UpdateCategoryCommandHandler(
            IUnitOfWork unitOfWork,
            ICategoryQueryRepository queryRepository,
            ICategoryCommandRepository commandRepository)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _queryRepository.GetByIdAsync(request.Id);

            if (category is null)
            {
                return Result.Failure(CategoryErrors.NotFound);
            }

            category.ChangeName(request.Name);
            category.ChangePhoto(request.Photo);

            await _commandRepository.UpdateAsync(category);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
