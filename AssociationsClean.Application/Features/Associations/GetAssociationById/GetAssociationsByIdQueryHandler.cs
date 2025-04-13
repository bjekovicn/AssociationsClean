using AssociationsClean.Domain.Shared.Abstractions;
using AssociationsClean.Domain.Features.Associations;
using AssociationsClean.Application.Shared.Abstractions.Messaging;
using AssociationsClean.Application.Features.Associations;

namespace Associations.Application.Features.Associations.GetAssociations
{
    internal sealed class GetAssociationByIdQueryHandler : IQueryHandler<GetAssociationByIdQuery, Association>
    {
        private readonly IAssociationQueryRepository _associationQueryRepository;

        public GetAssociationByIdQueryHandler(IAssociationQueryRepository associationQueryRepository)
        {
            _associationQueryRepository = associationQueryRepository;
        }

        public async Task<Result<Association>> Handle(GetAssociationByIdQuery request, CancellationToken cancellationToken)
        {
            var association = await _associationQueryRepository.GetByIdAsync(request.Id);
            if (association == null)
            {
                return Result.Failure<Association>(AssociationErrors.NotFound);
            }

            return Result.Success(association);
        }
    }
}