using MediatR;
using CagnotteSolidaire.Domain.Contracts;
using CagnotteSolidaire.Domain.Entities;
using CagnotteSolidaire.Domain.Services;

namespace CagnotteSolidaire.Domain.Queries.Associations;

public class RechercherAssociationsQueryHandler
    : IRequestHandler<RechercherAssociationsQuery, IReadOnlyList<AssociationDTO>>
{
    private readonly IJoAssociationService _joService;

    public RechercherAssociationsQueryHandler(
        IJoAssociationService joService)
    {
        _joService = joService;
    }

    public async Task<IReadOnlyList<AssociationDTO>> Handle(
        RechercherAssociationsQuery query,
        CancellationToken cancellationToken)
    {
        var associations =
            await _joService.Rechercher(query.Terme, "68");

        return associations
            .Select(a => new AssociationDTO(
                a.Id,
                a.Nom,
                a.NumeroRNA
            ))
            .ToList();
    }
}


