namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Determines the week of year for the <paramref name="source"/>.</summary>
    public static int GetWeekOfYear(this System.DateTime source, System.Globalization.CalendarWeekRule rule, System.DayOfWeek firstDayOfWeek)
      => System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(source, rule, firstDayOfWeek);

    /// <summary>Determines the week of year for the <paramref name="source"/>.</summary>
    public static int GetWeekOfYear(this System.DateTime source)
      => GetWeekOfYear(source, System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
  }
}
