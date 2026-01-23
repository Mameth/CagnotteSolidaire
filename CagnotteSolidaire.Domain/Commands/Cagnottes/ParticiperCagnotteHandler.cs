using MediatR;
using CagnotteSolidaire.Domain.Entities;
using CagnotteSolidaire.Domain.Repositories;
using CagnotteSolidaire.Domain.ValueObjects;

namespace CagnotteSolidaire.Domain.Commands.Cagnottes;

public class ParticiperCagnotteHandler : IRequestHandler<ParticiperCagnotteCommand>
{
    private readonly ICagnotteRepository _cagnotteRepository;
    private readonly IUtilisateurRepository _utilisateurRepository;

    public ParticiperCagnotteHandler(
        ICagnotteRepository cagnotteRepository,
        IUtilisateurRepository utilisateurRepository)
    {
        _cagnotteRepository = cagnotteRepository;
        _utilisateurRepository = utilisateurRepository;
    }

    public async Task Handle(ParticiperCagnotteCommand request, CancellationToken cancellationToken)
    {
        // 1. Récupérer la cagnotte
        var cagnotte = await _cagnotteRepository.GetById(request.CagnotteId);
        if (cagnotte is null)
            throw new InvalidOperationException("Cagnotte introuvable.");

        // 2. Récupérer l'utilisateur
        var utilisateur = await _utilisateurRepository.GetById(request.ParticipantId);
        if (utilisateur is null)
            throw new InvalidOperationException("Utilisateur introuvable.");

        // 3. Vérifier que c'est bien un Participant (et pas un Gestionnaire)
        if (utilisateur is not Participant participant)
            throw new InvalidOperationException("Seul un participant peut faire un don.");

        // 4. Préparer le montant (Value Object Money)
        var montant = new Money(request.Montant);

        // 5. ACTION MÉTIER (DDD)
        // On utilise la méthode de l'entité Participant pour créer l'objet Participation
        var participation = participant.Participer(cagnotte, montant);

        // On ajoute cette participation à la cagnotte (ce qui met à jour le montant total)
        cagnotte.AjouterParticipation(participation);

        // 6. Sauvegarder les changements
        // Comme la participation est dans la liste de la cagnotte, un simple Update de la cagnotte suffit
        await _cagnotteRepository.Update(cagnotte);
    }
}