using GameDatabase.Domain.SeedWork;

namespace GameDatabase.Domain.AggregatesModel.GameAggregate;

public class GamePlatform : Entity<GamePlatform>
{
    protected GamePlatform()
    {
    }

    public GamePlatform(string gameID, string platformID)
    {
        GameId = gameID;
        PlatformId = platformID;
    }

    public string GameId { get; set; }

    public string PlatformId { get; set; }

    //EF
    public virtual Game Game { get; set; }

    public virtual Platform Platform { get; set; }
}