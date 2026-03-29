using GameDatabase.API.ViewModels;
using GameDatabase.Domain.AggregatesModel.GameAggregate;

namespace GameDatabase.API.MapperExtensions;

public static class PlatformMapperExtension
{
    public static PlatformModel? ToViewModel(this Platform? entity)
    {
        if (entity == null) return null;
        return new PlatformModel
        {
            Id =  entity.Id,
            Description =  entity.Description,
        };
    }

    public static Platform? ToEntity(this PlatformModel? model)
    {
        return model == null ? null :  Platform.Factory(model.Description);
    }

    public static IEnumerable<PlatformModel?>? ToViewModelList(this IEnumerable<Platform?>? entities)
    {
        return entities?.Select(c => c.ToViewModel());
    }

    public static IEnumerable<Platform?>? ToEntityList(this IEnumerable<PlatformModel?>? models)
    {
        return models?.Select(c => c.ToEntity());
    }
}