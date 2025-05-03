namespace SharedKernel.Domain.ValueObject;

public abstract class UuidValueObject
{
  public Guid Value { get; }

  protected UuidValueObject(Guid value)
  {
    if (value == Guid.Empty)
      throw new ArgumentException("UUID can't be empty");

    Value = value;
  }

  public override bool Equals(object? obj)
  {
    if (obj is not UuidValueObject other)
      return false;
    return Value == other.Value;
  }

  public override int GetHashCode() => Value.GetHashCode();

  public override string ToString() => Value.ToString();
}
