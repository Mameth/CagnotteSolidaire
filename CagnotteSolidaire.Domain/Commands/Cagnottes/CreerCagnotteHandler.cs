using MediatR;
using CagnotteSolidaire.Domain.Entities;
using CagnotteSolidaire.Domain.Repositories;
using CagnotteSolidaire.Domain.ValueObjects;
using CagnotteSolidaire.Domain.Exceptions;

namespace CagnotteSolidaire.Domain.Commands.Cagnottes;

public class CreerCagnotteHandler : IRequestHandler<CreerCagnotteCommand, Guid>
{
    private readonly ICagnotteRepository _cagnotteRepository;
    private readonly IUtilisateurRepository _utilisateurRepository;

    public CreerCagnotteHandler(
        ICagnotteRepository cagnotteRepository,
        IUtilisateurRepository utilisateurRepository)
    {
        _cagnotteRepository = cagnotteRepository;
        _utilisateurRepository = utilisateurRepository;
    }

    public async Task<Guid> Handle(CreerCagnotteCommand request, CancellationToken cancellationToken)
    {
        // 1. Récupérer l'utilisateur
        var utilisateur = await _utilisateurRepository.GetById(request.CreateurId);
        
        if (utilisateur is null)
             throw new InvalidOperationException("Utilisateur inconnu.");

        // 2. Vérifier si c'est un gestionnaire (Cast)
        if (utilisateur is not Gestionnaire gestionnaire)
        {
             throw new InvalidOperationException("Seul un gestionnaire peut créer une cagnotte.");
        }

        // 3. Préparer les données
        var objectif = new Money(request.Objectif); // Utilisation du constructeur simple

        // 4. Créer la Cagnotte
        var nouvelleCagnotte = new Cagnotte(
            Guid.NewGuid(),             // Id
            gestionnaire.AssociationId, // AssociationId
            request.Nom,                // Nom
            request.Description,        // Description
            objectif                    // Objectif
        );

        // 5. Sauvegarder
        await _cagnotteRepository.Add(nouvelleCagnotte);

        return nouvelleCagnotte.Id;
    }
}