using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Determines the first day of the month in the source.</summary>
    public static System.DateTime FirstDayOfMonth(this System.DateTime source)
      => new System.DateTime(source.Year, source.Month, 1);

    /// <summary>Determines the first day of the specified quarter in the source.</summary>
    public static System.DateTime FirstDayOfQuarter(this System.DateTime source, int quarter)
    {
      switch (quarter)
      {
        case 1:
          return new System.DateTime(source.Year, 1, 1);
        case 2:
          return new System.DateTime(source.Year, 4, 1);
        case 3:
          return new System.DateTime(source.Year, 7, 1);
        case 4:
          return new System.DateTime(source.Year, 10, 1);
        default:
          throw new System.ArgumentOutOfRangeException(nameof(quarter));
      }
    }
    /// <summary>Determines the first day of the current calendar quarter in the source.</summary>
    public static System.DateTime FirstDayOfQuarter(this System.DateTime source)
      => FirstDayOfQuarter(source, source.QuarterOfYear());

    /// <summary>Determines the first day of the week in the source, based on the current DateTimeFormatInfo.</summary>
    public static System.DateTime FirstDayOfWeek(this System.DateTime source)
      => source.FirstDayOfWeek(System.Globalization.DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);
    /// <summary>Determines the first day of the week in the source, based on the specified DateTimeFormatInfo.</summary>
    public static System.DateTime FirstDayOfWeek(this System.DateTime source, System.DayOfWeek firstDayOfWeek)
      => source.PreviousDayOfWeek(firstDayOfWeek, true);

    /// <summary>Determines the first day of the year in the source.</summary>
    public static System.DateTime FirstDayOfYear(this System.DateTime source)
      => new System.DateTime(source.Year, 1, 1);
  }
}
