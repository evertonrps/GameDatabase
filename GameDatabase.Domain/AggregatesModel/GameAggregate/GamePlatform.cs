using GameDatabase.Domain.SeedWork;

namespace GameDatabase.Domain.AggregatesModel.GameAggregate;

public class GamePlatform : Entity<GamePlatform>
{
    protected GamePlatform()
    {
    }

    public GamePlatform(int gameID, int platformID)
    {
        GameId = gameID;
        PlatformId = platformID;
    }

    public int GameId { get; set; }

    public int PlatformId { get; set; }

    //EF
    public virtual Game Game { get; set; }

    public virtual Platform Platform { get; set; }
}