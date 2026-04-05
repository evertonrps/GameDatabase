using GameDatabase.Domain.SeedWork;

namespace GameDatabase.Domain.AggregatesModel.GameAggregate;

public class Platform : Entity<Platform>
{
    public Platform()
    {
    }

    private Platform(string description, string id)
    {
        Id = id;
        Description = description;
    }

    public string Description { get; private set; }

    //EF
    public virtual ICollection<GamePlatform> GamePlatform { get; set; }

    public static Platform Factory(string description, string? id = null)
    {
        if (id == null)
            id = Guid.NewGuid().ToString();
        var platform = new Platform(description, id);
        platform.ValidateNow(new PlatformValidator(), platform);
        return platform;
    }
}