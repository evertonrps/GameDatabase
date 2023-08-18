using GameDatabase.Data.Extensions;
using GameDatabase.Domain.AggregatesModel.GameAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameDatabase.Data.Mappings;

public class PlatformMapping : EntityTypeConfiguration<Platform>
{
    public override void Map(EntityTypeBuilder<Platform> builder)
    {
        builder.Property(c => c.Description).HasColumnType("varchar(150)").IsRequired();

        builder.Ignore(e => e.Erros);

        builder.Ignore(e => e.GamePlatform);

        builder.ToTable("Platform");
    }
}