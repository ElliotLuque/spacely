namespace ReservationSystem.Reservations.Application.Create;

using Domain;
using Spaces.Domain;
using Users.Domain;

public class CreateReservationUseCase
{
  private readonly IReservationRepository _reservationRepository;
  private readonly IUserRepository _userRepository;
  private readonly ISpaceRepository _spaceRepository;

  public CreateReservationUseCase(IReservationRepository reservationRepository, IUserRepository userRepository, ISpaceRepository spaceRepository)
  {
    _reservationRepository = reservationRepository;
    _userRepository = userRepository;
    _spaceRepository = spaceRepository;
  }

  public async Task<Reservation> Execute(CreateReservationCommand command)
  {
    var user = await  _userRepository.FindByIdAsync(new UserId(command.UserId));
    if (user is null)
      throw new ArgumentException("User not found");
    
    var space = await _spaceRepository.FindByIdAsync(new SpaceId(command.SpaceId));
    if (space is null)
      throw new ArgumentException("Space not found");
    
    var range = new ReservationDateRange(command.StartDate, command.EndDate);
    
    var reservation = new Reservation(
      new ReservationId(command.ReservationId),
      new UserId(command.UserId),
      new SpaceId(command.SpaceId),
      range,
      ReservationStatus.Pending
    );
    
    await _reservationRepository.SaveAsync(reservation);
    
    return reservation;
  }
}
