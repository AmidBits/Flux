namespace Flux
{
  public static partial class UnitsExtensions
  {
    //  /// <summary>Returns the approximate number of computed seconds for the instance pro-rata rate. This is by not an exact measurement and used only to compare two instances.</summary>
    //  public static long GetTotalApproximateMilliseconds(this Temporal.Moment source)
    //    => (source.Year * 31536000L + source.Month * 2628000L + source.Day * 86400L + source.Hour * 3600L + source.Minute * 60L + source.Second) * 1000L + source.Millisecond;

    /// <summary>Returns the approximate number of computed seconds for the instance pro-rata rate. This is by not an exact measurement and used only to compare two instances.</summary>
    public static double GetTotalSeconds(this Temporal.Duration source)
    => double.CopySign(double.Abs(source.Years) * Temporal.Duration.AverageSecondsInYear + source.Months * Temporal.Duration.AverageSecondsInMonth + source.Days * Temporal.Duration.SecondsInDay + source.Hours * Temporal.Duration.HoursInDay + source.Minutes * Temporal.Duration.SecondsInMinute + source.Seconds + source.Fractions, source.Years);

    //  public static Temporal.Moment ToMomentUtc(this System.DateTime source)
    //    => new(source.Year, source.Month, source.Day, source.Hour, source.Minute, source.Second, (short)source.Millisecond);
  }
}
