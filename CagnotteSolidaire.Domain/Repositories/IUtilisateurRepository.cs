using CagnotteSolidaire.Domain.Entities;

namespace CagnotteSolidaire.Domain.Repositories;

public interface IUtilisateurRepository
{
    Task<Utilisateur?> GetByEmail(string email);
    Task Add(Utilisateur utilisateur);

    Task<Utilisateur?> GetById(Guid id);
}