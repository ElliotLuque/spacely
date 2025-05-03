namespace ReservationSystem.Users.Infrastructure;

using Domain;

using Shared.Infrastructure;

public class PostgresUserRepository : IUserRepository
{
  private readonly ReservationSystemDbContext _dbContext;

  public PostgresUserRepository(ReservationSystemDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task SaveAsync(User user)
  {
    await _dbContext.Users.AddAsync(user);
    await _dbContext.SaveChangesAsync();
  }

  public Task<User?> FindByIdAsync(UserId userId)
  {
    throw new NotImplementedException();
  }
}
