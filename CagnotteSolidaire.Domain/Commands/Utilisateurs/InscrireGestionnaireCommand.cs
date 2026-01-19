using MediatR;

namespace CagnotteSolidaire.Domain.Commands.Utilisateurs;


public record InscrireGestionnaireCommand(
    string Nom,
    string Prenom,
    string Email,
    Guid AssociationId,
    string AssociationNom,
    string AssociationRna
) : IRequest<Guid>;
