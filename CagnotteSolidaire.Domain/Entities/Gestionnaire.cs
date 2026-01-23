using CagnotteSolidaire.Domain.ValueObjects;

namespace CagnotteSolidaire.Domain.Entities;

public class Gestionnaire : Utilisateur
{
    public Guid AssociationId { get; private set; }

    
    public virtual Association Association { get; private set; } = null!;

    protected Gestionnaire() { }

   
    public Gestionnaire(
        Guid id,
        string nom,
        string prenom,
        Email email,
        string motDePasse,
        Guid associationId)
        : base(id, nom, prenom, email, motDePasse)
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