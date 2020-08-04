
namespace Flux
{
  public static partial class Math
  {
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static partial class Interpolate
    {
      /// <summary>Hermite interpolation like cubic requires 4 points so that it can achieve a higher degree of continuity. In addition it has nice tension and biasing controls. Tension can be used to tighten up the curvature at the known points. The bias is used to twist the curve about the known points. The examples shown here have the default tension and bias values of 0, it will be left as an exercise for the reader to explore different tension and bias values.</summary>
      /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
      /// <param name="tension">1 is high, 0 normal, -1 is low.</param>
      /// <param name="bias">0 is even, positive is towards first segment, negative towards the other.</param>
      public static double Hermite(double y0, double y1, double y2, double y3, double mu, double tension, double bias)
      {
        var mu2 = mu * mu;
        var mu3 = mu2 * mu;

        var onePbias = 1 + bias;
        var oneMbias = 1 - bias;

        var oneMtension = 1 - tension;

        var m0 = (y1 - y0) * onePbias * oneMtension / 2;
        m0 += (y2 - y1) * oneMbias * oneMtension / 2;
        var m1 = (y2 - y1) * onePbias * oneMtension / 2;
        m1 += (y3 - y2) * oneMbias * oneMtension / 2;

        var a0 = 2 * mu3 - 3 * mu2 + 1;
        var a1 = mu3 - 2 * mu2 + mu;
        var a2 = mu3 - mu2;
        var a3 = -2 * mu3 + 3 * mu2;

        return a0 * y1 + a1 * m0 + a2 * m1 + a3 * y2;
      }
    }
  }
}
