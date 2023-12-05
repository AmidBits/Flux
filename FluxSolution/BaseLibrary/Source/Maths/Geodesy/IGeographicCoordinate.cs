namespace Flux.Geometry
{
  public interface IGeographicCoordinate
  {
    /// <summary>The height (a.k.a. altitude) of the geographic position in meters.</summary>
    double Altitude { get; init; }

    /// <summary>The latitude component of the geographic position in degrees. Range from -90.0 (southern hemisphere) to 90.0 degrees (northern hemisphere).</summary>
    double Latitude { get; init; }

    /// <summary>The longitude component of the geographic position in degrees. Range from -180.0 (western half) to 180.0 degrees (eastern half).</summary>
    double Longitude { get; init; }

    double LatitudeInRadians { get; }
    double LongitudeInRadians { get; }
  }
}
