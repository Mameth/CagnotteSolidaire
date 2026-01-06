using MediatR;
using CagnotteSolidaire.Domain.Entities;

namespace CagnotteSolidaire.Domain.Queries.Associations;

public record GetAssociationByRnaQuery(string NumeroRna)
    : IRequest<Association?>;
