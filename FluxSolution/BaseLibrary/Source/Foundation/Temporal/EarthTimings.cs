namespace Flux
{
  public static class EarthTimings
  {
    public const double AverageDaysInYear = 365.2425;

    public static readonly Quantity.Time AverageTimeInYear = new(AverageDaysInYear * 86400);
    public static readonly Quantity.Time AverageTimeInMonth = new(AverageTimeInYear.DefaultUnitValue / 12);

    public static readonly Quantity.Time TimeInDay = new(86400);

    private const decimal Lg = 6.969290134e-10m;
    public static decimal GetGeocentricCoordinateTimeDifference(System.DateTime dateTime)
      => Lg * (dateTime.ToJulianDate(ConversionCalendar.GregorianCalendar).DefaultUnitValue - 2443144.5003725m) * 86400;

    private const decimal Lb = 1.55051976772e-08m;
    public static decimal GetBarycentricCoordinateTimeDifference(System.DateTime dateTime)
      => Lb * (dateTime.ToJulianDate(ConversionCalendar.GregorianCalendar).DefaultUnitValue - 2443144.5003725m) * 86400;
  }
}
