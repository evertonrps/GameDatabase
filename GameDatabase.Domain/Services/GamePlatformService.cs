using GameDatabase.Domain.AggregatesModel.GameAggregate;
using GameDatabase.Domain.AggregatesModel.GameAggregate.Interfaces;
using GameDatabase.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace GameDatabase.Domain.Services;

public class GamePlatformService : IGamePlatformService
{
    private readonly IGamePlatformRepository _repository;
    private readonly ILogger<GameService> _logger;

    public GamePlatformService(IGamePlatformRepository repository, ILogger<GameService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<GamePlatform>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<bool> CreateGamePlatform(GamePlatform gamePlatform)
    {
        try
        {
           await _repository.Add(gamePlatform);
           await _repository.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Falha ao inserir novo game");
            return false;
        }
    }

    public async Task<GamePlatform> GetById(int id)
    {
        try
        {
            return await _repository.GetById(id);
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Falha ao buscar game platform");
            return default;
        }
    }
}

