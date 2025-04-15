namespace Flux
{
  public static partial class DateTimes
  {
    /// <summary>Determines the week of year for the <paramref name="source"/>.</summary>
    public static int GetWeekOfYear(this System.DateTime source, System.Globalization.CalendarWeekRule rule, System.DayOfWeek firstDayOfWeek)
      => System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(source, rule, firstDayOfWeek);

    /// <summary>Determines the week of year for the <paramref name="source"/>.</summary>
    public static int GetWeekOfYear(this System.DateTime source, System.Globalization.CultureInfo? culture = null)
      => GetWeekOfYear(source, (culture ?? System.Globalization.CultureInfo.CurrentCulture).DateTimeFormat.CalendarWeekRule, (culture ?? System.Globalization.CultureInfo.CurrentCulture).DateTimeFormat.FirstDayOfWeek);
  }
}
