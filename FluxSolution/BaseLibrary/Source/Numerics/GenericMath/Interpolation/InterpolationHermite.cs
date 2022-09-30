#if NET7_0_OR_GREATER
namespace Flux
{
  /// <summary>Cosine interpolation is a smoother and perhaps simplest function. A suitable orientated piece of a cosine function serves to provide a smooth transition between adjacent segments.</summary>
  /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
  public struct InterpolationHermite<TNode, TMu>
    : I4NodeInterpolatable<TNode, TMu>
    where TNode : System.Numerics.INumber<TNode>
    where TMu : System.Numerics.IFloatingPoint<TMu>, System.Numerics.IMultiplyOperators<TMu, TNode, TMu>
  {
    private TMu m_bias;
    private TMu m_tension;

    public InterpolationHermite(TMu bias, TMu tension)
    {
      m_bias = bias;
      m_tension = tension;
    }

    public TMu Bias { get => m_bias; init => m_bias = value; }
    public TMu Tension { get => m_tension; init => m_tension = value; }

    public TMu Interpolate(TNode v0, TNode v1, TNode v2, TNode v3, TMu mu)
    {
      var one = TMu.One;
      var two = one + one;
      var three = two + one;

      var mu2 = mu * mu;
      var mu3 = mu2 * mu;

      var biasP = (one + m_bias) * (one - m_tension);
      var biasM = (one - m_bias) * (one - m_tension);

      var m0 = biasP * (v1 - v0) / two + biasM * (v2 - v1) / two;
      var m1 = biasP * (v2 - v1) / two + biasM * (v3 - v2) / two;

      var a0 = two * mu3 - three * mu2 + one;
      var a1 = mu3 - two * mu2 + mu;
      var a2 = mu3 - mu2;
      var a3 = -two * mu3 + three * mu2;

      return a0 * v1 + a1 * m0 + a2 * m1 + a3 * v2;
    }
  }
}
#endif
