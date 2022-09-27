#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Cubic interpolation is the simplest method that offers true continuity between the segments. As such it requires more than just the two endpoints of the segment but also the two points on either side of them.</summary>
    /// <remarks>Paul Breeuwsma proposes the following coefficients for a smoother interpolated curve, which uses the slope between the previous point and the next as the derivative at the current point. The results is what are generally referred to as Catmull-Rom splines.</remarks>
    /// <param name="v0">Pre-source point.</param>
    /// <param name="v1">Source point.</param>
    /// <param name="v2">Target point.</param>
    /// <param name="v3">Post-target point.</param>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static TSelf InterpolateCubicPb<TSelf, TMu>(this TSelf v0, TSelf v1, TSelf v2, TSelf v3, TMu mu)
      where TSelf : System.Numerics.INumberBase<TSelf>, System.Numerics.IMultiplyOperators<TSelf, TMu, TSelf>
      where TMu : System.Numerics.IFloatingPoint<TMu>
    {
      var two = TSelf.One + TSelf.One;
      var half = TSelf.One / two;
      var oneAndHalf = two - half;

      var mu2 = mu * mu;

      var a0 = -half * v0 + oneAndHalf * v1 - oneAndHalf * v2 + half * v3;
      var a1 = v0 - (two + half) * v1 + two * v2 - half * v3;
      var a2 = -half * v0 + half * v2;
      var a3 = v1;

      return a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3;
    }
  }
}
#endif
