using GameDatabase.Domain.AggregatesModel.GameAggregate;

namespace GameDatabase.Domain.Interfaces.Services;

public interface IGamePlatformService
{
    public Task<IEnumerable<GamePlatform>> GetAll();
    public Task<bool> CreateGamePlatform(GamePlatform gamePlatform);
    public Task<GamePlatform> GetById(int id);
}