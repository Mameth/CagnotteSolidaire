using CagnotteSolidaire.Domain.Entities;
namespace CagnotteSolidaire.Domain.Repositories;

public interface IAssociationRepository
{
    Task<Association?> GetById(Guid id);
    Task<Association?> GetByRna(string numeroRna);
    Task Add(Association association);
}
