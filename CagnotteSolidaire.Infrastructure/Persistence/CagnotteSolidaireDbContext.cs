using Microsoft.EntityFrameworkCore;
using CagnotteSolidaire.Domain.Entities;

namespace CagnotteSolidaire.Infrastructure.Persistence;

public class CagnotteDbContext : DbContext
{
    public DbSet<Utilisateur> Utilisateurs => Set<Utilisateur>();
    public DbSet<Association> Associations => Set<Association>();

    public CagnotteDbContext(DbContextOptions<CagnotteDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CagnotteDbContext).Assembly);
    }
}
