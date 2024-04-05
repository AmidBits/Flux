namespace Flux
{
  public static partial class Fx
  {
    private const double Lg = 6.969290134e-10;
    /// <summary></summary>
    /// <see href="https://en.wikipedia.org/wiki/Geocentric_Coordinate_Time"/>
    public static double GetGeocentricCoordinateTimeDifference(this System.DateTime dateTime)
      => Lg * (dateTime.ToJulianDate(Quantities.TemporalCalendar.GregorianCalendar).Value - 2443144.5003725) * 86400;

    private const double Lb = 1.55051976772e-08;
    /// <summary></summary>
    /// <see href="https://en.wikipedia.org/wiki/Barycentric_Coordinate_Time"/>
    public static double GetBarycentricCoordinateTimeDifference(this System.DateTime dateTime)
      => Lb * (dateTime.ToJulianDate(Quantities.TemporalCalendar.GregorianCalendar).Value - 2443144.5003725) * 86400;
  }
}
