using System.Linq;

namespace Flux
{
  public static partial class SystemDateTimeEm
  {
    /// <summary>Determines the last day of the month in the source.</summary>
    public static System.DateTime LastDayOfMonth(this System.DateTime source)
      => new System.DateTime(source.Year, source.Month, System.DateTime.DaysInMonth(source.Year, source.Month));

    /// <summary>Determines the last day of the quarter in the source.</summary>
    public static System.DateTime LastDayOfQuarter(this System.DateTime source)
      => source.GetQuarters().ElementAt(source.QuarterOfYear() - 1).end;

    /// <summary>Determines the last day of the week in the source, based on the current DateTimeFormatInfo.</summary>
    public static System.DateTime LastDayOfWeek(this System.DateTime source)
      => source.LastDayOfWeek((System.DayOfWeek)(((int)System.Globalization.DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek + 6) % 7));
    /// <summary>Determines the last day of the week in the source, based on the specified DateTimeFormatInfo.</summary>
    public static System.DateTime LastDayOfWeek(this System.DateTime source, System.DayOfWeek lastDayOfWeek)
      => source.Next(lastDayOfWeek, true);

    /// <summary>Determines the last day of the year in the source.</summary>
    public static System.DateTime LastDayOfYear(this System.DateTime source)
      => new System.DateTime(source.Year, 12, 31);
  }
}
