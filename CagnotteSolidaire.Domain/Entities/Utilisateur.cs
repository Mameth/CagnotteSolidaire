using CagnotteSolidaire.Domain.ValueObjects;

public abstract class Utilisateur
{
    public Guid Id { get; protected set; }
    public string Nom { get; protected set; } = string.Empty;
    public string Prenom { get; protected set; } = string.Empty;
    public Email Email { get; protected set; } = null!;
    
    protected Utilisateur() { }

    protected Utilisateur(Guid id, string nom, string prenom, Email email)
    {
        Id = id;
        Nom = nom;
        Prenom = prenom;
        Email = email;
    }
}
