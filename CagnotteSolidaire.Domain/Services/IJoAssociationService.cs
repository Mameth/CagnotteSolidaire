using CagnotteSolidaire.Domain.Entities;

namespace CagnotteSolidaire.Domain.Services;

public interface IJoAssociationService
{
    Task<IReadOnlyList<Association>> Rechercher(
        string terme,
        string departement);
}
