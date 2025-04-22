

using AssociationsClean.Domain.Shared.Abstractions;
using AssociationsClean.Application.Shared.Abstractions.Messaging;
using AssociationsClean.Application.Features.AssociationsHistory;

namespace AssociationsClean.Application.Features.Associations.GetRandomAssociationsByCategoryIds
{
    internal sealed class GetRandomAssociationsByCategoriesQyeryHandler : IQueryHandler<GetRandomAssociationsByCategoriesQuery, IReadOnlyList<AssociationWithCategory>>
    {
        private readonly IAssociationQueryRepository _associationQueryRepository;

        public GetRandomAssociationsByCategoriesQyeryHandler(
            IAssociationQueryRepository associationQueryRepository)
        {
            _associationQueryRepository = associationQueryRepository;
        }



        public async Task<Result<IReadOnlyList<AssociationWithCategory>>> Handle(GetRandomAssociationsByCategoriesQuery request, CancellationToken cancellationToken)
        {

            var associations = await _associationQueryRepository.GetRandomByCategoryIdsAsync(request.count,request.categoryIds);
            return Result.Success<IReadOnlyList<AssociationWithCategory>>(associations);
        }
    }
}
