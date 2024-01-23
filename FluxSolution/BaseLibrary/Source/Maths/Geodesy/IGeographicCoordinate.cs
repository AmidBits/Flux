namespace Flux.Geometry
{
  public interface IGeographicCoordinate
  {
    /// <summary>The height (a.k.a. altitude) of the geographic position in meters.</summary>
    Units.Length Altitude { get; init; }

    /// <summary>The latitude component of the geographic position in degrees. Range from -90.0 (southern hemisphere) to 90.0 degrees (northern hemisphere).</summary>
    Units.Latitude Latitude { get; init; }

    /// <summary>The longitude component of the geographic position in degrees. Range from -180.0 (western half) to 180.0 degrees (eastern half).</summary>
    Units.Longitude Longitude { get; init; }

    double LatitudeInDegrees { get; }
    double LatitudeInRadians { get; }
    double LongitudeInDegrees { get; }
    double LongitudeInRadians { get; }
  }
}
