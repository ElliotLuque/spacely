namespace ReservationSystem.Reservations.Application.Confirm;

using Domain;

public class ConfirmReservationUseCase
{
  private readonly IReservationRepository _reservationRepository;

  public ConfirmReservationUseCase(IReservationRepository reservationRepository)
  {
    _reservationRepository = reservationRepository;
  }

  public async Task Execute(ConfirmReservationCommand command)
  {
    var reservation = await _reservationRepository.FindByIdAsync(new ReservationId(command.ReservationId));
    if (reservation is null)
      throw new ArgumentException("Reservation not found");
    
    reservation.Confirm();

    await _reservationRepository.SaveAsync(reservation);
  }
}
