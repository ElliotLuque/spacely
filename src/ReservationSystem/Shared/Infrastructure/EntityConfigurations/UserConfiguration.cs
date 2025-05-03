namespace ReservationSystem.Shared.Infrastructure.EntityConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Users.Domain;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.ToTable(nameof(ReservationSystemDbContext.Users));

    builder.HasKey(x => x.UserId);

    builder.Property(x => x.UserId)
      .HasConversion(
        id => id.Value,
        value => new UserId(value))
      .ValueGeneratedNever()
      .HasColumnName("user_id");

    builder.Property(x => x.Name)
      .HasConversion(
        name => name.Value,
        value => new UserName(value))
      .HasColumnName("name");

    builder.Property(x => x.Email)
      .HasConversion(
        email => email.Value,
        value => new UserEmail(value))
      .HasColumnName("email");

    builder.Property(x => x.Active)
      .HasColumnName("active");
  }
}
