using CagnotteSolidaire.Domain.Entities;
using CagnotteSolidaire.Domain.Repositories;
using CagnotteSolidaire.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CagnotteSolidaire.Infrastructure.Repositories;

public class AssociationRepository : IAssociationRepository
{
    private readonly CagnotteDbContext _context;

    public AssociationRepository(CagnotteDbContext context)
    {
        _context = context;
    }

    public Task<Association?> GetById(Guid id)
        => _context.Associations.FindAsync(id).AsTask();

    public Task<Association?> GetByRna(string numeroRna)
        => _context.Associations
            .FirstOrDefaultAsync(a => a.NumeroRNA == numeroRna);

    public async Task Add(Association association)
    {
        _context.Associations.Add(association);
        await _context.SaveChangesAsync();
    }
}
