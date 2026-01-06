using CagnotteSolidaire.Domain.Entities;
using CagnotteSolidaire.Domain.Repositories;

namespace CagnotteSolidaire.Domain.Tests.Mocks;

public class UtilisateurRepositoryMock : IUtilisateurRepository
{
    private readonly List<Utilisateur> _utilisateurs = new();

    public Task<Utilisateur?> GetByEmail(string email)
    {
        var user = _utilisateurs.FirstOrDefault(u => u.Email.Value == email);
        return Task.FromResult(user);
    }

    public Task Add(Utilisateur utilisateur)
    {
        _utilisateurs.Add(utilisateur);
        return Task.CompletedTask;
    }
}
