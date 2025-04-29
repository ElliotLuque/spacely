namespace ReservationSystem.Spaces.Domain;

public interface ISpaceRepository
{
  Task SaveAsync(Space space);
  Task<Space?> FindByIdAsync(SpaceId spaceId);
}
