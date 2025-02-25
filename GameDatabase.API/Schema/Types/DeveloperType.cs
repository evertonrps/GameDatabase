using GameDatabase.Domain.AggregatesModel.GameAggregate;

namespace GameDatabase.API.Schema.Types;

public class DeveloperType : ObjectType<Developer>
{
    protected override void Configure(IObjectTypeDescriptor<Developer> descriptor)
    {
        descriptor.Ignore(f => f.Erros);
        base.Configure(descriptor);
    }
}