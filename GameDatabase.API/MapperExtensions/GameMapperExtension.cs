using GameDatabase.API.ViewModels;
using GameDatabase.Domain.AggregatesModel.GameAggregate;

namespace GameDatabase.API.MapperExtensions;

public static class GameMapperExtension
{
    public static GameModel? ToViewModel(this Game? entity)
    {
        if (entity == null) return null;
        return new GameModel
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
        };
    }

    public static Game? ToEntity(this GameModel? model)
    {
        return model == null ? null : Game.Factory(model.Title, model.Description, model.DeveloperId);
    }

    public static IEnumerable<GameModel?>? ToViewModelList(this IEnumerable<Game?>? entities)
    {
        return entities?.Select(c => c.ToViewModel());
    }

    public static IEnumerable<Game?>? ToEntityList(this IEnumerable<GameModel?>? models)
    {
        return models?.Select(c => c.ToEntity());
    }
    
}