using MediatR;
using CagnotteSolidaire.Domain.Entities;

namespace CagnotteSolidaire.Domain.Queries.Utilisateurs;

public record GetUtilisateurByEmailQuery(string Email)
    : IRequest<Utilisateur?>;
