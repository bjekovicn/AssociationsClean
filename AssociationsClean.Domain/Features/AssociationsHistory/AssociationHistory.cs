public sealed class AssociationHistory
{
    public Guid UserUuid { get; private set; }
    public int AssociationId { get; private set; }
    public DateTime AnsweredAt { get; private set; }

    public AssociationHistory(Guid userUuid, int associationId)
    {
        UserUuid = userUuid;
        AssociationId = associationId;
        AnsweredAt = DateTime.UtcNow;
    }

    private AssociationHistory() { }
}
