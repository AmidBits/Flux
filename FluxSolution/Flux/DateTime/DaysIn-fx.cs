namespace Flux
{
  public static partial class DateTimes
  {
    /// <summary>Determines the number of days in the month of the source.</summary>
    public static int DaysInMonth(this System.DateTime source) => System.DateTime.DaysInMonth(source.Year, source.Month);

    /// <summary>Determines the number of days in the quarter of the source.</summary>
    public static int DaysInQuarter(this System.DateTime source) => source.LastDayOfQuarter().Subtract(source.FirstDayOfQuarter()).Days + 1;

    /// <summary>Determines the number of days in the year of the source.</summary>
    public static int DaysInYear(this System.DateTime source) => System.DateTime.IsLeapYear(source.Year) ? 366 : 365;
  }
}
