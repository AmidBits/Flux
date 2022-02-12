namespace Flux
{
  public static class EarthTimings
  {
    public const double AverageDaysInYear = 365.2425;

    public static readonly Time AverageTimeInYear = new(AverageDaysInYear * 86400);
    public static readonly Time AverageTimeInMonth = new(AverageTimeInYear.Value / 12);

    public static readonly Time TimeInDay = new(86400);

    private const double Lg = 6.969290134e-10;
    public static double GetGeocentricCoordinateTimeDifference(System.DateTime dateTime)
      => Lg * (dateTime.ToJulianDate(ConversionCalendar.GregorianCalendar).Value - 2443144.5003725) * 86400;

    private const double Lb = 1.55051976772e-08;
    public static double GetBarycentricCoordinateTimeDifference(System.DateTime dateTime)
      => Lb * (dateTime.ToJulianDate(ConversionCalendar.GregorianCalendar).Value - 2443144.5003725) * 86400;
  }
}
