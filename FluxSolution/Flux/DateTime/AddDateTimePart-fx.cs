namespace Flux
{
  public static partial class Fx
  {
    public static System.DateTime AddDateTimePart(this System.DateTime source, Temporal.DateTimePart part, int count)
      => part switch
      {
        Temporal.DateTimePart.Year => source.AddYears(count),
        Temporal.DateTimePart.Quarter => source.AddMonths(count * 3),
        Temporal.DateTimePart.Month => source.AddMonths(count),
        Temporal.DateTimePart.FortNight => source.AddDays(count * 14),
        Temporal.DateTimePart.Week => source.AddDays(count * 7),
        Temporal.DateTimePart.Day => source.AddDays(count),
        Temporal.DateTimePart.Hour => source.AddHours(count),
        Temporal.DateTimePart.Minute => source.AddMinutes(count),
        Temporal.DateTimePart.Second => source.AddSeconds(count),
        Temporal.DateTimePart.Millisecond => source.AddMilliseconds(count),
        Temporal.DateTimePart.Microsecond => source.AddMicroseconds(count),
        Temporal.DateTimePart.Tick => source.AddTicks(count),
        _ => throw new NotImplementedException(),
      };
  }
}
