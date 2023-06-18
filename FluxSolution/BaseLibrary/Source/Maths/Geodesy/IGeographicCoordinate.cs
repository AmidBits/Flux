namespace Flux
{
  public static partial class GeometryExtensionMethods
  {
    /// <summary>Converts the geographic coordinates to spherical coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static Geometry.SphericalCoordinate ToSphericalCoordinate(this Geometry.IGeographicCoordinate source)
      => new(
        source.Altitude.Value,
        System.Math.PI - (source.Latitude.Radians + System.Math.PI / 2),
        source.Longitude.Radians + System.Math.PI
      );
  }

  namespace Geometry
  {
    public interface IGeographicCoordinate
    {
      /// <summary>The height (a.k.a. altitude) of the geographic position in meters.</summary>
      Units.Length Altitude { get; init; }
      /// <summary>The latitude component of the geographic position in degrees. Range from -90.0 (southern hemisphere) to 90.0 degrees (northern hemisphere).</summary>
      Units.Latitude Latitude { get; init; }
      /// <summary>The longitude component of the geographic position in degrees. Range from -180.0 (western half) to 180.0 degrees (eastern half).</summary>
      Units.Longitude Longitude { get; init; }
    }
  }
}
