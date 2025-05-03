using ReservationSystem.Reservations.Application.Create;
using ReservationSystem.Reservations.Domain;
using ReservationSystem.Spaces.Domain;
using ReservationSystem.Users.Domain;

namespace ReservationSystem.Test.Reservations.Application.Create;

public class CreateReservationUseCaseTest
{
  [Fact]
  public async Task ShouldCreateReservation()
  {
    // Given
    var reservationId = new ReservationId(Guid.NewGuid());
    var userId = new UserId(Guid.NewGuid());
    var spaceId = new SpaceId(Guid.NewGuid());

    var userRepoMock = new Mock<IUserRepository>();
    userRepoMock
      .Setup(repo => repo.FindByIdAsync(It.IsAny<UserId>()))
      .ReturnsAsync(new User(userId, new UserName("Mock user"), new UserEmail("mock@example.com"), true));

    var spaceRepoMock = new Mock<ISpaceRepository>();
    spaceRepoMock
      .Setup(repo => repo.FindByIdAsync(It.IsAny<SpaceId>()))
      .ReturnsAsync(new Space(spaceId, new SpaceName("Mock space"), new SpaceDescription("Mock space description"),
        true));

    var reservationRepoMock = new Mock<IReservationRepository>();

    var useCase = new CreateReservationUseCase(
      reservationRepoMock.Object,
      userRepoMock.Object,
      spaceRepoMock.Object
    );

    var command = new CreateReservationCommand(
      ReservationId: reservationId.Value,
      UserId: userId.Value,
      SpaceId: spaceId.Value,
      StartDate: DateTime.UtcNow.AddDays(1),
      EndDate: DateTime.UtcNow.AddDays(2)
    );

    // When
    var reservation = await useCase.Execute(command);

    // Then
    Assert.NotNull(reservation);
    Assert.Equal(command.UserId, reservation.UserId.Value);
    Assert.Equal(command.SpaceId, reservation.SpaceId.Value);
    Assert.Equal(ReservationStatus.Pending, reservation.Status);
    reservationRepoMock.Verify(
      x => x.SaveAsync(It.Is<Reservation>(r =>
        r.ReservationId.Value == reservationId.Value &&
        r.Status == ReservationStatus.Pending)),
      Times.Once
    );
  }

  [Fact]
  public async Task ShouldErrorWhenUserNotFound()
  {
    // Given
    var reservationId = new ReservationId(Guid.NewGuid());
    var userId = new UserId(Guid.NewGuid());
    var spaceId = new SpaceId(Guid.NewGuid());

    var userRepoMock = new Mock<IUserRepository>();
    userRepoMock
      .Setup(repo => repo.FindByIdAsync(It.IsAny<UserId>()))
      .ReturnsAsync((User?)null);

    var spaceRepoMock = new Mock<ISpaceRepository>();
    spaceRepoMock
      .Setup(repo => repo.FindByIdAsync(It.IsAny<SpaceId>()))
      .ReturnsAsync(new Space(spaceId, new SpaceName("Mock space"), new SpaceDescription("Mock space description"),
        true));

    var reservationRepoMock = new Mock<IReservationRepository>();

    var useCase = new CreateReservationUseCase(
      reservationRepoMock.Object,
      userRepoMock.Object,
      spaceRepoMock.Object
    );

    var command = new CreateReservationCommand(
      ReservationId: reservationId.Value,
      UserId: userId.Value,
      SpaceId: spaceId.Value,
      StartDate: DateTime.UtcNow.AddHours(1),
      EndDate: DateTime.UtcNow.AddHours(2)
    );

    // When & Then
    var exception = await Assert.ThrowsAsync<ArgumentException>(() => useCase.Execute(command));
    Assert.Equal("User not found", exception.Message);
    reservationRepoMock.Verify(
      x => x.SaveAsync(It.IsAny<Reservation>()),
      Times.Never
    );
  }

  [Fact]
  public async Task ShouldErrorWhenSpaceNotFound()
  {
    // Given
    var reservationId = new ReservationId(Guid.NewGuid());
    var userId = new UserId(Guid.NewGuid());
    var spaceId = new SpaceId(Guid.NewGuid());

    var userRepoMock = new Mock<IUserRepository>();
    userRepoMock
      .Setup(repo => repo.FindByIdAsync(It.IsAny<UserId>()))
      .ReturnsAsync(new User(userId, new UserName("Mock user"), new UserEmail("mock@example.com"), true));

    var spaceRepoMock = new Mock<ISpaceRepository>();
    spaceRepoMock
      .Setup(repo => repo.FindByIdAsync(It.IsAny<SpaceId>()))
      .ReturnsAsync((Space?)null);

    var reservationRepoMock = new Mock<IReservationRepository>();

    var useCase = new CreateReservationUseCase(
      reservationRepoMock.Object,
      userRepoMock.Object,
      spaceRepoMock.Object
    );

    var command = new CreateReservationCommand(
      ReservationId: reservationId.Value,
      UserId: userId.Value,
      SpaceId: spaceId.Value,
      StartDate: DateTime.UtcNow.AddHours(1),
      EndDate: DateTime.UtcNow.AddHours(2)
    );

    // When & Then
    var exception = await Assert.ThrowsAsync<ArgumentException>(() => useCase.Execute(command));
    Assert.Equal("Space not found", exception.Message);
    reservationRepoMock.Verify(
      x => x.SaveAsync(It.IsAny<Reservation>()),
      Times.Never
    );
  }
}
