using CagnotteSolidaire.Domain.Entities;

namespace CagnotteSolidaire.Domain.Services;

public interface ITokenService
{
    string GenerateToken(Utilisateur utilisateur);
}