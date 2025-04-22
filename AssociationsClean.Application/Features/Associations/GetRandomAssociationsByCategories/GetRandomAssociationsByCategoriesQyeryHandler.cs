

using AssociationsClean.Domain.Shared.Abstractions;
using AssociationsClean.Application.Shared.Abstractions.Messaging;
using AssociationsClean.Application.Features.AssociationsHistory;

namespace AssociationsClean.Application.Features.Associations.GetRandomAssociationsByCategoryIds
{
    internal sealed class GetRandomAssociationsByCategoriesQyeryHandler : IQueryHandler<GetRandomAssociationsByCategoriesQuery, IReadOnlyList<AssociationWithCategory>>
    {
        private readonly IAssociationQueryRepository _associationQueryRepository;
        private readonly IAssociationHistoryQueryRepository _associationHistoryQueryRepository;

        public GetRandomAssociationsByCategoriesQyeryHandler(
            IAssociationQueryRepository associationQueryRepository, 
            IAssociationHistoryQueryRepository associationHistoryQueryRepository)
        {
            _associationQueryRepository = associationQueryRepository;
            _associationHistoryQueryRepository = associationHistoryQueryRepository;
        }



        public async Task<Result<IReadOnlyList<AssociationWithCategory>>> Handle(GetRandomAssociationsByCategoriesQuery request, CancellationToken cancellationToken)
        {

            var associations = await _associationQueryRepository.GetRandomByCategoryIdsAsync(request.count,request.categoryIds);
            return Result.Success<IReadOnlyList<AssociationWithCategory>>(associations);
        }
    }
}
