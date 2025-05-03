namespace ReservationSystem.Reservations.Application.FindById;

using Domain;

using Find;

public class FindReservationByIdUseCase
{
  private readonly IReservationRepository _reservationRepository;

  public FindReservationByIdUseCase(IReservationRepository reservationRepository)
  {
    _reservationRepository = reservationRepository;
  }

  public async Task<ReservationSystemReservationResponse> Execute(FindReservationByIdQuery query)
  {
    var reservation = await _reservationRepository.FindByIdAsync(new ReservationId(query.ReservationId));
    if (reservation is null)
      throw new ArgumentException("Reservation not found");

    return new ReservationSystemReservationResponse(
      Id: reservation.ReservationId.Value.ToString(),
      SpaceId: reservation.SpaceId.Value.ToString(),
      UserId: reservation.UserId.Value.ToString(),
      StartDate: reservation.DateRange.Start,
      EndDate: reservation.DateRange.End,
      Status: reservation.Status.ToString()
    );
  }
}
