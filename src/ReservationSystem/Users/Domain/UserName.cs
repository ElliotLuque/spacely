namespace ReservationSystem.Users.Domain;

using Shared.Domain.ValueObject;

public sealed class UserName: StringValueObject
{
  private const int MaxLength = 50;
  private const int MinLength = 5;
  
  public UserName(string value) : base(value)
  {
    if (value.Length < MinLength)
      throw new ArgumentException($"User name must be at least {MinLength} characters long");
    
    if (value.Length > MaxLength)
        throw new ArgumentException($"User name can't be longer than {MaxLength} characters");
  }
}
