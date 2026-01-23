using CagnotteSolidaire.Domain.ValueObjects;

namespace CagnotteSolidaire.Domain.Entities;

public abstract class Utilisateur
{
    public Guid Id { get; protected set; }
    public string Nom { get; protected set; } = string.Empty;
    public string Prenom { get; protected set; } = string.Empty;
    public Email Email { get; protected set; } = null!;
    
    public string MotDePasse { get; protected set; } = string.Empty;
    
    protected Utilisateur() { }

    protected Utilisateur(Guid id, string nom, string prenom, Email email, string motDePasse)
    {
        Id = id;
        Nom = nom;
        Prenom = prenom;
        Email = email;
        MotDePasse = motDePasse;
    }
}