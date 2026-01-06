using CagnotteSolidaire.Domain.Entities;
using CagnotteSolidaire.Domain.Repositories;
using CagnotteSolidaire.Domain.ValueObjects;
using MediatR;

namespace CagnotteSolidaire.Domain.Commands.Utilisateurs;

public class InscrireParticipantHandler
    : IRequestHandler<InscrireParticipantCommand, Guid>
{
    private readonly IUtilisateurRepository _utilisateurRepository;

    public InscrireParticipantHandler(IUtilisateurRepository utilisateurRepository)
    {
        _utilisateurRepository = utilisateurRepository;
    }

    public async Task<Guid> Handle(
        InscrireParticipantCommand request,
        CancellationToken cancellationToken)
    {
        var email = new Email(request.Email);

        var existant = await _utilisateurRepository.GetByEmail(email.Value);
        if (existant != null)
            throw new InvalidOperationException("Un utilisateur avec cet email existe déjà.");

        var participant = new Participant(
            Guid.NewGuid(),
            request.Nom,
            request.Prenom,
            email);

        await _utilisateurRepository.Add(participant);

        return participant.Id;
    }
}
