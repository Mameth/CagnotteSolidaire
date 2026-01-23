using MediatR;
using CagnotteSolidaire.Domain.Entities;
using CagnotteSolidaire.Domain.Repositories;

namespace CagnotteSolidaire.Domain.Queries.Cagnottes;

public class GetCagnotteByIdHandler : IRequestHandler<GetCagnotteByIdQuery, Cagnotte?>
{
    private readonly ICagnotteRepository _repository;

    public GetCagnotteByIdHandler(ICagnotteRepository repository)
    {
        _repository = repository;
    }

    public async Task<Cagnotte?> Handle(GetCagnotteByIdQuery request, CancellationToken cancellationToken)
    {
        // Ton repository utilise déjà .Include(c => c.Participations) dans GetById
        return await _repository.GetById(request.Id);
    }
}