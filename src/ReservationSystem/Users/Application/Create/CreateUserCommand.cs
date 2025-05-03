namespace ReservationSystem.Users.Application.Create;

public sealed record CreateUserCommand(
  Guid UserId,
  string Name,
  string Email,
  bool Active
);
