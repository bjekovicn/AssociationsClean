

using AssociationsClean.Domain.Shared.Abstractions;
using AssociationsClean.Domain.Features.Associations;
using AssociationsClean.Application.Shared.Abstractions.Messaging;
using AssociationsClean.Application.Features.Categories;

namespace AssociationsClean.Application.Features.Associations.CreateAssociation
{
    internal sealed class CreateAssociationCommandHandler : ICommandHandler<CreateAssociationCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAssociationCommandRepository _associationRepository;
        private readonly ICategoryQueryRepository _categoryRepository;

        public CreateAssociationCommandHandler(
            IUnitOfWork unitOfWork, 
            IAssociationCommandRepository associationRepository,
            ICategoryQueryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _associationRepository = associationRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Result> Handle(CreateAssociationCommand request, CancellationToken cancellationToken)
        {
            var categoryExists = await _categoryRepository.ExistsAsync(request.CategoryId);
            if (!categoryExists)
            {
                return Result.Failure(AssociationErrors.CategoryNotFound);
            }

            var association = new Association(request.AssociationName, request.Description, request.CategoryId);
            await _associationRepository.AddAsync(association);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
