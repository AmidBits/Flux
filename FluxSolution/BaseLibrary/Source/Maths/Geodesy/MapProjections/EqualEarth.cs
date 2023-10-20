#if NET7_0_OR_GREATER
namespace Flux.Geometry.MapProjections
{
  // https://en.wikipedia.org/wiki/Equal_Earth_projection
  public readonly record struct EqualEarthProjection
    : IMapForwardProjectable, IMapReverseProjectable
  {
    public static readonly EqualEarthProjection Default;

    //#pragma warning disable CA1822 // Mark members as static
    public CartesianCoordinate3<double> ProjectForward(IGeographicCoordinate location)
    {
      const double A1 = 1.340264;
      const double A2 = -0.081106;
      const double A3 = 0.000893;
      const double A4 = 0.003796;
      const double A23 = A2 * 3;
      const double A37 = A3 * 7;
      const double A49 = A4 * 9;

      var lat = location.LatitudeInRadians;
      var lon = location.LongitudeInRadians;

      var M = System.Math.Sqrt(3) / 2;
      var p = System.Math.Asin(M * System.Math.Sin(lat)); // parametric latitude
      var p2 = System.Math.Pow(p, 2);
      var p6 = System.Math.Pow(p, 6);
      var x = lon * System.Math.Cos(p) / (M * (A1 + A23 * p2 + p6 * (A37 + A49 * p2)));
      var y = p * (A1 + A2 * p2 + p6 * (A3 + A4 * p2));

      return new(x, y, location.Altitude);
    }
    public IGeographicCoordinate ProjectReverse(ICartesianCoordinate3<double> location)
    {
      const double A1 = 1.340264;
      const double A2 = -0.081106;
      const double A3 = 0.000893;
      const double A4 = 0.003796;
      const double A23 = A2 * 3;
      const double A37 = A3 * 7;
      const double A49 = A4 * 9;

      var iterations = 20;
      var limit = 1e-8;
      var M = System.Math.Sqrt(3) / 2;

      var p = location.Y; // Initial estimate for parametric latitude.
      var dp = 0.0; // No change at start.
      var dy = 0.0;

      for (var i = iterations - 1; i >= 0 && System.Math.Abs(dp) > limit; i--)
      {
        p -= dp;
        var p2 = System.Math.Pow(p, 2);
        var p6 = System.Math.Pow(p, 6);
        var fy = p * (A1 + A2 * p2 + p6 * (A3 + A4 * p2)) - location.Y; // fy is the function you need the root of.
        dy = A1 + A23 * p2 + p6 * (A37 + A49 * p2); // dy is the derivative of the function
        dp = fy / dy; // dp is fy/dy or the change in estimate.
      }

      var lon = M * location.X * dy / System.Math.Cos(p);
      var lat = System.Math.Asin(System.Math.Sin(p) / M);

      return new GeographicCoordinate(
        Units.Angle.ConvertRadianToDegree(lat),
        Units.Angle.ConvertRadianToDegree(lon),
        location.Z
      );
    }
    //#pragma warning restore CA1822 // Mark members as static
  }
}
#endif
