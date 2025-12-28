using CagnotteSolidaire.Domain.ValueObjects;
using CagnotteSolidaire.Domain.Entities;


public class Cagnotte
{
    public Guid Id { get; private set; }
    public Guid AssociationId { get; private set; }
    
    public string Nom { get; protected set; } = string.Empty;
    public string Description { get; protected set; } = string.Empty;
    public Money Objectif { get; protected set; } = null!;
    public StatutCagnotte Statut { get; private set; }

    protected Cagnotte() { }

    public Cagnotte(
        Guid id,
        Guid associationId,
        string nom,
        string description,
        Money objectif)
    {
        Id = id;
        AssociationId = associationId;
        Nom = nom;
        Description = description;
        Objectif = objectif;
        Statut = StatutCagnotte.EnCours;
    }

    public void Cloturer(bool objectifAtteint)
    {
        if (Statut != StatutCagnotte.EnCours)
        throw new InvalidOperationException("La cagnotte est déjà clôturée.");

        Statut = objectifAtteint
            ? StatutCagnotte.Cloturee
            : StatutCagnotte.Annulee;
    }

    public bool EstCloturee()
        => Statut != StatutCagnotte.EnCours;
}
