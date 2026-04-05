using GameDatabase.Domain.SeedWork;

namespace GameDatabase.Domain.AggregatesModel.GameAggregate;

public class Game : Entity<Game>
{
    protected Game()
    {
        GamePlatform = new List<GamePlatform>();
    }

    private Game(string title, string description, string developerId, string id) : this()
    {
        Id = id;
        Title = title;
        Description = description;
        DeveloperId = developerId;
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public string DeveloperId { get; private set; }

    //EF
    public virtual Developer Developer { get; set; }
    public virtual ICollection<GamePlatform> GamePlatform { get; set; }

    public static Game Factory(string title, string description, string developerId, string? id = null)
    {
        if (id == null)
            id = Guid.NewGuid().ToString();
        var game = new Game(title, description, developerId, id);
        game.ValidateNow(new GameValidator(), game);
        return game;
    }
}