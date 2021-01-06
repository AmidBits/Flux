namespace Flux
{
  public static partial class SystemDateTimeEm
  {
    /// <summary>Determines the current calendar quarter (1-4) of the source.</summary>
    public static int QuarterOfYear(this System.DateTime source)
      => (((source.Month - 1) / 3) + 1);

    /// <summary>Determines the current week of the source using the current DateTimeFormatInfo.</summary>
    public static int WeekOfYear(this System.DateTime source)
      => source.WeekOfYear(System.Globalization.DateTimeFormatInfo.CurrentInfo);
    /// <summary>Determines the current week of the source using the specified DateTimeFormatInfo.</summary>
    public static int WeekOfYear(this System.DateTime source, System.Globalization.DateTimeFormatInfo dateTimeFormatInfo)
      => (dateTimeFormatInfo ?? throw new System.ArgumentNullException(nameof(dateTimeFormatInfo))).Calendar.GetWeekOfYear(source, dateTimeFormatInfo.CalendarWeekRule, dateTimeFormatInfo.FirstDayOfWeek);

    /// <summary>Determines the current week of the source date.</summary>
    /// <see cref="https://stackoverflow.com/questions/1497586/how-can-i-calculate-find-the-week-number-of-a-given-date"/>
    [System.Obsolete(@"Please use the built-in System.Globalization.ISOWeek.GetWeekOfYear() instead.")]
    public static int WeekOfYearISO8601(this System.DateTime source)
      => (int)System.Globalization.CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(source) is var day ? System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(source.AddDays(4 - (day == 0 ? 7 : day)), System.Globalization.CalendarWeekRule.FirstFourDayWeek, System.DayOfWeek.Monday) : throw new System.Exception();
  }
}
