namespace ReservationSystem.Shared.Infrastructure.EntityConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Spaces.Domain;

public class SpaceConfiguration : IEntityTypeConfiguration<Space>
{
  public void Configure(EntityTypeBuilder<Space> builder)
  {
    builder.ToTable(nameof(ReservationSystemDbContext.Spaces));

    builder.HasKey(x => x.SpaceId);

    builder.Property(x => x.SpaceId)
      .HasConversion(
        id => id.Value,
        value => new SpaceId(value))
      .ValueGeneratedNever()
      .HasColumnName("space_id");

    builder.Property(x => x.Name)
      .HasConversion(
        name => name.Value,
        value => new SpaceName(value))
      .HasColumnName("name");

    builder.Property(x => x.Description)
      .HasConversion(
        description => description.Value,
        value => new SpaceDescription(value));

    builder.Property(x => x.Active)
      .HasColumnName("active");
  }
}
