using Xunit;
using Moq;
using CagnotteSolidaire.Domain.Commands.Cagnottes;
using CagnotteSolidaire.Domain.Repositories;
using CagnotteSolidaire.Domain.Entities;
using CagnotteSolidaire.Domain.ValueObjects;
using CagnotteSolidaire.Domain.Services;
using CagnotteSolidaire.Domain.Enums;

namespace CagnotteSolidaire.Domain.Tests;

public class CloturerCagnotteHandlerTests
{
    private readonly Mock<ICagnotteRepository> _cagnotteRepoMock;
    private readonly Mock<IEmailService> _emailMock;
    private readonly CloturerCagnotteHandler _handler;

    public CloturerCagnotteHandlerTests()
    {
        _cagnotteRepoMock = new Mock<ICagnotteRepository>();
        _emailMock = new Mock<IEmailService>();
        _handler = new CloturerCagnotteHandler(_cagnotteRepoMock.Object, _emailMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCloturer_WhenObjectifAtteint()
    {
        // Arrange
        var cagnotteId = Guid.NewGuid();
        var participantId = Guid.NewGuid();
        var cagnotte = new Cagnotte(cagnotteId, Guid.NewGuid(), "Succes", "Desc", new Money(1000));
        
        var participation = new Participation(Guid.NewGuid(), cagnotteId, participantId, new Money(1000));
        cagnotte.AjouterParticipation(participation); 

        _cagnotteRepoMock.Setup(r => r.GetById(cagnotteId)).ReturnsAsync(cagnotte);

        // Act
        await _handler.Handle(new CloturerCagnotteCommand(cagnotteId, Guid.NewGuid()), CancellationToken.None);

        // Assert
        Assert.Equal(StatutCagnotte.Cloturee, cagnotte.Statut); 
        _emailMock.Verify(e => e.EnvoyerEmail(It.IsAny<string>(), It.Is<string>(s => s.Contains("atteint")), It.IsAny<string>()), Times.AtLeastOnce);
    }

    [Fact]
    public async Task Handle_ShouldAnnuler_WhenObjectifNonAtteint()
    {
        // Arrange 
        var cagnotteId = Guid.NewGuid();
        var participantId = Guid.NewGuid();
        var cagnotte = new Cagnotte(cagnotteId, Guid.NewGuid(), "Fail", "Desc", new Money(1000));
        

        var participation = new Participation(Guid.NewGuid(), cagnotteId, participantId, new Money(500));
        cagnotte.AjouterParticipation(participation); 

        _cagnotteRepoMock.Setup(r => r.GetById(cagnotteId)).ReturnsAsync(cagnotte);

        // Act
        await _handler.Handle(new CloturerCagnotteCommand(cagnotteId, Guid.NewGuid()), CancellationToken.None);

        // Assert
        Assert.Equal(StatutCagnotte.Annulee, cagnotte.Statut); 
        _emailMock.Verify(e => e.EnvoyerEmail(It.IsAny<string>(), It.Is<string>(s => s.Contains("annulée")), It.IsAny<string>()), Times.AtLeastOnce);
    }
}