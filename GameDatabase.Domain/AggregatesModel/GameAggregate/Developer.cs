using System;
using System.Collections.Generic;
using GameDatabase.Domain.SeedWork;

namespace GameDatabase.Domain.AggregatesModel.GameAggregate;

public class Developer : Entity<Developer>
{
    public string Name { get; private set; }
    public DateTime Founded { get; set; }

    public string WebSite { get; private set; }

    public virtual ICollection<Game>? Games { get; set; }

    protected Developer()
    {
    }

    private Developer(string name, DateTime founded, string webSite)
    {
        Name = name;
        Founded = founded;
        WebSite = webSite;
    }

    public static Developer Factory(string name, DateTime founded, string webSite)
    {
        var developer = new Developer(name, founded, webSite);
        developer.ValidateNow(new DeveloperValidator(), developer);
        return developer;
    }

}

