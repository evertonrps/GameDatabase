using GameDatabase.API.Schema.Types;
using GameDatabase.Domain.AggregatesModel.GameAggregate;
using GameDatabase.Domain.Interfaces.Services;
using HotChocolate.Language;

namespace GameDatabase.API.Schema.Queries;

[ExtendObjectType(OperationType.Query)]
public class DeveloperQuery
{
    public async Task<IEnumerable<Developer>> GetDeveloperList([Service] IDeveloperService developerService)
    {
        var developers = await developerService.GetAll();
        if (developers.Any())
        {
            return developers;
        }

        return default;
    }
    
    public async Task<Developer> GetDeveloperById([Service] IDeveloperService developerService, int id)
    {
        return await developerService.GetById(id);
    }
}