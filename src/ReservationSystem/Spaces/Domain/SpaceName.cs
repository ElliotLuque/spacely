namespace ReservationSystem.Spaces.Domain;

using Shared.Domain.ValueObject;

public sealed class SpaceName : StringValueObject
{
  private const int MaxLength = 50;

  public SpaceName(string value) : base(value)
  {
    if (value.Length > MaxLength)
      throw new ArgumentException($"Space name can't be longer than {MaxLength} characters");
  }
}
