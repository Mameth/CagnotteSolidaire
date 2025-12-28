using CagnotteSolidaire.Domain.ValueObjects;

namespace CagnotteSolidaire.Domain.Entities;

public class Gestionnaire : Utilisateur
{
    public Guid AssociationId { get; private set; }

    protected Gestionnaire() { }

    public Gestionnaire(
        Guid id,
        string nom,
        string prenom,
        Email email,
        Guid associationId)
        : base(id, nom, prenom, email)
    {
        AssociationId = associationId;
    }

    public Cagnotte CreerCagnotte(
        string nom,
        string description,
        Money objectif)
    {
        return new Cagnotte(
            Guid.NewGuid(),
            AssociationId,
            nom,
            description,
            objectif);
    }
}
