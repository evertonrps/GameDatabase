
using GameDatabase.Domain.AggregatesModel.GameAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameDatabase.Infra.Data.EntityConfigurations;

public class DeveloperEntityTypeConfiguration : IEntityTypeConfiguration<Developer>
{
    public void Configure(EntityTypeBuilder<Developer> builder)
    {   
        builder.ToTable("Developer");
    }
}

