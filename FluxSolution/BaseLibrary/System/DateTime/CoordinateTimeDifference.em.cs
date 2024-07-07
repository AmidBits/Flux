namespace Flux
{
  public static partial class Fx
  {
    private const double Lg = 6.969290134e-10;

    /// <summary>
    /// <para>A clock that performs exactly the same movements as the Earth but is outside the system's (Earth's) gravity well.</para>
    /// <see href="https://en.wikipedia.org/wiki/Geocentric_Coordinate_Time"/>
    /// </summary>
    public static double GetGeocentricCoordinateTimeDifference(this System.DateTime dateTime)
      => Lg * (dateTime.ToJulianDate(Quantities.TemporalCalendar.GregorianCalendar).Value - 2443144.5003725) * 86400;

    private const double Lb = 1.55051976772e-08;

    /// <summary>
    /// <para>A clock that performs exactly the same movements as the Solar System but is outside the system's (the Solar System's) gravity well.</para>
    /// <see href="https://en.wikipedia.org/wiki/Barycentric_Coordinate_Time"/>
    /// </summary>
    public static double GetBarycentricCoordinateTimeDifference(this System.DateTime dateTime)
      => Lb * (dateTime.ToJulianDate(Quantities.TemporalCalendar.GregorianCalendar).Value - 2443144.5003725) * 86400;
  }
}
