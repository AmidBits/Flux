namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Determines the last day of the month in the source.</summary>
    public static System.DateTime LastDayOfMonth(this System.DateTime source)
      => new(source.Year, source.Month, System.DateTime.DaysInMonth(source.Year, source.Month));

    /// <summary>Determines the last day of the quarter in the source.</summary>
    public static System.DateTime LastDayOfQuarter(this System.DateTime source, int quarter)
    {
      return quarter switch
      {
        1 => new(source.Year, 3, 31),
        2 => new(source.Year, 6, 30),
        3 => new(source.Year, 9, 30),
        4 => new(source.Year, 12, 31),
        _ => throw new System.ArgumentOutOfRangeException(nameof(quarter)),
      };
    }
    /// <summary>Determines the last day of the specified quarter.</summary>
    public static System.DateTime LastDayOfQuarter(this System.DateTime source)
      => LastDayOfQuarter(source, source.QuarterOfYear());

    /// <summary>Determines the last day of the week in the source, based on the current DateTimeFormatInfo.</summary>
    public static System.DateTime LastDayOfWeek(this System.DateTime source)
      => LastDayOfWeek(source, (System.DayOfWeek)(((int)System.Globalization.DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek + 6) % 7));
    /// <summary>Determines the last day of the week in the source, based on the specified DateTimeFormatInfo.</summary>
    public static System.DateTime LastDayOfWeek(this System.DateTime source, System.DayOfWeek lastDayOfWeek)
      => NextDayOfWeek(source, lastDayOfWeek, true);

    /// <summary>Determines the last day of the year in the source.</summary>
    public static System.DateTime LastDayOfYear(this System.DateTime source)
      => new(source.Year, 12, 31);
  }
}
