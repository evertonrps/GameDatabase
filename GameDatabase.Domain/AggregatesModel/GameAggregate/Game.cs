using GameDatabase.Domain.SeedWork;
using System.Collections.Generic;

namespace GameDatabase.Domain.AggregatesModel.GameAggregate;

public class Game : Entity<Game>
{
    protected Game()
    {
        GamePlatform = new List<GamePlatform>();
    }

    private Game(string title, string description, int developerId) : this()
    {
        Title = title;
        Description = description;
        DeveloperId = developerId;
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
}