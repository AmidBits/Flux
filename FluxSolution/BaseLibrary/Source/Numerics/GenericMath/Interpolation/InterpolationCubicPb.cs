#if NET7_0_OR_GREATER
namespace Flux
{
  /// <summary>Cosine interpolation is a smoother and perhaps simplest function. A suitable orientated piece of a cosine function serves to provide a smooth transition between adjacent segments.</summary>
  /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
  public readonly struct InterpolationCubicPb<TNode, TMu>
    : I4NodeInterpolatable<TNode, TMu>
    where TNode : System.Numerics.INumber<TNode>
    where TMu : System.Numerics.IFloatingPoint<TMu>, System.Numerics.IMultiplyOperators<TMu, TNode, TMu>
  {
    public TMu Interpolate(TNode v0, TNode v1, TNode v2, TNode v3, TMu mu)
    {
      var two = TMu.One + TMu.One;
      var half = TMu.One / two;
      var oneAndHalf = two - half;

      var mu2 = mu * mu;

      var a0 = -half * v0 + oneAndHalf * v1 - oneAndHalf * v2 + half * v3;
      var a1 = TMu.CreateChecked(v0) - (two + half) * v1 + two * v2 - half * v3;
      var a2 = -half * v0 + half * v2;
      var a3 = TMu.CreateChecked(v1);

      return mu * mu2 * a0 + mu2 * a1 + mu * a2 + a3;
    }
  }
}
#endif
