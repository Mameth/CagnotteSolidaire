using CagnotteSolidaire.Domain.Commands.Utilisateurs;
using CagnotteSolidaire.Domain.Entities;
using CagnotteSolidaire.Domain.Tests.Mocks;
using Xunit;

namespace CagnotteSolidaire.Domain.Tests;

public class InscrireGestionnaireHandlerTests
{
    [Fact]
    public async Task InscrireGestionnaire_Valide_CreeGestionnaire()
    {
        // Arrange
        var userRepo = new UtilisateurRepositoryMock();
        var assocRepo = new AssociationRepositoryMock();

        var association = new Association(
            Guid.NewGuid(),
            "Croix Rouge",
            "W123456789",
            "68"
        );
        assocRepo.Seed(association);

        var handler = new InscrireGestionnaireCommandHandler(
            userRepo,
            assocRepo);

        var command = new InscrireGestionnaireCommand(
            Nom: "Durand",
            Prenom: "Alice",
            Email: "alice@asso.fr",
            AssociationId: association.Id,
            AssociationNom: association.Nom,
            AssociationRna: association.NumeroRNA
        );

        // Act
        var id = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotEqual(Guid.Empty, id);
    }

    [Fact]
    public async Task InscrireGestionnaire_AssociationInconnue_CreeAssociationPuisGestionnaire()
    {
        // Arrange
        var userRepo = new UtilisateurRepositoryMock();
        var assocRepo = new AssociationRepositoryMock();

        var handler = new InscrireGestionnaireCommandHandler(
            userRepo,
            assocRepo);

        var command = new InscrireGestionnaireCommand(
            Nom: "Durand",
            Prenom: "Alice",
            Email: "alice@asso.fr",
            AssociationId: Guid.NewGuid(),
            AssociationNom: "Association Inconnue",
            AssociationRna: "W999999999"
        );

        // Act
        var id = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotEqual(Guid.Empty, id);
    }
}
