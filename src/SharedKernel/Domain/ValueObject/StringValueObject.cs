namespace SharedKernel.Domain.ValueObject
{
  public abstract class StringValueObject(string value)
  {
    public string Value { get; } = value;

    public override string ToString() => Value;

    public override bool Equals(object? obj)
    {
      if (obj is not StringValueObject other)
        return false;

      return Value == other.Value && GetType() == other.GetType();
    }

    public override int GetHashCode() => HashCode.Combine(Value, GetType());
  }
}
