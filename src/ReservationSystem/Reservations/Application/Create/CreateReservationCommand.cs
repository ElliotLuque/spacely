namespace ReservationSystem.Reservations.Application.Create;

public sealed record CreateReservationCommand(
  Guid ReservationId,
  Guid UserId,
  Guid SpaceId,
  DateTime StartDate,
  DateTime EndDate
);
