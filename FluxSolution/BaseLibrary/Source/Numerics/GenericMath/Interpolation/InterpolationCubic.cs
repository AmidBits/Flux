#if NET7_0_OR_GREATER
namespace Flux.Interpolation
{
  /// <summary>Cosine interpolation is a smoother and perhaps simplest function. A suitable orientated piece of a cosine function serves to provide a smooth transition between adjacent segments.</summary>
  /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
  public record class CubicInterpolation<TNode, TMu>
    : I4NodeInterpolatable<TNode, TMu>
    where TNode : System.Numerics.INumber<TNode>
    where TMu : System.Numerics.IFloatingPoint<TMu>, System.Numerics.IAdditionOperators<TMu, TNode, TMu>, System.Numerics.IMultiplyOperators<TMu, TNode, TMu>
  {
    #region Static methods
    public TMu Interpolate(TNode n0, TNode n1, TNode n2, TNode n3, TMu mu)
    {
      var mu2 = mu * mu;

      var a0 = n3 - n2 - n0 + n1;
      var a1 = n0 - n1 - a0;
      var a2 = n2 - n0;
      var a3 = n1;

      return mu * mu2 * a0 + mu2 * a1 + mu * a2 + a3;
    }
    #endregion Static methods

    #region Implemented interfaces
    public TMu Interpolate4Node(TNode n0, TNode n1, TNode n2, TNode n3, TMu mu)
      => Interpolate4Node(n0, n1, n2, n3, mu);
    #endregion Implemented interfaces
  }
}
#endif
