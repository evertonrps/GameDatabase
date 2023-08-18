using GameDatabase.Data.Context;
using GameDatabase.Data.Repository;
using GameDatabase.Domain.AggregatesModel.GameAggregate.Interfaces;
using GameDatabase.Domain.Interfaces.Services;
using GameDatabase.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace GameDatabase.IoC;

public static class BootStrapper
{
    public static void ConfigureServices(IServiceCollection services)
    {
        //Serilog
        LogConfiguration.CreateLogger();
        services.AddLogging(builder => { builder.AddSerilog(); });
        
        //DbContext
        services.AddDbContext<GameDatabaseContext>();
        
        //Services
        services.AddScoped(typeof(IDeveloperService), typeof(DeveloperService));
        services.AddScoped(typeof(IGameService), typeof(GameService));
        services.AddScoped(typeof(IPlatformService), typeof(PlatformService));
        services.AddScoped(typeof(IGamePlatformService), typeof(GamePlatformService));
        
        //Repositories
        services.AddScoped(typeof(IGameRepository), typeof(GameRepository));
        services.AddScoped(typeof(IDeveloperRepository), typeof(DeveloperRepository));
        services.AddScoped(typeof(IPlatformRepository), typeof(PlatformRepository));
        services.AddScoped(typeof(IGamePlatformRepository), typeof(GamePlatformRespository));
    }
}