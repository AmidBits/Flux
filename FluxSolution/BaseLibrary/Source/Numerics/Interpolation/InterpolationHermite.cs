namespace Flux.Interpolation
{
  /// <summary>Cosine interpolation is a smoother and perhaps simplest function. A suitable orientated piece of a cosine function serves to provide a smooth transition between adjacent segments.</summary>
  /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
  public record class HermiteInterpolation<TNode, TMu>
    : I4NodeInterpolatable<TNode, TMu>
    where TNode : System.Numerics.INumber<TNode>
    where TMu : System.Numerics.IFloatingPoint<TMu>, System.Numerics.IMultiplyOperators<TMu, TNode, TMu>
  {
    private readonly TMu m_bias;
    private readonly TMu m_tension;

    public HermiteInterpolation(TMu bias, TMu tension)
    {
      m_bias = bias;
      m_tension = tension;
    }

    public TMu Bias { get => m_bias; init => m_bias = value; }
    public TMu Tension { get => m_tension; init => m_tension = value; }

    #region Static methods
    public static TMu Interpolate(TNode n0, TNode n1, TNode n2, TNode n3, TMu mu, TMu tension, TMu bias)
    {
      var one = TMu.One;
      var two = TMu.One.Multiply(2);
      var three = two + one;

      var mu2 = mu * mu;
      var mu3 = mu2 * mu;

      var biasP = (one + bias) * (one - tension);
      var biasM = (one - bias) * (one - tension);

      var m0 = biasP * (n1 - n0) / two + biasM * (n2 - n1) / two;
      var m1 = biasP * (n2 - n1) / two + biasM * (n3 - n2) / two;

      var a0 = two * mu3 - three * mu2 + one;
      var a1 = mu3 - two * mu2 + mu;
      var a2 = mu3 - mu2;
      var a3 = -two * mu3 + three * mu2;

      return a0 * n1 + a1 * m0 + a2 * m1 + a3 * n2;
    }
    #endregion Static methods

    #region Implemented interfaces
    public TMu Interpolate4Node(TNode n0, TNode n1, TNode n2, TNode n3, TMu mu) => Interpolate(n0, n1, n2, n3, mu, m_tension, m_bias);
    #endregion Implemented interfaces
  }
}