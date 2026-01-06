using MediatR;
using CagnotteSolidaire.Domain.Entities;
using CagnotteSolidaire.Domain.Repositories;
using CagnotteSolidaire.Domain.ValueObjects;


namespace CagnotteSolidaire.Domain.Commands.Utilisateurs;

public class InscrireGestionnaireCommandHandler
    : IRequestHandler<InscrireGestionnaireCommand, Guid>
{
    private readonly IUtilisateurRepository _utilisateurRepository;
    private readonly IAssociationRepository _associationRepository;

    public InscrireGestionnaireCommandHandler(
        IUtilisateurRepository utilisateurRepository,
        IAssociationRepository associationRepository)
    {
        _utilisateurRepository = utilisateurRepository;
        _associationRepository = associationRepository;
    }

    public async Task<Guid> Handle(
        InscrireGestionnaireCommand command,
        CancellationToken cancellationToken)
    {
        // 1️⃣ Email unique
        var existingUser =
            await _utilisateurRepository.GetByEmail(command.Email);

        if (existingUser != null)
            throw new InvalidOperationException("Email déjà utilisé");

        // 2️⃣ Association existante
        var association =
            await _associationRepository.GetById(command.AssociationId);

        if (association == null)
            throw new InvalidOperationException("Association inconnue");

        // 3️⃣ Création du gestionnaire
        var gestionnaire = new Gestionnaire(
            Guid.NewGuid(),
            command.Nom,
            command.Prenom,
            new Email(command.Email),
            association.Id
        );

        // 4️⃣ Sauvegarde
        await _utilisateurRepository.Add(gestionnaire);

        return gestionnaire.Id;
    }
}
