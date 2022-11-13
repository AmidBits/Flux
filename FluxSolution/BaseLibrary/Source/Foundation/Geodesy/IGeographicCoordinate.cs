namespace Flux
{
  public interface IGeographicCoordinate
  {
    /// <summary>The height (a.k.a. altitude) of the geographic position in meters.</summary>
    Length Altitude { get; }
    /// <summary>The latitude component of the geographic position in degrees. Range from -90.0 (southern hemisphere) to 90.0 degrees (northern hemisphere).</summary>
    Latitude Latitude { get; }
    /// <summary>The longitude component of the geographic position in degrees. Range from -180.0 (western half) to 180.0 degrees (eastern half).</summary>
    Longitude Longitude { get; }
  }
}
