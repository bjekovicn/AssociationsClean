
using AssociationsClean.Domain.Shared.Abstractions;
using AssociationsClean.Domain.Features.Associations;
using AssociationsClean.Application.Shared.Abstractions.Messaging;

namespace AssociationsClean.Application.Features.Associations.GetAsociationsByCategory
{
    internal sealed class GetAssociationsByCategoryQueryHandler : IQueryHandler<GetAssociationsByCategoryQuery, IReadOnlyList<Association>>
    {
        private readonly IAssociationQueryRepository _associationQueryRepository;

        public GetAssociationsByCategoryQueryHandler(IAssociationQueryRepository associationQueryRepository)
        {
            _associationQueryRepository = associationQueryRepository;
        }

        public async Task<Result<IReadOnlyList<Association>>> Handle(GetAssociationsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var associations = await _associationQueryRepository.GetByCategoryIdAsync(request.CategoryId);
            return Result.Success<IReadOnlyList<Association>>(associations);
        }
    }
}
