using MediatR;
using CagnotteSolidaire.Domain.Contracts;

namespace CagnotteSolidaire.Domain.Queries.Associations;

public record RechercherAssociationsQuery(
    string Terme
) : IRequest<IReadOnlyList<AssociationDTO>>;
