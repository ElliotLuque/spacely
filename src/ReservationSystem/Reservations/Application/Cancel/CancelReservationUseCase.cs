namespace ReservationSystem.Reservations.Application.Cancel;

using Domain;

public class CancelReservationUseCase
{
  private readonly IReservationRepository _reservationRepository;

  public CancelReservationUseCase(IReservationRepository reservationRepository)
  {
    _reservationRepository = reservationRepository;
  }

  public async Task Execute(CancelReservationCommand command)
  {
    var reservation = await _reservationRepository.FindByIdAsync(new ReservationId(command.ReservationId));
    if (reservation is null)
      throw new ArgumentException("Reservation not found");

    reservation.Cancel();

    await _reservationRepository.SaveAsync(reservation);
  }
}
