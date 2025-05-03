namespace ReservationSystem.Test.Users.Application.Create;

using ReservationSystem.Users.Application.Create;
using ReservationSystem.Users.Domain;

public class CreateUserUseCaseTest
{
  [Fact]
  public async Task ShouldCreateUser()
  {
    // Given
    var command = new CreateUserCommand(
      UserId: Guid.NewGuid(),
      Name: "Mock user",
      Email: "user@example.com",
      Active: true
    );

    var userRepoMock = new Mock<IUserRepository>();

    var useCase = new CreateUserUseCase(userRepoMock.Object);

    // When
    var user = await useCase.Execute(command);

    // Then
    Assert.NotNull(user);
    Assert.Equal(command.UserId, user.UserId.Value);
    Assert.Equal(command.Name, user.Name.Value);
    Assert.Equal(command.Email, user.Email.Value);
    Assert.Equal(command.Active, user.Active);
    userRepoMock.Verify(
      x => x.SaveAsync(It.Is<User>(u =>
        u.UserId.Value == command.UserId &&
        u.Name.Value == command.Name &&
        u.Email.Value == command.Email &&
        u.Active == command.Active)),
      Times.Once
    );
  }
}
