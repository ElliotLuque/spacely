namespace ReservationSystem.Test.Reservations.Application.Cancel;

using ReservationSystem.Reservations.Application.Cancel;
using ReservationSystem.Reservations.Domain;
using Spaces.Domain;

using Users.Domain;

public class CancelReservationUseCaseTest
{
  [Fact]
  public async Task ShouldCancelReservation()
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

    var useCase = new CancelReservationUseCase(
      reservationRepoMock.Object
    );

    var command = new CancelReservationCommand(
     ReservationId: reservationId.Value
    );
    
    // When
    await useCase.Execute(command);

    // Then
    Assert.Equal(ReservationStatus.Canceled, reservation.Status);
    reservationRepoMock.Verify(
      x => x.SaveAsync(reservation),
      Times.Once
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
      .ReturnsAsync((Reservation?) null);

    var useCase = new CancelReservationUseCase(
      reservationRepoMock.Object
    );

    var command = new CancelReservationCommand(
      ReservationId: reservationId.Value
    );
    
    // When & Then
    var exception = await Assert.ThrowsAsync<ArgumentException>(() => useCase.Execute(command));
    Assert.Equal("Reservation not found", exception.Message);
  }
}
