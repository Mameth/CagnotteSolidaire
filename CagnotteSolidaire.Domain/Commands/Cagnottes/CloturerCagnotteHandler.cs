using MediatR;
using CagnotteSolidaire.Domain.Repositories;
using CagnotteSolidaire.Domain.Services;
using CagnotteSolidaire.Domain.Enums;

namespace CagnotteSolidaire.Domain.Commands.Cagnottes;

public class CloturerCagnotteHandler : IRequestHandler<CloturerCagnotteCommand>
{
    private readonly ICagnotteRepository _cagnotteRepository;
    private readonly IEmailService _emailService;

    public CloturerCagnotteHandler(ICagnotteRepository cagnotteRepository, IEmailService emailService)
    {
        _cagnotteRepository = cagnotteRepository;
        _emailService = emailService;
    }

    public async Task Handle(CloturerCagnotteCommand request, CancellationToken cancellationToken)
    {
        // 1. Récupération
        var cagnotte = await _cagnotteRepository.GetById(request.CagnotteId);
        if (cagnotte is null)
            throw new KeyNotFoundException("Cagnotte introuvable.");

        // 2. Action Métier
        cagnotte.Cloturer();

        // 3. Notification (Simulation Email )
        foreach (var participation in cagnotte.Participations)
        {
            string sujet = cagnotte.Statut == StatutCagnotte.Cloturee 
                ? "Objectif atteint !" 
                : "Cagnotte annulée";
            
            // On simule l'envoi à l'ID du participant faute d'avoir chargé l'utilisateur complet
            await _emailService.EnvoyerEmail(participation.ParticipantId.ToString(), sujet, $"La cagnotte {cagnotte.Nom} est terminée.");
        }

        // 4. Sauvegarde
        await _cagnotteRepository.Update(cagnotte);
    }
}