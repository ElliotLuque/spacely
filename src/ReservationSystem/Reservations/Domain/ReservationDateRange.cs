namespace ReservationSystem.Reservations.Domain
{
  public sealed class ReservationDateRange
  {
    public DateTime Start { get; }
    public DateTime End { get; }

    public ReservationDateRange(DateTime start, DateTime end)
    {
      if (start >= end)
        throw new ArgumentException("Start date must be previous to end date");

      Start = start;
      End = end;
    }

    public bool Overlaps(ReservationDateRange other) => Start < other.End && other.Start < End;
  }
}
