using GameDatabase.Data.Context;
using GameDatabase.Domain.AggregatesModel.GameAggregate;
using GameDatabase.Domain.AggregatesModel.GameAggregate.Interfaces;

namespace GameDatabase.Data.Repository;

public class DeveloperRepository : Repository<Developer>, IDeveloperRepository
{
    public DeveloperRepository(GameDatabaseContext context) : base(context)
    {
    }
}