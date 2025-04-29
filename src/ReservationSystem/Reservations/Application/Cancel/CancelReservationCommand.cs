namespace ReservationSystem.Reservations.Application.Cancel;

public sealed record CancelReservationCommand(
  Guid ReservationId
);
