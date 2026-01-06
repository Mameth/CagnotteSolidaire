using CagnotteSolidaire.Domain.Commands.Utilisateurs;
using CagnotteSolidaire.Domain.Tests.Mocks;
using Xunit;
using UtilisateurRepositoryMockTest = CagnotteSolidaire.Domain.Tests.Mocks.UtilisateurRepositoryMock;

namespace CagnotteSolidaire.Domain.Tests.Handlers;

public class InscrireParticipantHandlerTests
{
    [Fact]
    public async Task InscrireParticipant_NouvelEmail_CreeParticipant()
    {
        // Arrange
        var repository = new UtilisateurRepositoryMock();
        var handler = new InscrireParticipantHandler(repository);

        var command = new InscrireParticipantCommand(
            Nom: "Dupont",
            Prenom: "Jean",
            Email: "jean.dupont@test.com"
        );

        // Act
        var id = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotEqual(Guid.Empty, id);
    }

    [Fact]
    public async Task InscrireParticipant_EmailDejaExistant_ThrowException()
    {
        // Arrange
        var repository = new UtilisateurRepositoryMock();
        var handler = new InscrireParticipantHandler(repository);

        var command = new InscrireParticipantCommand(
            "Dupont",
            "Jean",
            "jean.dupont@test.com"
        );

        await handler.Handle(command, CancellationToken.None);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            handler.Handle(command, CancellationToken.None)
        );
    }
}
