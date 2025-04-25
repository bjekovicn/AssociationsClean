
using AssociationsClean.Application.Shared.Abstractions.Messaging;
using AssociationsClean.Domain.Shared.Abstractions;
using MediatR;

namespace AssociationsClean.Application.Features.AssociationsHistory.AddManyAssociationHistories
{
    public sealed class AddManyAssociationHistoriesCommandHandler:ICommandHandler<AddManyAssociationHistoriesCommand>
    {
        private readonly IAssociationHistoryCommandRepository _commandRepository;
        private readonly IAssociationHistoryQueryRepository _queryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddManyAssociationHistoriesCommandHandler(
            IAssociationHistoryCommandRepository commandRepository,
            IAssociationHistoryQueryRepository queryRepository,
            IUnitOfWork unitOfWork)
        {
            _commandRepository = commandRepository;
            _unitOfWork = unitOfWork;
            _queryRepository = queryRepository;
        }


        async Task<Result> IRequestHandler<AddManyAssociationHistoriesCommand, Result>.Handle(AddManyAssociationHistoriesCommand request, CancellationToken cancellationToken)
        {
            var alreadyAnswered = await _queryRepository.GetAnsweredAssociationIdsAsync(request.UserUuid);

            var newAssociations = request.Associations
                .Where(kvp => !alreadyAnswered.Contains(kvp.Key))
                .Select(kvp => (kvp.Key, kvp.Value))
                .ToList();

            if (!newAssociations.Any()) return Result.Success();

            await _commandRepository.AddManyAsync(request.UserUuid, newAssociations);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
