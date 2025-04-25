
using AssociationsClean.Application.Shared.Abstractions.Messaging;

namespace AssociationsClean.Application.Features.AssociationsHistory.GetAnsweredAssociations
{
    public sealed record GetAnsweredAssociationsQuery(Guid UserUuid) : IQuery<List<AnsweredAssociation>>;
}
