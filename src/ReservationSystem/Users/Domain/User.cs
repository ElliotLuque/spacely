namespace ReservationSystem.Users.Domain;

using SharedKernel.Domain;

public class User : AggregateRoot
{
  public UserId UserId { get; private set; }
  public UserName Name { get; private set; }
  public UserEmail Email { get; private set; }
  public bool Active { get; private set; }

  public User(UserId userId, UserName name, UserEmail email, bool active)
  {
    UserId = userId;
    Name = name;
    Email = email;
    Active = active;
  }
}
