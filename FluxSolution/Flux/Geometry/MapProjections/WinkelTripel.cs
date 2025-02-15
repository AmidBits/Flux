namespace Flux.Geometry.MapProjections
{
  // https://en.wikipedia.org/wiki/Winkel_tripel_projection
  public readonly record struct WinkelTripelProjection
    : IMapForwardProjectable
  {
    public static readonly WinkelTripelProjection Default;

    public System.Numerics.Vector3 ProjectForward(CoordinateSystems.GeographicCoordinate project)
    {
      var lat = project.Latitude.Value;
      var lon = project.Longitude.Value;

      var cosLatitude = System.Math.Cos(lat);

      var sinc = Units.Angle.Sincu(System.Math.Acos(cosLatitude * System.Math.Cos(lon / 2)));

      var x = 0.5 * (lon * System.Math.Cos(System.Math.Acos(2 / System.Math.PI)) + ((2 * cosLatitude * System.Math.Sin(lon / 2)) / sinc));
      var y = 0.5 * (lat + (System.Math.Sin(lat) / sinc));

      return new((float)x, (float)y, (float)project.Altitude.Value);
    }
  }

}
