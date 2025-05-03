namespace ReservationSystem.Spaces.Application.Create;

using Domain;

public class CreateSpaceUseCase
{
  private readonly ISpaceRepository _spaceRepository;

  public CreateSpaceUseCase(ISpaceRepository spaceRepository)
  {
    _spaceRepository = spaceRepository;
  }

  public async Task<Space> Execute(CreateSpaceCommand command)
  {
    var space = new Space(
      new SpaceId(command.SpaceId),
      new SpaceName(command.Name),
      new SpaceDescription(command.Description),
      command.Active
    );

    await _spaceRepository.SaveAsync(space);

    return space;
  }
}
