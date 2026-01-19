using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CagnotteSolidaire.Domain.Entities;

namespace CagnotteSolidaire.Infrastructure.Persistence.Configurations;

public class AssociationConfiguration : IEntityTypeConfiguration<Association>
{
    public void Configure(EntityTypeBuilder<Association> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Nom)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(a => a.NumeroRNA)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(a => a.Departement)
            .IsRequired()
            .HasMaxLength(3);

        builder.HasIndex(a => a.NumeroRNA)
            .IsUnique();
    }
}
