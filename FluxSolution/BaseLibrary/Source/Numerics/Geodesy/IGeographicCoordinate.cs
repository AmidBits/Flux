namespace Flux.Geometry
{
  public interface IGeographicCoordinate
  {
    /// <summary>
    /// <para>The height (a.k.a. altitude) of the geographic position in meters.</para>
    /// </summary>
    double Altitude { get; }

    /// <summary>
    /// <para>Latitude, unit of degree, a component of the geographic position. Range from -90.0 (southern hemisphere) to 90.0 degrees (northern hemisphere).</para>
    /// </summary>
    double Latitude { get; }

    /// <summary>
    /// <para>Longitude, unit of degree, a component of the geographic position. Range from -180.0 (western half) to 180.0 degrees (eastern half).</para>
    /// </summary>
    double Longitude { get; }

    /// <summary>
    /// <para>Latitude in radians.</para>
    /// </summary>
    double LatitudeInRadians { get; }
    /// <summary>
    /// <para>Longitude in radians.</para>
    /// </summary>
    double LongitudeInRadians { get; }
  }
}
