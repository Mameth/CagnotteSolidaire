using Microsoft.EntityFrameworkCore;
using CagnotteSolidaire.Domain.Entities;

namespace CagnotteSolidaire.Infrastructure.Persistence;

public class CagnotteDbContext : DbContext
{
    public DbSet<Utilisateur> Utilisateurs => Set<Utilisateur>();
    public DbSet<Association> Associations => Set<Association>();

    public DbSet<Cagnotte> Cagnottes => Set<Cagnotte>();
    public DbSet<Participation> Participations => Set<Participation>();

    public CagnotteDbContext(DbContextOptions<CagnotteDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CagnotteDbContext).Assembly);


        modelBuilder.Entity<Cagnotte>(entity =>
        {
            entity.OwnsOne(c => c.Objectif, m =>
            {
                m.Property(p => p.Value).HasColumnName("Objectif").HasPrecision(18, 2);
            });

            entity.OwnsOne(c => c.MontantActuel, m =>
            {
                m.Property(p => p.Value).HasColumnName("MontantActuel").HasPrecision(18, 2);
            });
        });

        modelBuilder.Entity<Participation>(entity =>
        {
            entity.OwnsOne(p => p.Montant, m =>
            {
                m.Property(p => p.Value).HasColumnName("Montant").HasPrecision(18, 2);
            });
        });
        
    }
}