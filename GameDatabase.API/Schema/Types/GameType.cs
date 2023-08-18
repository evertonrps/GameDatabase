using GameDatabase.Domain.AggregatesModel.GameAggregate;

namespace GameDatabase.API.Schema.Types;

public class GameType : ObjectType<Game>
{
    protected override void Configure(IObjectTypeDescriptor<Game> descriptor)
    {
        descriptor.Ignore(f => f.Erros);
        base.Configure(descriptor);
    }
}