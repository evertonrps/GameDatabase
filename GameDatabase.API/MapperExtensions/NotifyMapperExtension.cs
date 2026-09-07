using GameDatabase.API.ViewModels;
using GameDatabase.Domain.SeedWork;

namespace GameDatabase.API.MapperExtensions;

public static class NotifyMapperExtension
{
    public static NotifyModel? ToViewModel(this NotifyMessage? entity)
    {
        if (entity == null) return null;
        return new NotifyModel
        {
            Id = entity.Id,
            Message = entity.Message
        };
    }

    public static NotifyMessage? ToEntity(this NotifyModel? model)
    {
        return model == null ? null : new NotifyMessage(model.Id, "default", model.Message);
    }

    public static IEnumerable<NotifyModel?>? ToViewModelList(this IEnumerable<NotifyMessage?>? entities)
    {
        return entities?.Select(c => c.ToViewModel());
    }

    public static IEnumerable<NotifyMessage?>? ToEntityList(this IEnumerable<NotifyModel?>? models)
    {
        return models?.Select(c => c.ToEntity());
    }
}