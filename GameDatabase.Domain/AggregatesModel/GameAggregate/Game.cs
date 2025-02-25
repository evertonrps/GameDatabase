using GameDatabase.Domain.SeedWork;

namespace GameDatabase.Domain.AggregatesModel.GameAggregate;

public class Game : Entity<Game>
{
    protected Game()
    {
    }

    private Game(string title, string _description, int _DeveloperId)
    {
        Title = title;
        Description = _description;
        DeveloperId = _DeveloperId;
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public int DeveloperId { get; private set; }

    //EF
    public virtual Developer Developer { get; set; }
    public virtual ICollection<GamePlatform> GamePlatform { get; set; }

    public static Game Factory(string title, string description, int developerId)
    {
        var game = new Game(title, description, developerId);
        game.ValidateNow(new GameValidator(), game);
        return game;
    }

    //public virtual ICollection<Platform> Platform { get; set; }

    // public virtual ICollection<GamePlatform> GamePlatform { get; set; }
}