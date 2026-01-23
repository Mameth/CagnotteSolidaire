using CagnotteSolidaire.Domain.Entities;
using CagnotteSolidaire.Domain.Repositories;
using CagnotteSolidaire.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CagnotteSolidaire.Infrastructure.Repositories;

public class UtilisateurRepository : IUtilisateurRepository
{
    private readonly CagnotteDbContext _context;

    public UtilisateurRepository(CagnotteDbContext context)
    {
        _context = context;
    }

    public Task<Utilisateur?> GetByEmail(string email)
    {
        return _context.Utilisateurs
            .FirstOrDefaultAsync(u => u.Email.Value == email);
    }

    public Task<Utilisateur?> GetById(Guid id)
    {
        return _context.Utilisateurs
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task Add(Utilisateur utilisateur)
    {
        _context.Utilisateurs.Add(utilisateur);
        await _context.SaveChangesAsync();
    }
}