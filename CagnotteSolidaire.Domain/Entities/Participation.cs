using CagnotteSolidaire.Domain.Exceptions;
using CagnotteSolidaire.Domain.ValueObjects;

namespace CagnotteSolidaire.Domain.Entities;

public class Participation
{
    public Guid Id { get; private set; }
    public Guid CagnotteId { get; private set; }
    public Utilisateur Participant { get; private set; }
    public Money Montant { get; private set; }
    public DateTime DateParticipation { get; private set; }

    protected Participation() { }

    public Participation(Guid cagnotteId, Utilisateur participant, Money montant)
    {
        if (participant is null)
            throw new BusinessException("Le participant est obligatoire.");

        if (!participant.EstParticipant())
            throw new BusinessException("Seuls les participants peuvent participer.");

        if (montant is null || montant.Valeur <= 0)
            throw new BusinessException("Le montant de participation doit être supérieur à 0.");

        Id = Guid.NewGuid();
        CagnotteId = cagnotteId;
        Participant = participant;
        Montant = montant;
        DateParticipation = DateTime.UtcNow;
    }
}
