namespace ReservationSystem.Test.Spaces.Application.Create;

using ReservationSystem.Spaces.Application.Create;
using ReservationSystem.Spaces.Domain;

public class CreateSpaceUseCaseTest
{
  [Fact]
  public async Task ShouldCreateSpace()
  {
    // Given
    var spaceRepoMock = new Mock<ISpaceRepository>();

    var command = new CreateSpaceCommand(
      SpaceId: Guid.NewGuid(),
      Name: "Mock space",
      Description: "Mock space description",
      Active: true
    );

    var useCase = new CreateSpaceUseCase(
      spaceRepoMock.Object
    );

    // When
    var space = await useCase.Execute(command);

    // Then
    Assert.NotNull(space);
    Assert.Equal(command.SpaceId, space.SpaceId.Value);
    Assert.Equal(command.Name, space.Name.Value);
    Assert.Equal(command.Description, space.Description.Value);
    Assert.Equal(command.Active, space.Active);
    spaceRepoMock.Verify(
      x => x.SaveAsync(It.Is<Space>(s =>
        s.SpaceId.Value == command.SpaceId &&
        s.Name.Value == command.Name &&
        s.Description.Value == command.Description &&
        s.Active == command.Active)),
      Times.Once
    );
  }
}
