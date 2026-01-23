using CagnotteSolidaire.Domain.ValueObjects;

namespace CagnotteSolidaire.Domain.Entities; 

public class Participant : Utilisateur
{
    protected Participant() { }

    public Participant(Guid id, string nom, string prenom, Email email, string motDePasse)
        : base(id, nom, prenom, email, motDePasse)
    {
    }

    public Participation Participer(Cagnotte cagnotte, Money montant)
    {
        if (cagnotte.EstCloturee())
            throw new InvalidOperationException("La cagnotte est déjà clôturée.");

        return new Participation(
            Guid.NewGuid(), 
            cagnotte.Id,    
            this.Id,        
            montant         
        );
    }
}