
using AssociationsClean.Application.Features.Associations.GetRandomAssociationsByCategoryIds;
using AssociationsClean.Application.Features.AssociationsHistory;
using AssociationsClean.Application.Shared.Abstractions.Messaging;
using AssociationsClean.Domain.Shared.Abstractions;

namespace AssociationsClean.Application.Features.Associations.GetUnansweredAssociationsByCategoryIds
{
    internal class GetUnansweredAssociationsByCategoriesQueryHandler
        : IQueryHandler<GetUnansweredAssociationsByCategoriesQuery, IReadOnlyList<AssociationWithCategory>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAssociationQueryRepository _associationQueryRepository;
        private readonly IAssociationHistoryQueryRepository _associationHistoryQueryRepository;
        private readonly IAssociationHistoryCommandRepository _associationHistoryCommandRepository;

        public GetUnansweredAssociationsByCategoriesQueryHandler(
            IUnitOfWork unitOfWork,
            IAssociationQueryRepository associationQueryRepository,
            IAssociationHistoryQueryRepository associationHistoryQueryRepository,
            IAssociationHistoryCommandRepository associationHistoryCommandRepository)
        {
            _unitOfWork = unitOfWork;
            _associationQueryRepository = associationQueryRepository;
            _associationHistoryQueryRepository = associationHistoryQueryRepository;
            _associationHistoryCommandRepository = associationHistoryCommandRepository; 
        }

        private int _count = 20;
        public async Task<Result<IReadOnlyList<AssociationWithCategory>>> Handle(
        GetUnansweredAssociationsByCategoriesQuery request,
        CancellationToken cancellationToken)
        {
            var answeredIds = await _associationHistoryQueryRepository
                .GetAnsweredAssociationIdsAsync(request.UserUuid);


            var unansweredAssociations = await _associationQueryRepository
                .GetRandomUnansweredByCategoryIdsAsync(
                    _count,
                    request.CategoryIds,
                    answeredIds
                );

            var result = unansweredAssociations.ToList();

            if (result.Count < _count && answeredIds.Any())
            {
                int remainingCount = _count - result.Count;

                var oldestAnsweredAssociations = await _associationHistoryQueryRepository
                    .GetOldestAnsweredAssociationsByCategoryIdsAsync(
                        request.UserUuid,
                        request.CategoryIds,
                        remainingCount
                    );

                // Delete the history entries for these associations
                var oldestAnsweredAssociationIds = oldestAnsweredAssociations.Select(a => a.Id).ToList();
                await _associationHistoryCommandRepository.DeleteManyAsync(
                      request.UserUuid,
                      oldestAnsweredAssociationIds);

                result.AddRange(oldestAnsweredAssociations);
            }

            var associationIds = result.Select(a => a.Id).ToList();
            await _unitOfWork.SaveChangesAsync();

            return Result.Success<IReadOnlyList<AssociationWithCategory>>(result);
        }
    }
}
