namespace Flux.Cartography
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

      var cosLatitude = double.Cos(lat);

      var sinc = double.Sincu(double.Acos(cosLatitude * double.Cos(lon / 2)));

      var x = 0.5 * (lon * double.Cos(double.Acos(2 / double.Pi)) + ((2 * cosLatitude * double.Sin(lon / 2)) / sinc));
      var y = 0.5 * (lat + (double.Sin(lat) / sinc));

      return new((float)x, (float)y, (float)project.Altitude.Value);
    }
  }

}
