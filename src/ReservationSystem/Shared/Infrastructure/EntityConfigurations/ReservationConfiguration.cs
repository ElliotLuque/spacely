namespace ReservationSystem.Shared.Infrastructure.EntityConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Reservations.Domain;

using Spaces.Domain;

using Users.Domain;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
  public void Configure(EntityTypeBuilder<Reservation> builder)
  {
    builder.ToTable(nameof(ReservationSystemDbContext.Reservations));

    builder.HasKey(x => x.ReservationId);

    builder.Property(x => x.ReservationId)
      .HasConversion(
        id => id.Value,
        value => new ReservationId(value))
      .ValueGeneratedNever()
      .HasColumnName("reservation_id");

    builder.Property(x => x.UserId)
      .HasConversion(
        id => id.Value,
        value => new UserId(value))
      .HasColumnName("user_id");

    builder.Property(x => x.SpaceId)
      .HasConversion(
        id => id.Value,
        value => new SpaceId(value))
      .HasColumnName("space_id");

    builder.Property(x => x.Status)
      .HasConversion(
        status => status.Value,
        value => ReservationStatus.From(value))
      .HasColumnName("status");

    builder.OwnsOne(x => x.DateRange, dr =>
    {
      dr.Property(p => p.Start)
        .HasColumnName("start_date")
        .IsRequired();

      dr.Property(p => p.End)
        .HasColumnName("end_date")
        .IsRequired();
    });
  }
}
