namespace Flux
{
  public static partial class EarthTime
  {
    /// <summary>Average number of days in one Gregorian calendar year.</summary>
    public const double DaysInYearGregorianCalendar = 365.2425;
    /// <summary>Average number of days in one Julian calendar year.</summary>
    public const double DaysInYearJulianCalendar = 365.25;

    /// <summary>Average amount of time in one Gregorian calendar year.</summary>
    public static readonly Quantities.Time TimeInYearGregorianCalendar = new(DaysInYearGregorianCalendar * 86400);
    /// <summary>Average amount of time in one Gregorian calendar month.</summary>
    public static readonly Quantities.Time TimeInMonthGregorianCalendar = new(DaysInYearGregorianCalendar * 86400 / 12);

    /// <summary>Average amount of time in one Julian calendar year.</summary>
    public static readonly Quantities.Time TimeInYearJulianCalendar = new(DaysInYearJulianCalendar * 86400);
    /// <summary>Average amount of time in one Julian calendar month.</summary>
    public static readonly Quantities.Time TimeInMonthJulianCalendar = new(DaysInYearJulianCalendar * 86400 / 12);

    /// <summary>Amount of time in one day (24-hours).</summary>
    public static readonly Quantities.Time TimeInDay = new(86400);

    private const double Lg = 6.969290134e-10;
    /// <summary></summary>
    /// <see href="https://en.wikipedia.org/wiki/Geocentric_Coordinate_Time"/>
    public static double GetGeocentricCoordinateTimeDifference(System.DateTime dateTime)
      => Lg * (dateTime.ToJulianDate(ConversionCalendar.GregorianCalendar).Value - 2443144.5003725) * 86400;

    private const double Lb = 1.55051976772e-08;
    /// <summary></summary>
    /// <see href="https://en.wikipedia.org/wiki/Barycentric_Coordinate_Time"/>
    public static double GetBarycentricCoordinateTimeDifference(System.DateTime dateTime)
      => Lb * (dateTime.ToJulianDate(ConversionCalendar.GregorianCalendar).Value - 2443144.5003725) * 86400;
  }
}