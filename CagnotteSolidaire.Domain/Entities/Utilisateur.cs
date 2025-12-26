using CagnotteSolidaire.Domain.Enums;
using CagnotteSolidaire.Domain.Exceptions;
using CagnotteSolidaire.Domain.ValueObjects;

namespace CagnotteSolidaire.Domain.Entities;

public class Utilisateur
{
    public Guid Id { get; private set; }
    public string Nom { get; private set; }
    public string Prenom { get; private set; }
    public Email Email { get; private set; }
    public RoleUtilisateur Role { get; private set; }

    protected Utilisateur() { }

    public Utilisateur(string nom, string prenom, Email email, RoleUtilisateur role)
    {
        if (string.IsNullOrWhiteSpace(nom))
            throw new BusinessException("Le nom est obligatoire.");

        if (string.IsNullOrWhiteSpace(prenom))
            throw new BusinessException("Le prénom est obligatoire.");

        Id = Guid.NewGuid();
        Nom = nom;
        Prenom = prenom;
        Email = email ?? throw new BusinessException("L'email est obligatoire.");
        Role = role;
    }

    public bool EstGestionnaire()
        => Role == RoleUtilisateur.Gestionnaire;

    public bool EstParticipant()
        => Role == RoleUtilisateur.Participant;
}
