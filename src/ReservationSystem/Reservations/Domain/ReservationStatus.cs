namespace ReservationSystem.Reservations.Domain
{
  using Shared.Domain.ValueObject;

  public sealed class ReservationStatus : StringValueObject
  {
    public static readonly ReservationStatus Pending = new ReservationStatus(nameof(Pending));
    public static readonly ReservationStatus Confirmed = new ReservationStatus(nameof(Confirmed));
    public static readonly ReservationStatus Canceled = new ReservationStatus(nameof(Canceled));

    private ReservationStatus(string value) : base(value) { }

    public bool CanConfirm() => Equals(this, Pending);

    public bool CanCancel() => !Equals(this, Canceled);
  }
}
