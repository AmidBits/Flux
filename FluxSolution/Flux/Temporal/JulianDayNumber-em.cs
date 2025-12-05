namespace Flux
{
  public static partial class TemporalExtensions
  {
    public static Temporal.JulianDayNumber ToJulianDayNumber(this System.DateTime source, Temporal.TemporalCalendar calendar = Temporal.TemporalCalendar.GregorianCalendar)
      => new(source.Year, source.Month, source.Day, calendar);
  }
}
