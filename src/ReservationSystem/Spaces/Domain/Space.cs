namespace ReservationSystem.Spaces.Domain;

using SharedKernel.Domain;

public class Space : AggregateRoot
{
  public SpaceId SpaceId { get; private set; }
  public SpaceName Name { get; private set; }
  public SpaceDescription Description { get; private set; }
  public bool Active { get; private set; }

  public Space(SpaceId spaceId, SpaceName name, SpaceDescription description, bool active)
  {
    SpaceId = spaceId;
    Name = name;
    Description = description;
    Active = active;
  }

  public void ChangeName(SpaceName name) => Name = name;
  public void ChangeDescription(SpaceDescription description) => Description = description;
  public void Delete() => Active = false;
}
