using CagnotteSolidaire.Domain.Enums;
using CagnotteSolidaire.Domain.Exceptions;
using CagnotteSolidaire.Domain.ValueObjects;

namespace CagnotteSolidaire.Domain.Entities;

public class Cagnotte
{
    public Guid Id { get; private set; }
    public string Nom { get; private set; }
    public string Description { get; private set; }
    public Money Objectif { get; private set; }
    public Money MontantActuel { get; private set; }
    public StatutCagnotte Statut { get; private set; }
    public Association Association { get; private set; }

    protected Cagnotte() { }

    public Cagnotte(
        string nom,
        string description,
        Money objectif,
        Association association,
        Utilisateur createur)
    {
        if (string.IsNullOrWhiteSpace(nom))
            throw new BusinessException("Le nom de la cagnotte est obligatoire.");

        if (objectif is null || objectif.Valeur <= 0)
            throw new BusinessException("L'objectif doit être supérieur à 0.");

        if (association is null)
            throw new BusinessException("Une association est obligatoire.");

        if (createur is null || !createur.EstGestionnaire())
            throw new BusinessException("Seul un gestionnaire peut créer une cagnotte.");

        Id = Guid.NewGuid();
        Nom = nom;
        Description = description;
        Objectif = objectif;
        Association = association;
        MontantActuel = Money.Creer(0);
        Statut = StatutCagnotte.Ouverte;
    }

    public void AjouterParticipation(Participation participation)
    {
        if (Statut != StatutCagnotte.Ouverte)
            throw new BusinessException("La cagnotte n'est pas ouverte.");

        MontantActuel = MontantActuel + participation.Montant;
    }

    public void Cloturer()
    {
        if (Statut != StatutCagnotte.Ouverte)
            throw new BusinessException("La cagnotte est déjà clôturée ou annulée.");

        Statut = MontantActuel.Valeur >= Objectif.Valeur
            ? StatutCagnotte.Cloturee
            : StatutCagnotte.Annulee;
    }
}
