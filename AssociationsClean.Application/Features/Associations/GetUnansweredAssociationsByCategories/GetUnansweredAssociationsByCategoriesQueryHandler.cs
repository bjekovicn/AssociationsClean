
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

        public async Task<Result<IReadOnlyList<AssociationWithCategory>>> Handle(
            GetUnansweredAssociationsByCategoriesQuery request,
            CancellationToken cancellationToken)
        {
            var answeredIds = await _associationHistoryQueryRepository
                .GetAnsweredAssociationIdsAsync(request.UserUuid);

            var associations = await _associationQueryRepository
                .GetRandomUnansweredByCategoryIdsAsync(
                    request.Count,
                    request.CategoryIds,
                    answeredIds
                );

            var associationIds = associations.Select(a => a.Id).ToList();
            await _associationHistoryCommandRepository.AddManyAsync(request.UserUuid,associationIds);

            await _unitOfWork.SaveChangesAsync();
            return Result.Success<IReadOnlyList<AssociationWithCategory>>(associations);
        }
    }
}
