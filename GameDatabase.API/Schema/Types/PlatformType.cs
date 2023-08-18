using GameDatabase.Domain.AggregatesModel.GameAggregate;

namespace GameDatabase.API.Schema.Types;

public class PlatformType : ObjectType<Platform>
{
    protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
    {
        descriptor.Ignore(f => f.Erros);
        base.Configure(descriptor);
    }
}