using MediatR;

namespace CagnotteSolidaire.Domain.Commands.Cagnottes;

public record ParticiperCagnotteCommand(
    Guid CagnotteId,
    decimal Montant,
    Guid ParticipantId // L'ID de l'utilisateur connecté
) : IRequest;