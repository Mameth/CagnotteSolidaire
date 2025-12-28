using CagnotteSolidaire.Domain.ValueObjects;


public class Participant : Utilisateur
{
    protected Participant() { }

    public Participant(Guid id, string nom, string prenom, Email email)
        : base(id, nom, prenom, email)
    {
    }

    public Participation Participer(Cagnotte cagnotte, Money montant)
    {
        if (cagnotte.EstCloturee())
        throw new InvalidOperationException("La cagnotte est déjà clôturée.");

        return new Participation(
            Guid.NewGuid(),
            cagnotte.Id,
            Id,
            montant);
    }
}
