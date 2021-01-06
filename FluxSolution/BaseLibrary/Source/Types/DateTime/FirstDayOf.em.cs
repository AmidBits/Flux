using System.Linq;

namespace Flux
{
  public static partial class SystemDateTimeEm
  {
    /// <summary>Determines the first day of the month in the source.</summary>
    public static System.DateTime FirstDayOfMonth(this System.DateTime source)
      => new System.DateTime(source.Year, source.Month, 1);

    /// <summary>Determines the first day of the current calendar quarter in the source.</summary>
    public static System.DateTime FirstDayOfQuarter(this System.DateTime source)
      => source.GetQuarters().ElementAt(source.QuarterOfYear() - 1).begin;

    /// <summary>Determines the first day of the week in the source, based on the current DateTimeFormatInfo.</summary>
    public static System.DateTime FirstDayOfWeek(this System.DateTime source)
      => source.FirstDayOfWeek(System.Globalization.DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);
    /// <summary>Determines the first day of the week in the source, based on the specified DateTimeFormatInfo.</summary>
    public static System.DateTime FirstDayOfWeek(this System.DateTime source, System.DayOfWeek firstDayOfWeek)
      => source.Previous(firstDayOfWeek, true);

    /// <summary>Determines the first day of the year in the source.</summary>
    public static System.DateTime FirstDayOfYear(this System.DateTime source)
      => new System.DateTime(source.Year, 1, 1);
  }
}
