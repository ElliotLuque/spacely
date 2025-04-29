namespace ReservationSystem.Users.Domain;

using Shared.Domain.ValueObject;

public sealed class UserEmail: StringValueObject
{
  public UserEmail(string value) : base(value)
  {
    // TODO: Proper regex validation
    if (!value.Contains("@"))
      throw new ArgumentException("Email must contain @");
  }
}
