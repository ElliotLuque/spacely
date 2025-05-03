namespace ReservationSystem.Test.Reservations.Application.FindById;

using ReservationSystem.Reservations.Application.Find;
using ReservationSystem.Reservations.Application.FindById;
using ReservationSystem.Reservations.Domain;
using ReservationSystem.Spaces.Domain;
using ReservationSystem.Users.Domain;

public class FindReservationByIdUseCaseTest
{
  [Fact]
  public async Task ShouldFindReservation()
  {
    //  Given
    var reservationId = Guid.NewGuid();
    var userId = Guid.NewGuid();
    var spaceId = Guid.NewGuid();
    var startDate = DateTime.UtcNow.AddHours(1);
    var endDate = DateTime.UtcNow.AddHours(2);
    var status = ReservationStatus.Pending;

    var reservation = new Reservation(
      new ReservationId(reservationId),
      new UserId(userId),
      new SpaceId(spaceId),
      new ReservationDateRange(startDate, endDate),
      status
    );

    var reservationRepoMock = new Mock<IReservationRepository>();
    reservationRepoMock
      .Setup(repo => repo.FindByIdAsync(It.Is<ReservationId>(id => id.Value == reservationId)))
      .ReturnsAsync(reservation);

    var query = new FindReservationByIdQuery(
      reservationId
    );

    var useCase = new FindReservationByIdUseCase(
      reservationRepoMock.Object
    );

    // When
    var result = await useCase.Execute(query);

    // Then
    Assert.NotNull(result);
    Assert.Equal(reservationId.ToString(), result.Id);
    Assert.Equal(spaceId.ToString(), result.SpaceId);
    Assert.Equal(userId.ToString(), result.UserId);
    Assert.Equal(startDate, result.StartDate);
    Assert.Equal(endDate, result.EndDate);
    Assert.Equal(status.ToString(), result.Status);
  }
}
