using CagnotteSolidaire.Domain.Entities;
using CagnotteSolidaire.Domain.Queries.Associations;
using CagnotteSolidaire.Domain.Tests.Mocks;
using Xunit;

namespace CagnotteSolidaire.Domain.Tests.Queries.Associations;

public class GetAssociationByRnaQueryTests
{
    [Fact]
    public async Task GetAssociationByRna_Existante_RetourneAssociation()
    {
        // Arrange
        var repo = new AssociationRepositoryMock();
        var association = new Association(
            Guid.NewGuid(),
            "Secours Populaire",
            "W987654321",
            "68"
        );
        repo.Seed(association);

        var handler = new GetAssociationByRnaQueryHandler(repo);
        var query = new GetAssociationByRnaQuery("W987654321");

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("W987654321", result!.NumeroRNA);
    }
}
