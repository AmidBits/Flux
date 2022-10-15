#if NET7_0_OR_GREATER
namespace Flux.Interpolation
{
  /// <summary>Cosine interpolation is a smoother and perhaps simplest function. A suitable orientated piece of a cosine function serves to provide a smooth transition between adjacent segments.</summary>
  /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
  public readonly struct CubicInterpolationPb<TNode, TMu>
    : I4NodeInterpolatable<TNode, TMu>
    where TNode : System.Numerics.INumber<TNode>
    where TMu : System.Numerics.IFloatingPoint<TMu>, System.Numerics.IMultiplyOperators<TMu, TNode, TMu>
  {
    #region Static methods
    public static TMu Interpolate(TNode n0, TNode n1, TNode n2, TNode n3, TMu mu)
    {
      var two = TMu.One + TMu.One;
      var half = TMu.One / two;
      var oneAndHalf = two - half;

      var mu2 = mu * mu;

      var a0 = -half * n0 + oneAndHalf * n1 - oneAndHalf * n2 + half * n3;
      var a1 = TMu.CreateChecked(n0) - (two + half) * n1 + two * n2 - half * n3;
      var a2 = -half * n0 + half * n2;
      var a3 = TMu.CreateChecked(n1);

      return mu * mu2 * a0 + mu2 * a1 + mu * a2 + a3;
    }
    #endregion Static methods

    #region Implemented interfaces
    public TMu Interpolate4Node(TNode n0, TNode n1, TNode n2, TNode n3, TMu mu)
      => Interpolate(n0, n1, n2, n3, mu);
    #endregion Implemented interfaces
  }
}
#endif
