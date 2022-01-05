namespace Flux
{
  public class LinearInterpolation
    : ITwoInterpolatable
  {
    public double GetInterpolation(double v1, double v2, double mu)
      => Interpolate(v1, v2, mu);

    /// <summary>Linear interpolation (a.k.a. lerp) is the simplest method of getting values at positions in between the data points. The points are simply joined by straight line segments. Each segment (bounded by two data points) can be interpolated independently. The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</summary>
    /// <param name="v1">Source point.</param>
    /// <param name="v2">Target point.</param>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double Interpolate(double v1, double v2, double mu)
      => v1 * (1 - mu) + v2 * mu;
  }

  //public static partial class Maths
  //{
  //  /// <summary>Linear interpolation (a.k.a. lerp) is the simplest method of getting values at positions in between the data points. The points are simply joined by straight line segments. Each segment (bounded by two data points) can be interpolated independently. The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</summary>
  //  /// <param name="v1">Source point.</param>
  //  /// <param name="v2">Target point.</param>
  //  /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
  //  /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
  //  public static double InterpolateLinear(double v1, double v2, double mu)
  //    => v1 * (1 - mu) + v2 * mu;
  //}
}
