
using AssociationsClean.Application.Shared.Abstractions.Messaging;
using AssociationsClean.Domain.Shared.Abstractions;

namespace AssociationsClean.Application.Features.AssociationsHistory.GetAnsweredAssociations
{
    public sealed class GetAnsweredAssociationsQueryHandler : IQueryHandler<GetAnsweredAssociationsQuery, List<AnsweredAssociation>>
    {
        private readonly IAssociationHistoryQueryRepository _repository;

        public GetAnsweredAssociationsQueryHandler(IAssociationHistoryQueryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<AnsweredAssociation>>> Handle(GetAnsweredAssociationsQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAnsweredAssociationsWithDetailsAsync(request.UserUuid);
            return Result.Success(result);
        }
    }
}
