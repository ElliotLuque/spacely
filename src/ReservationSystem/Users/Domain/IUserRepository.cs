namespace ReservationSystem.Users.Domain;

public interface IUserRepository
{
  Task SaveAsync(User user);
  Task<User?> FindByIdAsync(UserId userId);
}
