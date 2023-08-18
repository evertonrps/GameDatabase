using GameDatabase.Domain.AggregatesModel.GameAggregate;
using GameDatabase.Domain.Interfaces.Services;
using HotChocolate.Language;

namespace GameDatabase.API.Schema.Queries;

[ExtendObjectType(OperationType.Query)]
public class GameQuery
{
    public async Task<IEnumerable<Game>> GetGameList([Service] IGameService gameService)
    {
        return await gameService.GetAll();
    }
    
    public async Task<Game> GetGameById([Service] IGameService gameService, int id)
    {
        return await gameService.GetById(id);
    }
}