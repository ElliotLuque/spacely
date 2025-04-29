namespace ReservationSystem.Reservations.Domain
{
  using Shared.Domain;

  public class Reservation : AggregateRoot
  {
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid SpaceId { get; private set; }
    public ReservationDate Date { get; private set; }
    public ReservationStatus Status { get; private set; }

    public Reservation(Guid id, Guid userId, Guid spaceId, ReservationDate date, ReservationStatus status)
    {
      Id = id;
      UserId = userId;
      SpaceId = spaceId;
      Date = date;
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
  }
}
