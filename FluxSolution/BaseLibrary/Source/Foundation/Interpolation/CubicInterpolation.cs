namespace Flux
{
  public class CubicInterpolation
    : IFourfoldInterpolatable
  {
    public double GetInterpolation(double v0, double v1, double v2, double v3, double mu)
      => Interpolate(v0, v1, v2, v3, mu);

    /// <summary>Cubic interpolation is the simplest method that offers true continuity between the segments. As such it requires more than just the two endpoints of the segment but also the two points on either side of them.</summary>
    /// <param name="v0">Pre-source point.</param>
    /// <param name="v1">Source point.</param>
    /// <param name="v2">Target point.</param>
    /// <param name="v3">Post-target point.</param>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double Interpolate(double v0, double v1, double v2, double v3, double mu)
    {
      var mu2 = mu * mu;

      var a0 = v3 - v2 - v0 + v1;
      var a1 = v0 - v1 - a0;
      var a2 = v2 - v0;
      var a3 = v1;

      return (a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3);
    }
  }

  //public static partial class Maths
  //{
  //  /// <summary>Cubic interpolation is the simplest method that offers true continuity between the segments. As such it requires more than just the two endpoints of the segment but also the two points on either side of them.</summary>
  //  /// <param name="v0">Pre-source point.</param>
  //  /// <param name="v1">Source point.</param>
  //  /// <param name="v2">Target point.</param>
  //  /// <param name="v3">Post-target point.</param>
  //  /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
  //  /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
  //  public static double InterpolateCubic(double v0, double v1, double v2, double v3, double mu)
  //  {
  //    var mu2 = mu * mu;

  //    var a0 = v3 - v2 - v0 + v1;
  //    var a1 = v0 - v1 - a0;
  //    var a2 = v2 - v0;
  //    var a3 = v1;

  //    return (a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3);
  //  }
  //}
}