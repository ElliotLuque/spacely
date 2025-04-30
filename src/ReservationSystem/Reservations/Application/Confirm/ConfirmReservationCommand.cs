namespace ReservationSystem.Reservations.Application.Confirm;

public sealed record ConfirmReservationCommand(
  Guid ReservationId
);
