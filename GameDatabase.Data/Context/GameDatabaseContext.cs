using System.Data.Common;
using GameDatabase.Data.Extensions;
using GameDatabase.Data.Mappings;
using GameDatabase.Domain.AggregatesModel.GameAggregate;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace GameDatabase.Data.Context;

public class GameDatabaseContext : DbContext
{
    public DbSet<Game> Games { get; set; }
    public DbSet<Developer> Developers { get; set; }
    public DbSet<Platform> Platforms { get; set; }
    public DbSet<GamePlatform> GamePlatforms { get; set; }

    public GameDatabaseContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddConfiguration(new GameMapping());
        modelBuilder.AddConfiguration(new DeveloperMapping());
        modelBuilder.AddConfiguration(new PlatformMapping());
        modelBuilder.AddConfiguration(new GamePlatformMapping());
        // modelBuilder.AddConfiguration(new UsuarioMapping());
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