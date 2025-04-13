

using AssociationsClean.Domain.Shared.Abstractions;
using AssociationsClean.Domain.Features.Associations;
using AssociationsClean.Application.Shared.Abstractions.Messaging;
using AssociationsClean.Application.Features.Categories;

namespace AssociationsClean.Application.Features.Associations.CreateAssociation
{
    internal sealed class CreateAssociationCommandHandler : ICommandHandler<CreateAssociationCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryQueryRepository _categoryQueryRepository;
        private readonly IAssociationQueryRepository _associationQueryRepository;
        private readonly IAssociationCommandRepository _associationCommandRepository;

        public CreateAssociationCommandHandler(
            IUnitOfWork unitOfWork, 
            ICategoryQueryRepository categoryCommandRepository,
            IAssociationQueryRepository associationQueryRepository,
            IAssociationCommandRepository associationCommandRepository
            )
        {
            _unitOfWork = unitOfWork;
            _categoryQueryRepository = categoryCommandRepository;
            _associationQueryRepository = associationQueryRepository;
            _associationCommandRepository = associationCommandRepository;
        }

        public async Task<Result> Handle(CreateAssociationCommand request, CancellationToken cancellationToken)
        {
            var categoryExists = await _categoryQueryRepository.ExistsAsync(request.CategoryId);
            if (!categoryExists)
            {
                return Result.Failure(AssociationErrors.CategoryNotFound);
            }

            var associationExists = await _associationQueryRepository.ExistsAsync(request.CategoryId, request.AssociationName);
            if (associationExists)
            {
                return Result.Failure(AssociationErrors.DuplicateNameInCategory);
            }

            var association = new Association(request.AssociationName, request.Description, request.CategoryId);
            await _associationCommandRepository.AddAsync(association);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
