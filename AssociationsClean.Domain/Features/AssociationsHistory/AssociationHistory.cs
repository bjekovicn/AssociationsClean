public sealed class AssociationHistory
{
    public Guid UserUuid { get; private set; }
    public int AssociationId { get; private set; }

    public bool AnsweredCorrectly { get; private set; }
    public DateTime AnsweredAt { get; private set; }

    public AssociationHistory(Guid userUuid, int associationId, bool answeredCorrectly)
    {
        UserUuid = userUuid;
        AssociationId = associationId;
        AnsweredAt = DateTime.UtcNow;
        AnsweredCorrectly = answeredCorrectly;
    }

    private AssociationHistory() { }
}
