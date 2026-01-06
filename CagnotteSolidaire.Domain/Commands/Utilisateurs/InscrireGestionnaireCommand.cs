using MediatR;

namespace CagnotteSolidaire.Domain.Commands.Utilisateurs;

public record InscrireGestionnaireCommand(
    string Nom,
    string Prenom,
    string Email,
    Guid AssociationId
) : IRequest<Guid>;
