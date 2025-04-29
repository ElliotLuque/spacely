namespace ReservationSystem.Reservations.Infrastructure;

using System.Collections.Concurrent;

using Domain;
using Spaces.Domain;

public class InMemoryReservationRepository : IReservationRepository
{
  private readonly ConcurrentDictionary<Guid, Reservation> _reservations = new();
    
  public Task SaveAsync(Reservation reservation)
  {
    _reservations[reservation.ReservationId.Value] = reservation;
    return Task.CompletedTask;
  }

  public Task<Reservation?> FindByIdAsync(ReservationId reservationId)
  {
     _reservations.TryGetValue(reservationId.Value, out var reservation);
     return Task.FromResult(reservation);
  }

  public Task<IEnumerable<Reservation>> FindBySpaceAsync(SpaceId spaceId)
  {
    var reservation = _reservations.Values
      .Where(r => r.SpaceId == spaceId)
      .AsEnumerable();

    return Task.FromResult(reservation);
  }
}
