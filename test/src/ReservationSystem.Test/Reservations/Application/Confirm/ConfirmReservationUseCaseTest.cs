namespace ReservationSystem.Test.Reservations.Application.Confirm;

using ReservationSystem.Reservations.Application.Confirm;
using ReservationSystem.Reservations.Domain;

using Spaces.Domain;

using Users.Domain;

public class ConfirmReservationUseCaseTest
{
  [Fact]
  public async Task ShouldConfirmReservation()
  {
    // Given
    var reservationId = new ReservationId(Guid.NewGuid());

    var reservation = new Reservation(
      reservationId,
      new UserId(Guid.NewGuid()),
      new SpaceId(Guid.NewGuid()),
      new ReservationDateRange(DateTime.UtcNow.AddHours(1), DateTime.UtcNow.AddHours(2)),
      ReservationStatus.Pending
    );

    var reservationRepoMock = new Mock<IReservationRepository>();
    reservationRepoMock
      .Setup(repo => repo.FindByIdAsync(It.IsAny<ReservationId>()))
      .ReturnsAsync(reservation);

    var useCase = new ConfirmReservationUseCase(
      reservationRepoMock.Object
    );

    var command = new ConfirmReservationCommand(
      reservationId.Value
    );

    // When
    await useCase.Execute(command);

    // Then
    Assert.Equal(ReservationStatus.Confirmed, reservation.Status);
    reservationRepoMock.Verify(
      x => x.SaveAsync(reservation),
      Times.Once
    );
  }
  
  [Fact]
  public async Task ShouldNotConfirmReservation()
  {
    // Given
    var reservationId = new ReservationId(Guid.NewGuid());

    var reservation = new Reservation(
      reservationId,
      new UserId(Guid.NewGuid()),
      new SpaceId(Guid.NewGuid()),
      new ReservationDateRange(DateTime.UtcNow.AddHours(1), DateTime.UtcNow.AddHours(2)),
      ReservationStatus.Canceled
    );

    var reservationRepoMock = new Mock<IReservationRepository>();
    reservationRepoMock
      .Setup(repo => repo.FindByIdAsync(It.IsAny<ReservationId>()))
      .ReturnsAsync(reservation);

    var useCase = new ConfirmReservationUseCase(
      reservationRepoMock.Object
    );

    var command = new ConfirmReservationCommand(
      reservationId.Value
    );

    // When & Then
    var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => useCase.Execute(command));
    Assert.Equal("Can't confirm this reservation", exception.Message);
    reservationRepoMock.Verify(
      x => x.SaveAsync(reservation),
      Times.Never
    );
  }

  [Fact]
  public async Task ShouldErrorWhenReservationNotFound()
  {
    // Given
    var reservationId = new ReservationId(Guid.NewGuid());

    var reservationRepoMock = new Mock<IReservationRepository>();
    reservationRepoMock
      .Setup(repo => repo.FindByIdAsync(It.IsAny<ReservationId>()))
      .ReturnsAsync((Reservation?)null);

    var useCase = new ConfirmReservationUseCase(
      reservationRepoMock.Object
    );

    var command = new ConfirmReservationCommand(
      reservationId.Value
    );

    // When & Then
    var exception = await Assert.ThrowsAsync<ArgumentException>(() => useCase.Execute(command));
    Assert.Equal("Reservation not found", exception.Message);
    
  }
}
