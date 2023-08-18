
using GameDatabase.Domain.AggregatesModel.GameAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameDatabase.Infra.Data.EntityConfigurations;

public class GamePlatformEntityTypeConfiguration : IEntityTypeConfiguration<GamePlatform>
{
    public void Configure(EntityTypeBuilder<GamePlatform> builder)
    {   
        builder.ToTable("GamePlatform");
    }
}

