using GameDatabase.Data.Context;
using GameDatabase.Domain.AggregatesModel.GameAggregate;
using GameDatabase.Domain.AggregatesModel.GameAggregate.Interfaces;

namespace GameDatabase.Data.Repository;

public class PlatformRepository : Repository<Platform>, IPlatformRepository
{
    public PlatformRepository(GameDatabaseContext context) : base(context)
    {
    }
}