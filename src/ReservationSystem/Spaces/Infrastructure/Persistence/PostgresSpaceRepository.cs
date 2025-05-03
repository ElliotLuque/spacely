namespace ReservationSystem.Spaces.Infrastructure.Persistence;

using Domain;

using Shared.Infrastructure;

public class PostgresSpaceRepository : ISpaceRepository
{
  private readonly ReservationSystemDbContext _dbContext;

  public PostgresSpaceRepository(ReservationSystemDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task SaveAsync(Space space)
  {
    await _dbContext.Spaces.AddAsync(space);
    await _dbContext.SaveChangesAsync();
  }

  public async Task<Space?> FindByIdAsync(SpaceId spaceId)
  {
    throw new NotImplementedException();
  }
}
