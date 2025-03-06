namespace Flux
{
  public static partial class Em
  {
    public static Temporal.JulianDate ToJulianDate(this System.DateTime source, Temporal.TemporalCalendar calendar = Temporal.TemporalCalendar.GregorianCalendar)
      => new(source.Year, source.Month, source.Day, source.Hour, source.Minute, source.Second, source.Millisecond, source.Microsecond, source.Nanosecond, calendar);
  }
}
