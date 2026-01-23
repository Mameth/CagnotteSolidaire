using MediatR;

namespace CagnotteSolidaire.Domain.Commands.Cagnottes;

public record CreerCagnotteCommand(
    string Nom,
    string Description,
    decimal Objectif,
    Guid CreateurId // L'ID du gestionnaire connecté
) : IRequest<Guid>;