namespace ReservationSystem.Users.Application.Create;

using Domain;

public class CreateUserUseCase
{
  private readonly IUserRepository _userRepository;

  public CreateUserUseCase(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task<User> Execute(CreateUserCommand command)
  {
    var user = new User(
      userId: new UserId(command.UserId),
      name: new UserName(command.Name),
      email: new UserEmail(command.Email),
      active: command.Active
    );

    await _userRepository.SaveAsync(user);

    return user;
  }
}
