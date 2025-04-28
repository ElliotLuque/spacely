namespace Spacely.ReservationSystem.Shared
{
  public abstract class StringValueObject
  {
    public string Value { get; }

    public StringValueObject(string value)
    {
      Value = value;
    }

    public override string ToString()
    {
      return Value;
    }

    public override bool Equals(object obj)
    {
      if (obj is not StringValueObject other)
        return false;

      return Value == other.Value && GetType() == other.GetType();
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(Value, GetType());
    }
  }
}
