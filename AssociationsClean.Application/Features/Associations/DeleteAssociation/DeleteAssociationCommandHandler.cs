using AssociationsClean.Domain.Shared.Abstractions;
using AssociationsClean.Application.Shared.Abstractions.Messaging;
using AssociationsClean.Domain.Features.Associations;

namespace AssociationsClean.Application.Features.Associations.DeleteAssociation
{
    internal sealed class DeleteAssociationCommandHandler : ICommandHandler<DeleteAssociationCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAssociationQueryRepository _queryRepository;
        private readonly IAssociationCommandRepository _commandRepository;

        public DeleteAssociationCommandHandler(
            IUnitOfWork unitOfWork,
            IAssociationQueryRepository queryRepository,
            IAssociationCommandRepository commandRepository)
        {
            _unitOfWork = unitOfWork;
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
        }

        public async Task<Result> Handle(DeleteAssociationCommand request, CancellationToken cancellationToken)
        {
            var association = await _queryRepository.GetByIdAsync(request.Id);
            if (association is null)
            {
                return Result.Failure(AssociationErrors.NotFound);
            }

            await _commandRepository.DeleteAsync(association);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}