using GameDatabase.Domain.SeedWork;

namespace GameDatabase.Domain.AggregatesModel.GameAggregate;

public class Platform : Entity<Platform>
{
    public Platform()
    {
    }

    private Platform(string description)
    {
        Description = description;
    }

    public string Description { get; private set; }

    //EF
    public virtual ICollection<GamePlatform> GamePlatform { get; set; }

    public static Platform Factory(string description, int platformTypeId)
    {
        var platform = new Platform(description);
        platform.ValidateNow(new PlatformValidator(), platform);
        return platform;
    }
}