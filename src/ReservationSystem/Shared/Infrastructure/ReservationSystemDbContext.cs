namespace ReservationSystem.Shared.Infrastructure;

using EntityConfigurations;

using Microsoft.EntityFrameworkCore;

using Reservations.Domain;

using Spaces.Domain;

using Users.Domain;

public class ReservationSystemDbContext : DbContext
{
  public DbSet<Reservation> Reservations { get; set; }
  public DbSet<User> Users { get; set; }
  public DbSet<Space> Spaces { get; set; }

  public ReservationSystemDbContext(DbContextOptions<ReservationSystemDbContext> options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfiguration(new ReservationConfiguration());
    modelBuilder.ApplyConfiguration(new SpaceConfiguration());
    modelBuilder.ApplyConfiguration(new UserConfiguration());
  }
}
