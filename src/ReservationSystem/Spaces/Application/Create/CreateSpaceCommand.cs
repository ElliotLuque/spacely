namespace ReservationSystem.Spaces.Application.Create;

public sealed record CreateSpaceCommand(
  Guid SpaceId,
  string Name,
  string Description,
  bool Active
);
