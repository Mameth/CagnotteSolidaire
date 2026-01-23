using CagnotteSolidaire.Domain.ValueObjects;
using CagnotteSolidaire.Domain.Enums;

namespace CagnotteSolidaire.Domain.Entities;

public class Cagnotte
{
    public Guid Id { get; private set; }
    
    public Guid AssociationId { get; private set; }
    public virtual Association Association { get; private set; } = null!;

    public string Nom { get; protected set; } = string.Empty;
    public string Description { get; protected set; } = string.Empty;
    
    public Money Objectif { get; protected set; } = null!;
    public Money MontantActuel { get; protected set; } = null!;
    
    public StatutCagnotte Statut { get; private set; }

    private readonly List<Participation> _participations = new();
    public virtual IReadOnlyCollection<Participation> Participations => _participations.AsReadOnly();

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
        MontantActuel = new Money(0);
    }

    public void Cloturer()
    {
        if (Statut != StatutCagnotte.EnCours)
            throw new InvalidOperationException("La cagnotte est déjà clôturée.");

        if (MontantActuel.Value >= Objectif.Value)
        {
            Statut = StatutCagnotte.Cloturee; 
        }
        else
        {
            Statut = StatutCagnotte.Annulee;
        }
    }

    public bool EstCloturee() => Statut != StatutCagnotte.EnCours;
        
    public void AjouterParticipation(Participation participation)
    {
         _participations.Add(participation);
         MontantActuel = new Money(MontantActuel.Value + participation.Montant.Value);
    }
}