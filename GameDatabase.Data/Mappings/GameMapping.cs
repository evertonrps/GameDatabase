using GameDatabase.Data.Extensions;
using GameDatabase.Domain.AggregatesModel.GameAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameDatabase.Data.Mappings;

public class GameMapping : EntityTypeConfiguration<Game>
{
    public override void Map(EntityTypeBuilder<Game> builder)
    {
        builder.Property(c => c.Title)
            .IsRequired()
            .HasMaxLength(150)
            .HasColumnType("varchar(150)");

        builder.Property(c => c.Description)
            .IsRequired(false)
            .HasMaxLength(150)
            .HasColumnType("varchar(200)");

        builder.HasOne(c => c.Developer)
            .WithMany(y => y.Games)
            .HasForeignKey(c => c.DeveloperId);

        builder.Ignore(e => e.Erros);

        builder.ToTable("Game");
    }
}