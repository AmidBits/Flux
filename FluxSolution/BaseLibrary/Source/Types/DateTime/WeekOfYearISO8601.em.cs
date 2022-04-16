namespace Flux
{
  public static partial class DateTimeEm
  {
    /// <summary>Returns the ISO8601 current week of year of the <paramref name="source"/>.</summary>
    /// <see cref="https://stackoverflow.com/questions/1497586/how-can-i-calculate-find-the-week-number-of-a-given-date"/>
    [System.Obsolete(@"Please use the built-in System.Globalization.ISOWeek.GetWeekOfYear() instead.")]
    public static int WeekOfYearISO8601(this System.DateTime source)
      => (int)System.Globalization.CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(source) is var day ? System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(source.AddDays(4 - (day == 0 ? 7 : day)), System.Globalization.CalendarWeekRule.FirstFourDayWeek, System.DayOfWeek.Monday) : throw new System.Exception();
  }
}
