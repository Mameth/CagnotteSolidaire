using MediatR;
using CagnotteSolidaire.Domain.Entities;
using CagnotteSolidaire.Domain.Repositories;

namespace CagnotteSolidaire.Domain.Queries.Cagnottes;

public class GetCagnottesByGestionnaireHandler : IRequestHandler<GetCagnottesByGestionnaireQuery, IEnumerable<Cagnotte>>
{
    private readonly ICagnotteRepository _repository;

    public GetCagnottesByGestionnaireHandler(ICagnotteRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Cagnotte>> Handle(GetCagnottesByGestionnaireQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByGestionnaireId(request.GestionnaireId);
    }
}