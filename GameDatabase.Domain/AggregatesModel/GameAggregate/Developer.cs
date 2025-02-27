using GameDatabase.Domain.SeedWork;

namespace GameDatabase.Domain.AggregatesModel.GameAggregate;

public class Developer : Entity<Developer>
{
    protected Developer()
    {
    }

    private Developer(string name, DateTime founded, string webSite)
    {
        Name = name;
        Founded = founded;
        WebSite = webSite;
    }

    public string Name { get; private set; }
    public DateTime Founded { get; set; }

    public string WebSite { get; private set; }

    public virtual ICollection<Game>? Games { get; set; }

    public bool ChangeWebSite(string newWebSite)
    {
        if (string.IsNullOrEmpty(newWebSite))
            return false;

        WebSite = newWebSite;
        return true;
    }

    public static Developer Factory(string name, DateTime founded, string webSite)
    {
        var developer = new Developer(name, founded, webSite);
        developer.ValidateNow(new DeveloperValidator(), developer);
        return developer;
    }
}