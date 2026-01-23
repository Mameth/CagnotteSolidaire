namespace CagnotteSolidaire.Domain.Entities;

public class Association
{
    public Guid Id { get; protected set; }
    public string Nom { get; protected set; } = string.Empty;
    public string NumeroRNA { get; protected set; } = string.Empty;
    public string Departement { get; protected set; } = string.Empty;
    public string Description { get; protected set; } = string.Empty;

    protected Association() { }

    public Association(Guid id, string nom, string numeroRna, string departement, string description)
    {
        Id = id;
        Nom = nom;
        NumeroRNA = numeroRna;
        Departement = departement;
        Description = description;
    }
}