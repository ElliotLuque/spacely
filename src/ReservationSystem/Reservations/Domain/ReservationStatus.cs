namespace ReservationSystem.Reservations.Domain
{
  using SharedKernel.Domain.ValueObject;

  public sealed class ReservationStatus : StringValueObject
  {
    public static readonly ReservationStatus Pending = new(nameof(Pending));
    public static readonly ReservationStatus Confirmed = new(nameof(Confirmed));
    public static readonly ReservationStatus Canceled = new(nameof(Canceled));

    private ReservationStatus(string value) : base(value)
    {
    }

    public static ReservationStatus From(string value) =>
      value switch
      {
        nameof(Pending) => Pending,
        nameof(Confirmed) => Confirmed,
        nameof(Canceled) => Canceled,
        _ => throw new ArgumentException($"Invalid reservation status: {value}")
      };
  }
}
