using CagnotteSolidaire.Domain.Entities;
using CagnotteSolidaire.Domain.Repositories;
using CagnotteSolidaire.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CagnotteSolidaire.Infrastructure.Repositories;

public class CagnotteRepository : ICagnotteRepository
{
    private readonly CagnotteDbContext _context;

    public CagnotteRepository(CagnotteDbContext context)
    {
        _context = context;
    }

    public async Task Add(Cagnotte cagnotte)
    {
        await _context.Cagnottes.AddAsync(cagnotte);
        await _context.SaveChangesAsync();
    }

    public async Task<Cagnotte?> GetById(Guid id)
    {
        return await _context.Cagnottes
            .Include(c => c.Association)
            .Include(c => c.Participations)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Cagnotte>> GetByGestionnaireId(Guid gestionnaireId)
    {
        var gestionnaire = await _context.Utilisateurs
             .OfType<Gestionnaire>()
             .FirstOrDefaultAsync(g => g.Id == gestionnaireId);
             
        if (gestionnaire == null) return new List<Cagnotte>();
        
        return await _context.Cagnottes
             .Where(c => c.AssociationId == gestionnaire.AssociationId)
             .ToListAsync();
    }

    public async Task<IEnumerable<Cagnotte>> GetAll()
    {
        return await _context.Cagnottes
            .Include(c => c.Association) 
            .ToListAsync();
    }

    public async Task Update(Cagnotte cagnotte)
    {
        _context.ChangeTracker.DetectChanges();

        foreach (var participation in cagnotte.Participations)
        {
            var entry = _context.Entry(participation);

            if (entry.State == EntityState.Modified)
            {
                entry.State = EntityState.Added;
            }
            else if (entry.State == EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
        }

        await _context.SaveChangesAsync();
    }
}