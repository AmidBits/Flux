namespace Flux.Cartography
{
  // https://en.wikipedia.org/wiki/Equal_Earth_projection
  public readonly record struct EqualEarthProjection
    : IMapForwardProjectable, IMapReverseProjectable
  {
    public static readonly EqualEarthProjection Default;

    public System.Numerics.Vector3 ProjectForward(CoordinateSystems.GeographicCoordinate location)
    {
      const double A1 = 1.340264;
      const double A2 = -0.081106;
      const double A3 = 0.000893;
      const double A4 = 0.003796;
      const double A23 = A2 * 3;
      const double A37 = A3 * 7;
      const double A49 = A4 * 9;

      var lat = location.Latitude.Value;
      var lon = location.Longitude.Value;

      var M = double.Sqrt(3) / 2;
      var p = double.Asin(M * double.Sin(lat)); // parametric latitude
      var p2 = double.Pow(p, 2);
      var p6 = double.Pow(p, 6);
      var x = lon * double.Cos(p) / (M * (A1 + A23 * p2 + p6 * (A37 + A49 * p2)));
      var y = p * (A1 + A2 * p2 + p6 * (A3 + A4 * p2));

      return new((float)x, (float)y, (float)location.Altitude.Value);
    }
    public CoordinateSystems.GeographicCoordinate ProjectReverse(System.Numerics.Vector3 location)
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
      var M = double.Sqrt(3) / 2;

      var p = (double)location.Y; // Initial estimate for parametric latitude.
      var dp = 0.0; // No change at start.
      var dy = 0.0;

      for (var i = iterations - 1; i >= 0 && double.Abs(dp) > limit; i--)
      {
        p -= dp;
        var p2 = double.Pow(p, 2);
        var p6 = double.Pow(p, 6);
        var fy = p * (A1 + A2 * p2 + p6 * (A3 + A4 * p2)) - location.Y; // fy is the function you need the root of.
        dy = A1 + A23 * p2 + p6 * (A37 + A49 * p2); // dy is the derivative of the function
        dp = fy / dy; // dp is fy/dy or the change in estimate.
      }

      var lon = M * location.X * dy / double.Cos(p);
      var lat = double.Asin(double.Sin(p) / M);

      return new(
        lat,
        Units.AngleUnit.Radian,
        lon,
        Units.AngleUnit.Radian,
        location.Z,
        Units.LengthUnit.Meter
      );
    }
  }
}
