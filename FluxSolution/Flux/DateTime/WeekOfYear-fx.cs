namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Determines the current week of year of the <paramref name="source"/>. Uses the specified <paramref name="dateTimeFormatInfo"/>, or <see cref="System.Globalization.DateTimeFormatInfo.CurrentInfo"/> if null.</para>
    /// </summary>
    public static int WeekOfYear(this System.DateTime source, System.Globalization.DateTimeFormatInfo? dateTimeFormatInfo = null)
    {
      dateTimeFormatInfo ??= System.Globalization.DateTimeFormatInfo.CurrentInfo;

      return dateTimeFormatInfo.Calendar.GetWeekOfYear(source, dateTimeFormatInfo.CalendarWeekRule, dateTimeFormatInfo.FirstDayOfWeek);
    }
  }
}
