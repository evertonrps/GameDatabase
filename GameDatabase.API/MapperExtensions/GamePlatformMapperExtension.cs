using GameDatabase.API.ViewModels;
using GameDatabase.Domain.AggregatesModel.GameAggregate;

namespace GameDatabase.API.MapperExtensions;

public static class GamePlatformMapperExtension
{
    public static GamePlatformModel? ToViewModel(this GamePlatform? entity)
    {
        if (entity == null) return null;
        return new GamePlatformModel
        {
            GameId = entity.GameId,
            PlatformId = entity.PlatformId,
        };
    }

    public static GamePlatform? ToEntity(this GamePlatformModel? model)
    {
        return model == null ? null : new GamePlatform(model.GameId, model.PlatformId);
    }

    public static IEnumerable<GamePlatformModel?>? ToViewModelList(this IEnumerable<GamePlatform?>? entities)
    {
        return entities?.Select(c => c.ToViewModel());
    }

    public static IEnumerable<GamePlatform?>? ToEntityList(this IEnumerable<GamePlatformModel?>? models)
    {
        return models?.Select(c => c.ToEntity());
    }
}