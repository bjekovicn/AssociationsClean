
using AssociationsClean.Application.Shared.Abstractions.Messaging;

namespace AssociationsClean.Application.Features.AssociationsHistory.AddManyAssociationHistories
{
    public sealed record AddManyAssociationHistoriesCommand(
        Guid UserUuid,
        Dictionary<int, bool> Associations
    ) : ICommand;

}
