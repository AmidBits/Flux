#if NET7_0_OR_GREATER
namespace Flux.Interpolation
{
  /// <summary>Cosine interpolation is a smoother and perhaps simplest function. A suitable orientated piece of a cosine function serves to provide a smooth transition between adjacent segments.</summary>
  /// <param name="n1">Source point.</param>
  /// <param name="n2">Target point.</param>
  /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points, the mu range is [0, 1]. Values of mu outside the range result in extrapolation.</param>
  /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
  public record class CosineInterpolation<TNode, TMu>
    : I2NodeInterpolatable<TNode, TMu>
    where TNode : System.Numerics.INumber<TNode>
    where TMu : System.Numerics.IFloatingPoint<TMu>, System.Numerics.IMultiplyOperators<TMu, TNode, TMu>, System.Numerics.ITrigonometricFunctions<TMu>
  {
    #region Static methods
    public static TMu Interpolate(TNode n1, TNode n2, TMu mu)
    {
      var mu2 = (TMu.One - TMu.CosPi(mu)).Div2();

      return (TMu.One - mu2) * n1 + mu2 * n2;
    }
    #endregion Static methods

    #region Implemented interfaces
    public TMu Interpolate2Node(TNode n1, TNode n2, TMu mu)
      => Interpolate(n1, n2, mu);
    #endregion Implemented interfaces
  }
}
#endif
