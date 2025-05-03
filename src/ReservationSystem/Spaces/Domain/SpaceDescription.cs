namespace ReservationSystem.Spaces.Domain;

using SharedKernel.Domain.ValueObject;

public sealed class SpaceDescription : StringValueObject
{
  private const int MaxLength = 100;

  public SpaceDescription(string value) : base(value)
  {
    if (value.Length > MaxLength)
      throw new ArgumentException($"Space description can't be longer than {MaxLength} characters");
  }
}
