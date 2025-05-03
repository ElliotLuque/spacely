namespace CLI.Commands.Spaces;

using System.CommandLine;

using ReservationSystem.Spaces.Application.Create;
using ReservationSystem.Spaces.Domain;

public class CreateSpaceCliCommand
{
  public readonly ISpaceRepository _spaceRepository;

  public CreateSpaceCliCommand(ISpaceRepository spaceRepository)
  {
    _spaceRepository = spaceRepository;
  }

  public Command Build()
  {
    var cmd = new Command("space", "create a new space");

    var spaceIdOption = new Option<Guid?>("--id", "id of space");
    var nameOption = new Option<string>("--name", "name of space") { IsRequired = true };
    var descriptionOption = new Option<string>("--description", "description of space") { IsRequired = true };
    var activeOption = new Option<bool?>("--active", "space active");

    cmd.AddOption(spaceIdOption); 
    cmd.AddOption(nameOption);
    cmd.AddOption(descriptionOption);
    cmd.AddOption(activeOption);

    cmd.SetHandler(async (spaceId, name, description, active) =>
    {
      var command = new CreateSpaceCommand(
        spaceId ?? Guid.NewGuid(),
        name,
        description,
        active ?? true
      );

      var useCase = new CreateSpaceUseCase(
        _spaceRepository
      );

      var space = await useCase.Execute(command);

      Console.WriteLine($"Space created: {space.SpaceId.Value}");
    }, spaceIdOption, nameOption, descriptionOption, activeOption);

    return cmd;
  }
}
