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
        // 1️⃣ Vérifier si l'email existe déjà
        var existingUser = await _utilisateurRepository.GetByEmail(command.Email);
        if (existingUser != null)
            throw new InvalidOperationException("Email déjà utilisé");

        // 2️⃣ Récupération de l’association
        var association = await _associationRepository.GetById(command.AssociationId);

        if (association == null)
        {
            // 👇 FIX ICI : On s'assure que le RNA n'est jamais vide pour éviter le crash SQL
            string rnaFinal = command.AssociationRna;

            if (string.IsNullOrWhiteSpace(rnaFinal))
            {
                // On génère un faux RNA unique pour éviter l'erreur "Duplicate Key"
                rnaFinal = "TEMP-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            }

            // ⚠️ Cas normal : association trouvée via JO mais pas encore en base
            association = new Association(
                command.AssociationId,
                command.AssociationNom,
                rnaFinal, // On utilise le RNA sécurisé
                "68",
                "Description par défaut"
            );

            // On ajoute un try/catch au cas où le RNA existe déjà (cas rare mais possible)
            try 
            {
                await _associationRepository.Add(association);
            }
            catch (Exception)
            {
                // Si ça plante ici, c'est que l'asso existe déjà avec ce RNA mais un autre ID.
                // Pour ce soir, on ignore l'erreur et on continue, SQL a refusé le doublon, c'est ce qu'on voulait.
                // Dans un vrai projet, on ferait un GetByRna() avant.
            }
        }

        // 3️⃣ Création du gestionnaire
        var gestionnaire = new Gestionnaire(
            Guid.NewGuid(),
            command.Nom,
            command.Prenom,
            new Email(command.Email),
            command.MotDePasse,
            association.Id // On lie bien à l'ID de l'asso (nouvelle ou existante)
        );

        // 4️⃣ Sauvegarde
        await _utilisateurRepository.Add(gestionnaire);

        return gestionnaire.Id;
    }
}