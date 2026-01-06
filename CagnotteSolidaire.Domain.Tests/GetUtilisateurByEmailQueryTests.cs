using CagnotteSolidaire.Domain.Commands.Utilisateurs;
using CagnotteSolidaire.Domain.ValueObjects;
using CagnotteSolidaire.Domain.Queries.Utilisateurs;
using CagnotteSolidaire.Domain.Tests.Mocks;
using Xunit;

namespace CagnotteSolidaire.Domain.Tests.Queries.Utilisateurs;

public class GetUtilisateurByEmailQueryTests
{
    [Fact]
    public async Task GetUtilisateurByEmail_Existant_RetourneUtilisateur()
    {
        // Arrange
        var repo = new UtilisateurRepositoryMock();
        var utilisateur = new Participant(
            Guid.NewGuid(),
            "Dupont",
            "Jean",
            new Email("jean@test.com")
        );

        await repo.Add(utilisateur);

        var handler = new GetUtilisateurByEmailQueryHandler(repo);
        var query = new GetUtilisateurByEmailQuery("jean@test.com");

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Dupont", result!.Nom);
    }
}
