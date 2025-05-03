namespace ReservationSystem.Reservations.Infrastructure.Persistence;

using Domain;

using Microsoft.EntityFrameworkCore;

using Shared.Infrastructure;

using Spaces.Domain;

public class PostgresReservationRepository : IReservationRepository
{
  private readonly ReservationSystemDbContext _dbContext;

  public PostgresReservationRepository(ReservationSystemDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task SaveAsync(Reservation reservation)
  {
    await _dbContext.Reservations.AddAsync(reservation);
    await _dbContext.SaveChangesAsync();
  }

  public async Task<Reservation?> FindByIdAsync(ReservationId reservationId)
  {
    return await _dbContext.Reservations
      .FirstOrDefaultAsync(s => s.ReservationId == reservationId);
    ;
  }

  public Task<IEnumerable<Reservation>> FindBySpaceAsync(SpaceId spaceId) => throw new NotImplementedException();
}
