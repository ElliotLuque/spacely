namespace Spacely.ReservationSystem.Reservations.Domain
{
  public sealed class ReservationDate
  {
    public DateTime Start { get; }
    public DateTime End { get; }

    public ReservationDate(DateTime start, DateTime end)
    {
      if (start >= end)
        throw new ArgumentException("Start date must be previous to end date");

      Start = start;
      End = end;
    }

    public bool Overlaps(ReservationDate other)
    {
      return Start < other.End && other.Start < End;
    }

  }
}
