using AssociationsClean.Domain.Features.Associations;
using AssociationsClean.Domain.Shared.Abstractions;
using AssociationsClean.Application.Shared.Abstractions.Messaging;

namespace AssociationsClean.Application.Features.Associations.GetAllAssociations
{
    internal sealed class GetAllAssociationsQueryHandler : IQueryHandler<GetAllAssociationsQuery, IReadOnlyList<Association>>
    {
        private readonly IAssociationQueryRepository _associationQueryRepository;

        public GetAllAssociationsQueryHandler(IAssociationQueryRepository associationQueryRepository)
        {
            _associationQueryRepository = associationQueryRepository;
        }

        public async Task<Result<IReadOnlyList<Association>>> Handle(GetAllAssociationsQuery request, CancellationToken cancellationToken)
        {
            var associations = await _associationQueryRepository.GetAllAsync();
            return Result.Success<IReadOnlyList<Association>>(associations);
        }
    }
}