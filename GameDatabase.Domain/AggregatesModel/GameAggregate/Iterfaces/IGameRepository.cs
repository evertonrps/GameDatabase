using GameDatabase.Domain.Interfaces.Repositories;

namespace GameDatabase.Domain.AggregatesModel.GameAggregate.Interfaces;

public interface IGameRepository : IRepository<Game>
{
    Task<IEnumerable<Game>> GetByDeveloperIdAsync(string developerId);
}