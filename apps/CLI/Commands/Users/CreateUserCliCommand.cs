namespace CLI.Commands.Users;

using System.CommandLine;

using ReservationSystem.Users.Application.Create;
using ReservationSystem.Users.Domain;

public class CreateUserCliCommand
{
  private readonly IUserRepository _userRepository;

  public CreateUserCliCommand(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public Command Build()
  {
    var cmd = new Command("user", "create a new user");

    var userIdOption = new Option<Guid?>("--id", "id of user");
    var nameOption = new Option<string>("--name", "name of user") { IsRequired = true };
    var emailOption = new Option<string>("--email", "email of user") { IsRequired = true };
    var activeOption = new Option<bool?>("--active", "user active");

    cmd.AddOption(userIdOption);
    cmd.AddOption(nameOption);
    cmd.AddOption(emailOption);
    cmd.AddOption(activeOption);

    cmd.SetHandler(async (Guid? userId, string name, string email, bool? active) =>
    {
      var command = new CreateUserCommand(
        userId ?? Guid.NewGuid(),
        name,
        email,
        active ?? true
      );

      var useCase = new CreateUserUseCase(_userRepository);

      var user = await useCase.Execute(command);

      Console.WriteLine($"User created: {user.UserId.Value}");
    }, userIdOption, nameOption, emailOption, activeOption);

    return cmd;
  }
}
