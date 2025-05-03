namespace CLI.Commands.Reservations;

using System.CommandLine;

using ReservationSystem.Reservations.Application.Create;
using ReservationSystem.Reservations.Domain;
using ReservationSystem.Spaces.Domain;
using ReservationSystem.Users.Domain;

public class CreateReservationCliCommand
{
  private readonly IReservationRepository _reservationRepository;
  private readonly IUserRepository _userRepository;
  private readonly ISpaceRepository _spaceRepository;

  public CreateReservationCliCommand(IReservationRepository reservationRepository, IUserRepository userRepository,
    ISpaceRepository spaceRepository)
  {
    _reservationRepository = reservationRepository;
    _userRepository = userRepository;
    _spaceRepository = spaceRepository;
  }

  public Command Build()
  {
    var cmd = new Command("reservation", "create a new reservation");

    var reservationIdOption = new Option<Guid?>("--reservation-id", "Reservation ID ");
    var userIdOption = new Option<Guid>("--user-id", "User ID") { IsRequired = true };
    var spaceIdOption = new Option<Guid>("--space-id", "Space ID") { IsRequired = true };
    var startDateOption = new Option<DateTime>("--start-date", "Start date and time (UTC)") { IsRequired = true };
    var endDateOption = new Option<DateTime>("--end-date", "End date and time (UTC)") { IsRequired = true };

    cmd.AddOption(reservationIdOption);
    cmd.AddOption(userIdOption);
    cmd.AddOption(spaceIdOption);
    cmd.AddOption(startDateOption);
    cmd.AddOption(endDateOption);

    cmd.SetHandler(async (Guid? reservationId, Guid userId, Guid spaceId, DateTime startDate, DateTime endDate) =>
    {
      var command = new CreateReservationCommand(
        reservationId ?? Guid.NewGuid(),
        userId,
        spaceId,
        startDate,
        endDate
      );

      var useCase = new CreateReservationUseCase(
        _reservationRepository,
        _userRepository,
        _spaceRepository
      );
      var reservation = await useCase.Execute(command);

      Console.WriteLine($"Reservation created: {reservation.ReservationId.Value}");
    }, reservationIdOption, userIdOption, spaceIdOption, startDateOption, endDateOption);

    return cmd;
  }
}
