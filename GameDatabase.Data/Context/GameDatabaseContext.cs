using System.Data.Common;
using GameDatabase.Data.Extensions;
using GameDatabase.Data.Mappings;
using GameDatabase.Domain.AggregatesModel.GameAggregate;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace GameDatabase.Data.Context;

public class GameDatabaseContext : DbContext
{
    public GameDatabaseContext()
    {
        Database.EnsureCreated();
    }

    public DbSet<Game> Games { get; set; }
    public DbSet<Developer> Developers { get; set; }
    public DbSet<Platform> Platforms { get; set; }
    public DbSet<GamePlatform> GamePlatforms { get; set; }

    private Developer[] SeedDevelopers()
    {
        var developers = new[]
        {
            Developer.Factory("Nintendo", new DateTime(1889, 09, 23), "https://www.nintendo.com/",
                "2f7d2d06-ca4b-4e7c-872d-45aec9c36257"),
            Developer.Factory("SEGA", new DateTime(1960, 06, 03), "https://www.sega.com/",
                "a946ccc3-b868-43d9-8298-732a51afa6be"),
            Developer.Factory("Acclaim", new DateTime(1987, 01, 05), "https://www.acclaim.com/",
                "8036fa1f-c520-4de2-8566-d5501ae825d9")
        };
        return developers;
    }

    private Platform[] SeedPlatforms()
    {
        var platforms = new[]
        {
            Platform.Factory("Super Nintendo", "f33800b5-f2e1-4b6d-8dcc-4b2e8905f622"),
            Platform.Factory("Mega Drive", "c96873fa-15d7-475c-a442-056ac0e661b0")
        };
        return platforms;
    }

    private Game[] SeedGames()
    {
        var games = new[]
        {
            Game.Factory("Super Mario Bros 3", "Jogo de aventura do Mario", "2f7d2d06-ca4b-4e7c-872d-45aec9c36257",
                "8036fa1f-c520-4de2-8566-d5501ae825d9"),
            Game.Factory("Sonic II", "Jogo de aventura do Sonic", "a946ccc3-b868-43d9-8298-732a51afa6be",
                "d0d749eb-3dc0-48aa-ba9a-61abe1130c8a"),
            Game.Factory("Mortal Kombat II", "Jogo de luta", "8036fa1f-c520-4de2-8566-d5501ae825d9",
                "e6ca6f78-1b3a-4e74-b070-a8f1e6759950")
        };
        return games;
    }

    private GamePlatform[] SeedGamePlatforms()
    {
        var gamePlatforms = new[]
        {
            new GamePlatform("8036fa1f-c520-4de2-8566-d5501ae825d9", "f33800b5-f2e1-4b6d-8dcc-4b2e8905f622"),
            new GamePlatform("d0d749eb-3dc0-48aa-ba9a-61abe1130c8a", "c96873fa-15d7-475c-a442-056ac0e661b0"),
            new GamePlatform("e6ca6f78-1b3a-4e74-b070-a8f1e6759950", "f33800b5-f2e1-4b6d-8dcc-4b2e8905f622"),
            new GamePlatform("e6ca6f78-1b3a-4e74-b070-a8f1e6759950", "c96873fa-15d7-475c-a442-056ac0e661b0")
        };
        return gamePlatforms;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddConfiguration(new GameMapping());
        modelBuilder.AddConfiguration(new DeveloperMapping());
        modelBuilder.AddConfiguration(new PlatformMapping());
        modelBuilder.AddConfiguration(new GamePlatformMapping());
        // modelBuilder.AddConfiguration(new UsuarioMapping());
        //Seed
        modelBuilder.Entity<Developer>().HasData(SeedDevelopers());
        modelBuilder.Entity<Platform>().HasData(SeedPlatforms());
        modelBuilder.Entity<Game>().HasData(SeedGames());
        modelBuilder.Entity<GamePlatform>().HasData(SeedGamePlatforms());
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
        optionsBuilder.UseSqlite(CreateInMemoryDatabase());
        base.OnConfiguring(optionsBuilder);
    }

    private static DbConnection CreateInMemoryDatabase()
    {
        var connection = new SqliteConnection("DataSource=file::memory:?cache=shared");

        connection.Open();

        return connection;
    }
}