namespace ReservationSystem.Reservations.Domain;

using Spaces.Domain;

public interface IReservationRepository
{
   Task SaveAsync(Reservation reservation);
   Task<Reservation?> FindByIdAsync(ReservationId reservationId);
   Task<IEnumerable<Reservation>> FindBySpaceAsync(SpaceId spaceId);
}
