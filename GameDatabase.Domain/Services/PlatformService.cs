using GameDatabase.Domain.AggregatesModel.GameAggregate;
using GameDatabase.Domain.AggregatesModel.GameAggregate.Interfaces;
using GameDatabase.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace GameDatabase.Domain.Services;

public class PlatformService : IPlatformService
{
    private readonly IPlatformRepository _repository;
    private readonly ILogger<PlatformService> _logger;

    public PlatformService(IPlatformRepository repository, ILogger<PlatformService> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public Task<IEnumerable<Platform>> GetAll()
    {
        return _repository.GetAll();
    }

    public async Task<bool> CreatePlatform(Platform platform)
    {
        try
        {
            _repository.Add(platform);
            _repository.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Falha ao inserir novo game");
            return false;
        }
    }

    public async Task<Platform> GetById(int id)
    {
        try
        {
            return  await _repository.GetById(id);
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Falha ao buscar game");
            return default;
        }
    }
}