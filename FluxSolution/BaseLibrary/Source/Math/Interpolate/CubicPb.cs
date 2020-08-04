
namespace Flux
{
  public static partial class Math
  {
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static partial class Interpolate
    {
      /// <summary>Cubic interpolation is the simplest method that offers true continuity between the segments. As such it requires more than just the two endpoints of the segment but also the two points on either side of them.</summary>
      /// <remarks>Paul Breeuwsma proposes the following coefficients for a smoother interpolated curve, which uses the slope between the previous point and the next as the derivative at the current point. The results is what are generally referred to as Catmull-Rom splines.</remarks>
      /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
      public static double CubicPB(double y0, double y1, double y2, double y3, double mu)
      {
        var mu2 = mu * mu;

        var a0 = -0.5 * y0 + 1.5 * y1 - 1.5 * y2 + 0.5 * y3;
        var a1 = y0 - 2.5 * y1 + 2 * y2 - 0.5 * y3;
        var a2 = -0.5 * y0 + 0.5 * y2;
        var a3 = y1;

        return a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3;
      }
    }
  }
}
