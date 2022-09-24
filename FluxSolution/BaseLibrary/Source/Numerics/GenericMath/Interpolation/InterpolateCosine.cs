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
  }
}
#endif
