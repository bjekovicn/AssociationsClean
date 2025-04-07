

using AssociationsClean.Domain.Bookings;
using AssociationsClean.Domain.Shared.Abstractions;
using AssociationsClean.Application.Shared.Abstractions.Messaging;

namespace AssociationsClean.Application.Features.Categories.DeleteCategory
{
    internal sealed class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryQueryRepository _queryRepository;
        private readonly ICategoryCommandRepository _commandRepository;

        public DeleteCategoryCommandHandler(
            IUnitOfWork unitOfWork,
            ICategoryQueryRepository queryRepository,
            ICategoryCommandRepository commandRepository)
        {
            _unitOfWork = unitOfWork;
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
        }

        public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _queryRepository.GetByIdAsync(request.Id);

            if (category is null)
            {
                return Result.Failure(CategoryErrors.NotFound);
            }

            await _commandRepository.DeleteAsync(category);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
