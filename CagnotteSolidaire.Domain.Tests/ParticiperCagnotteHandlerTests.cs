using Xunit;
using Moq;
using CagnotteSolidaire.Domain.Commands.Cagnottes;
using CagnotteSolidaire.Domain.Repositories;
using CagnotteSolidaire.Domain.Entities;
using CagnotteSolidaire.Domain.ValueObjects;

namespace CagnotteSolidaire.Domain.Tests;

public class ParticiperCagnotteHandlerTests
{
    private readonly Mock<ICagnotteRepository> _cagnotteRepoMock;
    private readonly Mock<IUtilisateurRepository> _userRepoMock; 
    private readonly ParticiperCagnotteHandler _handler;

    public ParticiperCagnotteHandlerTests()
    {
        _cagnotteRepoMock = new Mock<ICagnotteRepository>();
        _userRepoMock = new Mock<IUtilisateurRepository>();
        

        _handler = new ParticiperCagnotteHandler(_cagnotteRepoMock.Object, _userRepoMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldAddParticipation_WhenCagnotteExists()
    {
        // Arrange
        var cagnotteId = Guid.NewGuid();
        var participantId = Guid.NewGuid();

        var cagnotte = new Cagnotte(cagnotteId, Guid.NewGuid(), "Test", "Desc", new Money(1000));
        
        var participant = new Participant(participantId, "Doe", "John", new Email("j@test.com"));

        _cagnotteRepoMock.Setup(r => r.GetById(cagnotteId))
                         .ReturnsAsync(cagnotte);
        
        _userRepoMock.Setup(r => r.GetById(participantId))
                     .ReturnsAsync(participant);

        var command = new ParticiperCagnotteCommand(cagnotteId, 100, participantId);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(100, cagnotte.MontantActuel.Value); 
        Assert.Single(cagnotte.Participations); 
        _cagnotteRepoMock.Verify(r => r.Update(cagnotte), Times.Once); 
    }
}