
using AssociationsClean.Application.Shared.Abstractions.Repositories;
using AssociationsClean.Domain.Features.Associations;

namespace AssociationsClean.Application.Features.Associations
{
    public interface IAssociationCommandRepository: IBaseCommandRepository<Association>
    {
    }
}
