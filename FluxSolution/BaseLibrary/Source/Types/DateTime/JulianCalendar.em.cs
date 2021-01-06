namespace Flux
{
  /// <seealso cref="https://en.wikipedia.org/wiki/Julian_day"/>
  public static partial class SystemDateTimeEm
  {
    public static System.DateTime FromJulianDate(this double julianDay)
      => System.DateTime.FromOADate(julianDay - 2415018.5);
    public static System.DateTime FromModifiedJulianDate(this double modifiedJulianDay)
      => FromJulianDate(modifiedJulianDay + 2400000.5);
    public static System.DateTime FromReducedJulianDate(this double reducedJulianDay)
      => FromJulianDate(reducedJulianDay + 2400000);
    public static System.DateTime FromTruncatedJulianDate(this double truncatedJulianDay)
      => FromJulianDate(truncatedJulianDay + 2440000.5);

    public static double ToJulianDate(this System.DateTime date)
      => date.ToOADate() + 2415018.5;
    public static double ToModifiedJulianDate(this System.DateTime date)
      => date.ToJulianDate() - 2400000.5;
    public static double ToReducedJulianDate(this System.DateTime date)
      => date.ToJulianDate() - 2400000;
    public static double ToTruncatedJulianDate(this System.DateTime date)
      => System.Math.Floor(date.ToJulianDate() - 2440000.5);
  }
}
