using Spacely.ReservationSystem.Shared.Domain.ValueObject;

namespace Spacely.ReservationSystem.Reservations.Domain
{
  public sealed class ReservationStatus : StringValueObject
  {
    public static readonly ReservationStatus Pending = new ReservationStatus(nameof(Pending));
    public static readonly ReservationStatus Confirmed = new ReservationStatus(nameof(Confirmed));
    public static readonly ReservationStatus Canceled = new ReservationStatus(nameof(Canceled));

    private ReservationStatus(string value) : base(value) { }

    public bool CanConfirm() => this == Pending;

    public bool CanCancel() => this != Canceled;
  }
}
