#if NET7_0_OR_GREATER
namespace Flux.Geometry.MapProjections
{
  // https://en.wikipedia.org/wiki/Winkel_tripel_projection
  public readonly record struct WinkelTripelProjection
    : IMapForwardProjectable
  {
    public static readonly WinkelTripelProjection Default;

    //#pragma warning disable CA1822 // Mark members as static
    public System.Numerics.Vector3 ProjectForward(IGeographicCoordinate project)
    {
      var lat = project.LatitudeInRadians;
      var lon = project.LongitudeInRadians;

      var cosLatitude = System.Math.Cos(lat);

      var sinc = Units.Angle.Sincu(System.Math.Acos(cosLatitude * System.Math.Cos(lon / 2)));

      var x = 0.5 * (lon * System.Math.Cos(System.Math.Acos(Maths.PiInto2)) + ((2 * cosLatitude * System.Math.Sin(lon / 2)) / sinc));
      var y = 0.5 * (lat + (System.Math.Sin(lat) / sinc));

      return new((float)x, (float)y, (float)project.Altitude);
    }
    //#pragma warning restore CA1822 // Mark members as static
  }

}
#endif
