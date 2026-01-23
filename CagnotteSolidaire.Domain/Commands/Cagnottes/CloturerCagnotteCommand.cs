using MediatR;

namespace CagnotteSolidaire.Domain.Commands.Cagnottes;

public record CloturerCagnotteCommand(Guid CagnotteId, Guid GestionnaireId) : IRequest;