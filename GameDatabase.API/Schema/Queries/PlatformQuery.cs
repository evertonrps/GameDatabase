using GameDatabase.Domain.AggregatesModel.GameAggregate;
using GameDatabase.Domain.Interfaces.Services;
using HotChocolate.Language;

namespace GameDatabase.API.Schema.Queries;

[ExtendObjectType(OperationType.Query)]
public class PlatformQuery
{
    public async Task<IEnumerable<Platform>> GetPlatformList([Service] IPlatformService platformService)
    {
        return await platformService.GetAll();
    }

    public async Task<Platform> GetPlatformById([Service] IPlatformService platformService, int id)
    {
        return await platformService.GetById(id);
    }
}