using CagnotteSolidaire.Domain.Exceptions;

namespace CagnotteSolidaire.Domain.Entities;

public class Association
{
    public Guid Id { get; private set; }
    public string Nom { get; private set; }
    public string IdentifiantOfficiel { get; private set; } // RNA ou SIREN
    public string Departement { get; private set; }

    protected Association() { }

    public Association(string nom, string identifiantOfficiel, string departement)
    {
        if (string.IsNullOrWhiteSpace(nom))
            throw new BusinessException("Le nom de l'association est obligatoire.");

        if (string.IsNullOrWhiteSpace(identifiantOfficiel))
            throw new BusinessException("L'identifiant officiel est obligatoire.");

        if (departement != "68")
            throw new BusinessException("L'association doit être dans le département 68.");

        Id = Guid.NewGuid();
        Nom = nom;
        IdentifiantOfficiel = identifiantOfficiel;
        Departement = departement;
    }
}
