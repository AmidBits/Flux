#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Cosine interpolation is a smoother and perhaps simplest function. A suitable orientated piece of a cosine function serves to provide a smooth transition between adjacent segments.</summary>
    /// <param name="v1">Source point.</param>
    /// <param name="v2">Target point.</param>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points, the mu range is [0, 1]. Values of mu outside the range result in extrapolation.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static TSelf InterpolateCosine<TSelf, TMu>(this TSelf v1, TSelf v2, TMu mu)
      where TSelf : System.Numerics.INumberBase<TSelf>, System.Numerics.IMultiplyOperators<TSelf, TMu, TSelf>
      where TMu : System.Numerics.IFloatingPoint<TMu>, System.Numerics.ITrigonometricFunctions<TMu>
    {
      var mu2 = (TMu.One - TMu.CosPi(mu)) / (TMu.One + TMu.One);

      return v1 * (TMu.One - mu2) + v2 * mu2;
    }

    /// <summary>Cubic interpolation is the simplest method that offers true continuity between the segments. As such it requires more than just the two endpoints of the segment but also the two points on either side of them.</summary>
    /// <param name="v0">Pre-source point.</param>
    /// <param name="v1">Source point.</param>
    /// <param name="v2">Target point.</param>
    /// <param name="v3">Post-target point.</param>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static TSelf InterpolateCubic<TSelf, TMu>(this TSelf v0, TSelf v1, TSelf v2, TSelf v3, TMu mu)
      where TSelf : System.Numerics.INumberBase<TSelf>, System.Numerics.IMultiplyOperators<TSelf, TMu, TSelf>
      where TMu : System.Numerics.IFloatingPoint<TMu>
    {
      var mu2 = mu * mu;

      var a0 = v3 - v2 - v0 + v1;
      var a1 = v0 - v1 - a0;
      var a2 = v2 - v0;
      var a3 = v1;

      return a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3;
    }

    public static TSelf InterpolateCubicPb<TSelf, TMu>(this TSelf v0, TSelf v1, TSelf v2, TSelf v3, TMu mu)
      where TSelf : System.Numerics.INumberBase<TSelf>, System.Numerics.IMultiplyOperators<TSelf, TMu, TSelf>
      where TMu : System.Numerics.IFloatingPoint<TMu>
    {
      var two = TSelf.One + TSelf.One;
      var halfOfOne = TSelf.One / two;
      var oneAndHalf = two - halfOfOne;
      var twoAndHalf = two + halfOfOne;

      var mu2 = mu * mu;

      var a0 = -halfOfOne * v0 + oneAndHalf * v1 - oneAndHalf * v2 + halfOfOne * v3;
      var a1 = v0 - twoAndHalf * v1 + two * v2 - halfOfOne * v3;
      var a2 = -halfOfOne * v0 + halfOfOne * v2;
      var a3 = v1;

      return a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3;
    }

    /// <summary>PREVIEW! Hermite interpolation like cubic requires 4 points so that it can achieve a higher degree of continuity. In addition it has nice tension and biasing controls. Tension can be used to tighten up the curvature at the known points. The bias is used to twist the curve about the known points. The examples shown here have the default tension and bias values of 0, it will be left as an exercise for the reader to explore different tension and bias values.</summary>
    /// <param name="v0">Pre-source point.</param>
    /// <param name="v1">Source point.</param>
    /// <param name="v2">Target point.</param>
    /// <param name="v3">Post-target point.</param>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
    /// <param name="tension">1 is high, 0 normal, -1 is low.</param>
    /// <param name="bias">0 is even, positive is towards first segment, negative towards the other.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static TSelf InterpolateHermite<TSelf, TMu>(this TSelf v0, TSelf v1, TSelf v2, TSelf v3, TMu mu, TMu tension, TMu bias)
      where TSelf : System.Numerics.INumberBase<TSelf>, System.Numerics.IMultiplyOperators<TSelf, TMu, TSelf>, System.Numerics.IDivisionOperators<TSelf, TMu, TSelf>
      where TMu : System.Numerics.IFloatingPoint<TMu>, System.Numerics.IMultiplyOperators<TMu, TSelf, TSelf>
    {
      var one = TMu.One;
      var two = one + one;
      var three = two + one;

      var mu2 = mu * mu;
      var mu3 = mu2 * mu;

      var biasP = (one + bias) * (one - tension);
      var biasM = (one - bias) * (one - tension);

      var m0 = (v1 - v0) * biasP / two + (v2 - v1) * biasM / two;
      var m1 = (v2 - v1) * biasP / two + (v3 - v2) * biasM / two;

      var a0 = (two * mu3 - three * mu2 + one);
      var a1 = (mu3 - two * mu2 + mu);
      var a2 = (mu3 - mu2);
      var a3 = (-two * mu3 + three * mu2);

      return a0 * v1 + a1 * m0 + a2 * m1 + a3 * v2;
    }

    /// <summary>PREVIEW! Linear interpolation (a.k.a. lerp) is the simplest method of getting values at positions in between the data points. The points are simply joined by straight line segments. Each segment (bounded by two data points) can be interpolated independently. The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</summary>
    public static TSelf InterpolateLinear<TSelf, TMu>(this TSelf v1, TSelf v2, TMu mu)
      where TSelf : System.Numerics.INumberBase<TSelf>, System.Numerics.IMultiplyOperators<TSelf, TMu, TSelf>
      where TMu : System.Numerics.IFloatingPoint<TMu>
      => v1 * (TMu.One - mu) + v2 * mu;
  }
}
#endif
