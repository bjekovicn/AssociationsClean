

using AssociationsClean.Domain.Shared.Abstractions;
using AssociationsClean.Domain.Features.Associations;
using AssociationsClean.Application.Shared.Abstractions.Messaging;

namespace AssociationsClean.Application.Features.Associations.GetRandomAssociationsByCategoryIds
{
    internal sealed class GetRandomAssociationsByCategoryIdsQyeryHandler : IQueryHandler<GerRandomAssociationsByCategoryIdsQuery, IReadOnlyList<AssociationWithCategory>>
    {
        private readonly IAssociationQueryRepository _associationQueryRepository;

        public GetRandomAssociationsByCategoryIdsQyeryHandler(IAssociationQueryRepository associationQueryRepository)
        {
            _associationQueryRepository = associationQueryRepository;
        }



        public async Task<Result<IReadOnlyList<AssociationWithCategory>>> Handle(GerRandomAssociationsByCategoryIdsQuery request, CancellationToken cancellationToken)
        {
            var associations = await _associationQueryRepository.GetRandomByCategoryIdsAsync(request.count,request.categoryIds);
            return Result.Success<IReadOnlyList<AssociationWithCategory>>(associations);
        }
    }
}
