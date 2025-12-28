public class Association
{
    public Guid Id { get; protected set; }
    public string Nom { get; protected set; } = string.Empty;
    public string NumeroRNA { get; protected set; } = string.Empty;
    public string Departement { get; protected set; } = string.Empty;

    protected Association() { } // EF Core

    public Association(Guid id, string nom, string numeroRna, string departement)
    {
        Id = id;
        Nom = nom;
        NumeroRNA = numeroRna;
        Departement = departement;
    }
}
