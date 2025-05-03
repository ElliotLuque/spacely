namespace ReservationSystem.Reservations.Application;

public sealed record ReservationSystemReservationResponse(
  string Id,
  string SpaceId,
  string UserId,
  DateTime StartDate,
  DateTime EndDate,
  string Status
);
