using AssociationsClean.Domain.Shared.Abstractions;
using AssociationsClean.Domain.Features.Associations;
using AssociationsClean.Application.Shared.Abstractions.Messaging;

namespace AssociationsClean.Application.Features.Associations.UpdateAssociation
{
    internal sealed class UpdateAssociationCommandHandler : ICommandHandler<UpdateAssociationCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAssociationQueryRepository _queryRepository;
        private readonly IAssociationCommandRepository _commandRepository;

        public UpdateAssociationCommandHandler(
            IUnitOfWork unitOfWork,
            IAssociationQueryRepository queryRepository,
            IAssociationCommandRepository commandRepository)
        {
            _unitOfWork = unitOfWork;
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
        }

        public async Task<Result> Handle(UpdateAssociationCommand request, CancellationToken cancellationToken)
        {
            var association = await _queryRepository.GetByIdAsync(request.Id);
            if (association is null)
            {
                return Result.Failure(AssociationErrors.NotFound);
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                association.ChangeName(request.Name);
            }

            if (request.Description is not null)
            {
                association.ChangeDescription(request.Description);
            }

            if (request.CategoryId.HasValue)
            {
                association.ChangeCategory(request.CategoryId.Value);
            }

            await _commandRepository.UpdateAsync(association);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}