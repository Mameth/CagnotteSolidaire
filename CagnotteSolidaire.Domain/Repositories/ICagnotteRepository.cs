using CagnotteSolidaire.Domain.Entities;

namespace CagnotteSolidaire.Domain.Repositories;

public interface ICagnotteRepository
{
    Task<Cagnotte?> GetById(Guid id);
    
    Task Add(Cagnotte cagnotte);
    
    Task Update(Cagnotte cagnotte);
    
    Task<IEnumerable<Cagnotte>> GetByGestionnaireId(Guid gestionnaireId);
    Task<IEnumerable<Cagnotte>> GetAll();
}