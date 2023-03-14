namespace Flux
{
  public static partial class ExtensionMethodsDateTime
  {
    /// <summary>Determines the current week of year of the <paramref name="source"/>. Using the specified <paramref name="dateTimeFormatInfo"/>.</summary>
    public static int WeekOfYear(this System.DateTime source, System.Globalization.DateTimeFormatInfo dateTimeFormatInfo)
      => (dateTimeFormatInfo ?? throw new System.ArgumentNullException(nameof(dateTimeFormatInfo))).Calendar.GetWeekOfYear(source, dateTimeFormatInfo.CalendarWeekRule, dateTimeFormatInfo.FirstDayOfWeek);
    /// <summary>Returns the current week of year of the <paramref name="source"/>. Using the current DateTimeFormatInfo.</summary>
    public static int WeekOfYear(this System.DateTime source)
      => WeekOfYear(source, System.Globalization.DateTimeFormatInfo.CurrentInfo);
  }
}
