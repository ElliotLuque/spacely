namespace CLI.Commands.Reservations;

using System.CommandLine;

using ReservationSystem.Reservations.Application.Find;
using ReservationSystem.Reservations.Application.FindById;
using ReservationSystem.Reservations.Domain;
using ReservationSystem.Reservations.Infrastructure;
using ReservationSystem.Spaces.Domain;
using ReservationSystem.Users.Domain;

public class FindReservationByIdCliCommand
{
  private readonly IReservationRepository _reservationRepository;

  public FindReservationByIdCliCommand(IReservationRepository reservationRepository)
  {
    _reservationRepository = reservationRepository;
  }

  public Command Build()
  {
    var cmd = new Command("reservation", "find a reservation by its id");

    var reservationIdOption = new Option<Guid>("--id", "reservation id") { IsRequired = true };
    cmd.AddOption(reservationIdOption);

    cmd.SetHandler(async (Guid reservationId) =>
    {
      var query = new FindReservationByIdQuery(reservationId);
      var useCase = new FindReservationByIdUseCase(_reservationRepository);

      var reservation = await useCase.Execute(query);

      Console.WriteLine("Reservation found!");
      Console.WriteLine("=================");
      Console.WriteLine($"id: {reservation.Id}");
      Console.WriteLine($"user_id: {reservation.UserId}");
      Console.WriteLine($"space_id: {reservation.SpaceId}");
      Console.WriteLine($"start_date: {reservation.StartDate}");
      Console.WriteLine($"end_date: {reservation.EndDate}");
      Console.WriteLine($"status: {reservation.Status}");
    }, reservationIdOption);

    return cmd;
  }
}
