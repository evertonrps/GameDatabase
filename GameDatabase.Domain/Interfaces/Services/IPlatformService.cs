using GameDatabase.Domain.AggregatesModel.GameAggregate;

namespace GameDatabase.Domain.Interfaces.Services;

public interface IPlatformService
{
    public Task<IEnumerable<Platform>> GetAll();
    public Task<bool> CreatePlatform(Platform platform);
    public Task<Platform> GetById(int id);
}