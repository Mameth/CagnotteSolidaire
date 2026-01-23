using MediatR;

namespace CagnotteSolidaire.Domain.Commands.Utilisateurs;

public record InscrireParticipantCommand(
    string Nom,
    string Prenom,
    string Email,
    string MotDePasse
) : IRequest<Guid>;