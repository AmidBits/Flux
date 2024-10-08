namespace Flux
{
  public static partial class Fx
  {
    public static System.DateTime AddDatePart(this System.DateTime source, DateTimePart part, int count)
      => part switch
      {
        DateTimePart.Year => source.AddYears(count),
        DateTimePart.Quarter => source.AddMonths(count * 3),
        DateTimePart.Month => source.AddMonths(count),
        DateTimePart.FortNight => source.AddDays(count * 14),
        DateTimePart.Week => source.AddDays(count * 7),
        DateTimePart.Day => source.AddDays(count),
        DateTimePart.Hour => source.AddHours(count),
        DateTimePart.Minute => source.AddMinutes(count),
        DateTimePart.Second => source.AddSeconds(count),
        _ => throw new NotImplementedException(),
      };
  }
}
