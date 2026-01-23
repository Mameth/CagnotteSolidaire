using Xunit;
using Moq;
using CagnotteSolidaire.Domain.Commands.Cagnottes;
using CagnotteSolidaire.Domain.Repositories;
using CagnotteSolidaire.Domain.Entities;
using CagnotteSolidaire.Domain.ValueObjects;

namespace CagnotteSolidaire.Domain.Tests;

public class CreerCagnotteHandlerTests
{
    private readonly Mock<ICagnotteRepository> _cagnotteRepoMock;
    private readonly Mock<IUtilisateurRepository> _userRepoMock;
    private readonly CreerCagnotteHandler _handler;

    public CreerCagnotteHandlerTests()
    {
        _cagnotteRepoMock = new Mock<ICagnotteRepository>();
        _userRepoMock = new Mock<IUtilisateurRepository>();
        _handler = new CreerCagnotteHandler(_cagnotteRepoMock.Object, _userRepoMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateCagnotte_WhenUserIsGestionnaire()
    {
        // Arrange
        var gestionnaireId = Guid.NewGuid();
        var gestionnaire = new Gestionnaire(gestionnaireId, "Nom", "Prenom", new Email("g@test.com"), Guid.NewGuid());
        
        _userRepoMock.Setup(repo => repo.GetById(gestionnaireId))
                     .ReturnsAsync(gestionnaire);

        var command = new CreerCagnotteCommand("Titre", "Desc", 1000m, gestionnaireId);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotEqual(Guid.Empty, result); // On vérifie qu'un ID a été généré
        _cagnotteRepoMock.Verify(repo => repo.Add(It.IsAny<Cagnotte>()), Times.Once); // On vérifie que la sauvegarde a été appelée
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenUserIsParticipant()
    {
        // Arrange
        var participantId = Guid.NewGuid();
        var participant = new Participant(participantId, "Nom", "Prenom", new Email("p@test.com"));

        _userRepoMock.Setup(repo => repo.GetById(participantId))
                     .ReturnsAsync(participant);

        var command = new CreerCagnotteCommand("Titre", "Desc", 1000m, participantId);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => 
            _handler.Handle(command, CancellationToken.None));
    }
}