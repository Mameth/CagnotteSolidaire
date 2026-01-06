using MediatR;
using CagnotteSolidaire.Domain.Entities;
using CagnotteSolidaire.Domain.Repositories;

namespace CagnotteSolidaire.Domain.Queries.Utilisateurs;

public class GetUtilisateurByEmailQueryHandler
    : IRequestHandler<GetUtilisateurByEmailQuery, Utilisateur?>
{
    private readonly IUtilisateurRepository _repository;

    public GetUtilisateurByEmailQueryHandler(IUtilisateurRepository repository)
    {
        _repository = repository;
    }

    public Task<Utilisateur?> Handle(
        GetUtilisateurByEmailQuery query,
        CancellationToken cancellationToken)
    {
        return _repository.GetByEmail(query.Email);
    }
}
