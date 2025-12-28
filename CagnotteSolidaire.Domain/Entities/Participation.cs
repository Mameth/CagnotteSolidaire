using CagnotteSolidaire.Domain.ValueObjects;

public class Participation
{
    public Guid Id { get; private set; }
    public Guid CagnotteId { get; private set; }
    public Guid ParticipantId { get; private set; }

    public Money Montant { get; protected set; } = null!;

    protected Participation() { }

    public Participation(
        Guid id,
        Guid cagnotteId,
        Guid participantId,
        Money montant)
    {
        Id = id;
        CagnotteId = cagnotteId;
        ParticipantId = participantId;
        Montant = montant;
    }
}
