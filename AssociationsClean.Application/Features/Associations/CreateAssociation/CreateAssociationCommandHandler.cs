

using AssociationsClean.Domain.Shared.Abstractions;
using AssociationsClean.Domain.Features.Associations;
using AssociationsClean.Application.Shared.Abstractions.Messaging;

namespace AssociationsClean.Application.Features.Associations.CreateAssociation
{
    internal sealed class CreateAssociationCommandHandler : ICommandHandler<CreateAssociationCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAssociationCommandRepository _associationRepository;

        public CreateAssociationCommandHandler(
            IUnitOfWork unitOfWork, 
            IAssociationCommandRepository associationRepository)
        {
            _unitOfWork = unitOfWork;
            _associationRepository = associationRepository;
        }

        public async Task<Result> Handle(CreateAssociationCommand request, CancellationToken cancellationToken)
        {
            var association = new Association(request.AssociationName, request.Description, request.CategoryId);
            await _associationRepository.AddAsync(association);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
