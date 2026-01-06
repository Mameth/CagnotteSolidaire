using MediatR;
using CagnotteSolidaire.Domain.Entities;
using CagnotteSolidaire.Domain.Repositories;

namespace CagnotteSolidaire.Domain.Queries.Associations;

public class GetAssociationByRnaQueryHandler
    : IRequestHandler<GetAssociationByRnaQuery, Association?>
{
    private readonly IAssociationRepository _repository;

    public GetAssociationByRnaQueryHandler(IAssociationRepository repository)
    {
        _repository = repository;
    }

    public Task<Association?> Handle(
        GetAssociationByRnaQuery query,
        CancellationToken cancellationToken)
    {
        return _repository.GetByRna(query.NumeroRna);
    }
}
