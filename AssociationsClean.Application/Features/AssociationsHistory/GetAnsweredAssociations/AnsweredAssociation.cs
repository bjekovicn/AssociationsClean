

namespace AssociationsClean.Application.Features.AssociationsHistory.GetAnsweredAssociations
{
    public class AnsweredAssociation
    {
        public int AssociationId { get; init; }
        public string AssociationName { get; init; } = default!;
        public bool AnsweredCorrectly { get; init; }
        public DateTime AnsweredAt { get; init; }
    }
}
