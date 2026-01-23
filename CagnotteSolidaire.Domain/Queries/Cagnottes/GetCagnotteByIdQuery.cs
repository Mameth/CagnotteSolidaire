using MediatR;
using CagnotteSolidaire.Domain.Entities;

namespace CagnotteSolidaire.Domain.Queries.Cagnottes;

public record GetCagnotteByIdQuery(Guid Id) : IRequest<Cagnotte?>;