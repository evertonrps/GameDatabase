using GameDatabase.Domain.AggregatesModel.GameAggregate;

namespace GameDatabase.Domain.Interfaces.Services;

public interface IGameService
{
    public Task<IEnumerable<Game>> GetAll();
    public Task<bool> CreateGame(Game developer);
    public Task<Game> GetById(int id);
}