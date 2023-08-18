using GameDatabase.Data.Context;
using GameDatabase.Domain.AggregatesModel.GameAggregate;
using GameDatabase.Domain.AggregatesModel.GameAggregate.Interfaces;

namespace GameDatabase.Data.Repository;

public class GamePlatformRespository: Repository<GamePlatform>, IGamePlatformRepository
{
    public GamePlatformRespository(GameDatabaseContext context) : base(context)
    {
    }
}