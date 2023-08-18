using GameDatabase.Domain.AggregatesModel.GameAggregate;

namespace GameDatabase.Domain.Interfaces.Services;

public interface IDeveloperService
{
    public Task<IEnumerable<Developer>> GetAll();
    public Task<Developer> CreateDeveloper(Developer developer);
    public Task<Developer> GetById(int id);
}