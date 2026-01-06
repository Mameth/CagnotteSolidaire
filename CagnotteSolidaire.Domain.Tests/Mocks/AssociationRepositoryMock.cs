using CagnotteSolidaire.Domain.Entities;
using CagnotteSolidaire.Domain.Repositories;

namespace CagnotteSolidaire.Domain.Tests.Mocks;

public class AssociationRepositoryMock : IAssociationRepository
{
    private readonly List<Association> _associations = new();

    public void Seed(Association association)
        => _associations.Add(association);

    public Task<Association?> GetById(Guid id)
        => Task.FromResult(_associations.FirstOrDefault(a => a.Id == id));

    public Task<Association?> GetByRna(string numeroRna)
        => Task.FromResult(_associations.FirstOrDefault(a => a.NumeroRNA == numeroRna));

    public Task Add(Association association)
    {
        _associations.Add(association);
        return Task.CompletedTask;
    }
}
