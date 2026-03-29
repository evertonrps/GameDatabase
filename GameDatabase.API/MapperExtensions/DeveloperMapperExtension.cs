using GameDatabase.API.ViewModels;
using GameDatabase.Domain.AggregatesModel.GameAggregate;

namespace GameDatabase.API.MapperExtensions;

public static class DeveloperMapperExtension
{
    public static DeveloperOutputModel? ToViewModel(this Developer? entity)
    {
        if (entity == null) return null;
        return new DeveloperOutputModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Founded =  entity.Founded,
            WebSite =  entity.WebSite,
        };
    }

    public static Developer? ToEntity(this DeveloperInputModel? model)
    {
        return model == null ? null : Developer.Factory(model.Name, model.Founded, model.WebSite);
    }

    public static IEnumerable<DeveloperOutputModel?>? ToViewModelList(this IEnumerable<Developer?>? entities)
    {
        return entities?.Select(c => c.ToViewModel());
    }

    public static IEnumerable<Developer?>? ToEntityList(this IEnumerable<DeveloperInputModel?>? models)
    {
        return models?.Select(c => c.ToEntity());
    }
    
}