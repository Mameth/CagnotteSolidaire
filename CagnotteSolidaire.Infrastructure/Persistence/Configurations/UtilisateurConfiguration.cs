using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CagnotteSolidaire.Domain.Entities;

namespace CagnotteSolidaire.Infrastructure.Persistence.Configurations;

public class UtilisateurConfiguration : IEntityTypeConfiguration<Utilisateur>
{
    public void Configure(EntityTypeBuilder<Utilisateur> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Nom)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Prenom)
            .IsRequired()
            .HasMaxLength(100);

        builder.OwnsOne(u => u.Email, email =>
        {
            email.Property(e => e.Value)
                 .HasColumnName("Email")
                 .IsRequired();
        });

        builder.HasDiscriminator<string>("TypeUtilisateur")
            .HasValue<Participant>("Participant")
            .HasValue<Gestionnaire>("Gestionnaire");
    }
}
