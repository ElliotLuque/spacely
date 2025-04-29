namespace ReservationSystem.Reservations.Domain
{
  using Shared.Domain;
  using Spaces.Domain;
  using Users.Domain;

  public class Reservation: AggregateRoot
  {
    public ReservationId ReservationId { get; private set; }
    public UserId UserId { get; private set; } 
    public SpaceId SpaceId { get; private set; }
    public ReservationDateRange DateRange { get; private set; }
    public ReservationStatus Status { get; private set; }

    public Reservation(ReservationId reservationId, UserId userId, SpaceId spaceId, ReservationDateRange dateRange, ReservationStatus status)
    {
      ReservationId = reservationId;
      UserId = userId;
      SpaceId = spaceId;
      DateRange = dateRange;
      Status = status;
    }

    public void Cancel()
    {
      if (!Status.CanCancel())
        throw new InvalidOperationException("Reservation already canceled");

      Status = ReservationStatus.Canceled;
    }

    public void Confirm()
    {
      if (!Status.CanConfirm())
        throw new InvalidOperationException("Can't confirm this reservation");

      Status = ReservationStatus.Confirmed;
    }
    
    public void Reschedule(ReservationDateRange newDateRange)
    {
      if (!Status.CanReschedule())
        throw new InvalidOperationException("Can't reschedule this reservation");
      
      DateRange = newDateRange;
    }

    public override bool Equals(object? obj)
    {
      if (obj is not Reservation other)
        return false;
      return ReservationId == other.ReservationId;
    }

    public override int GetHashCode() => HashCode.Combine(ReservationId, UserId, SpaceId, DateRange, Status);
  }
}
