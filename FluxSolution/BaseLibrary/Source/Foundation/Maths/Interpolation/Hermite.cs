
namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Hermite interpolation like cubic requires 4 points so that it can achieve a higher degree of continuity. In addition it has nice tension and biasing controls. Tension can be used to tighten up the curvature at the known points. The bias is used to twist the curve about the known points. The examples shown here have the default tension and bias values of 0, it will be left as an exercise for the reader to explore different tension and bias values.</summary>
    /// <param name="v0">Pre-source point.</param>
    /// <param name="v1">Source point.</param>
    /// <param name="v2">Target point.</param>
    /// <param name="v3">Post-target point.</param>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
    /// <param name="tension">1 is high, 0 normal, -1 is low.</param>
    /// <param name="bias">0 is even, positive is towards first segment, negative towards the other.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double InterpolateHermite(double v0, double v1, double v2, double v3, double mu, double tension, double bias)
    {
      var mu2 = mu * mu;
      var mu3 = mu2 * mu;

      var biasP = (1 + bias) * (1 - tension);
      var biasM = (1 - bias) * (1 - tension);

      var m0 = (v1 - v0) * biasP / 2 + (v2 - v1) * biasM / 2;
      var m1 = (v2 - v1) * biasP / 2 + (v3 - v2) * biasM / 2;

      var a0 = (2 * mu3 - 3 * mu2 + 1);
      var a1 = (mu3 - 2 * mu2 + mu);
      var a2 = (mu3 - mu2);
      var a3 = (-2 * mu3 + 3 * mu2);

      return a0 * v1 + a1 * m0 + a2 * m1 + a3 * v2;
    }
  }
}
