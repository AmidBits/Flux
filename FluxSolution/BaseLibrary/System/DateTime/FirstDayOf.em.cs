namespace Flux
{
  public static partial class ExtensionMethodsDateTime
  {
    /// <summary>Determines the first day of the month in the source.</summary>
    public static System.DateTime FirstDayOfMonth(this System.DateTime source)
      => new(source.Year, source.Month, 1);

    /// <summary>Determines the first day of the specified quarter in the source.</summary>
    public static System.DateTime FirstDayOfQuarter(this System.DateTime source, int quarter)
      => quarter switch
      {
        1 => new(source.Year, 1, 1),
        2 => new(source.Year, 4, 1),
        3 => new(source.Year, 7, 1),
        4 => new(source.Year, 10, 1),
        _ => throw new System.ArgumentOutOfRangeException(nameof(quarter)),
      };
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
      => new(source.Year, 1, 1);
  }
}
