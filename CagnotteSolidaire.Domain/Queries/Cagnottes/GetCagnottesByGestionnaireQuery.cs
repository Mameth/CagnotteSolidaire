using MediatR;
using CagnotteSolidaire.Domain.Entities;

namespace CagnotteSolidaire.Domain.Queries.Cagnottes;
public record GetCagnottesByGestionnaireQuery(Guid GestionnaireId) : IRequest<IEnumerable<Cagnotte>>;