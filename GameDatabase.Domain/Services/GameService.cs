using GameDatabase.Domain.AggregatesModel.GameAggregate;
using GameDatabase.Domain.AggregatesModel.GameAggregate.Interfaces;
using GameDatabase.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace GameDatabase.Domain.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _repository;
    private readonly ILogger<GameService> _logger;

    public GameService(IGameRepository repository, ILogger<GameService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public Task<IEnumerable<Game>> GetAll()
    {
        return _repository.GetAll();
    }

    public async Task<bool> CreateGame(Game game)
    {
        try
        {
            _repository.Add(game);
            _repository.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Falha ao inserir novo game");
            return false;
        }
    }

    public async Task<Game> GetById(int id)
    {
        try
        {
            return await  _repository.GetById(id);
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Falha ao buscar game");
            return default;
        }
    }
}