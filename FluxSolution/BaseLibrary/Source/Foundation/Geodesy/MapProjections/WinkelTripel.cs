namespace Flux.MapProjections
{
  // https://en.wikipedia.org/wiki/Winkel_tripel_projection
  public struct WinkelTripelProjection
    : IMapForwardProjectable
  {
    public static readonly WinkelTripelProjection Default;

//#pragma warning disable CA1822 // Mark members as static
    public CartesianCoordinate3 ProjectForward(GeographicCoordinate project)
    {
      var lat = project.Latitude.Radian;
      var lon = project.Longitude.Radian;

      var cosLatitude = System.Math.Cos(lat);

      var sinc = Maths.Sincu(System.Math.Acos(cosLatitude * System.Math.Cos(lon / 2)));

      var x = 0.5 * (lon * System.Math.Cos(System.Math.Acos(Maths.PiInto2)) + ((2 * cosLatitude * System.Math.Sin(lon / 2)) / sinc));
      var y = 0.5 * (lat + (System.Math.Sin(lat) / sinc));

      return new CartesianCoordinate3(x, y, project.Altitude.StandardUnitValue);
    }
//#pragma warning restore CA1822 // Mark members as static
  }

}
