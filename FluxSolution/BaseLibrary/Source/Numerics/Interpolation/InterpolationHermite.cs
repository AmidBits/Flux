namespace Flux.Interpolation
{
#if NET7_0_OR_GREATER

  /// <summary>Cosine interpolation is a smoother and perhaps simplest function. A suitable orientated piece of a cosine function serves to provide a smooth transition between adjacent segments.</summary>
  /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
  public record class HermiteInterpolation<TSelf>
    : I4NodeInterpolatable<TSelf>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>
  {
    private readonly TSelf m_bias;
    private readonly TSelf m_tension;

    public HermiteInterpolation(TSelf bias, TSelf tension)
    {
      m_bias = bias;
      m_tension = tension;
    }

    public TSelf Bias { get => m_bias; init => m_bias = value; }
    public TSelf Tension { get => m_tension; init => m_tension = value; }

    #region Static methods

    public static TSelf Interpolate(TSelf y0, TSelf y1, TSelf y2, TSelf y3, TSelf mu, TSelf tension, TSelf bias)
    {
      var one = TSelf.One;
      var two = TSelf.One + TSelf.One;
      var three = two + one;

      var mu2 = mu * mu;
      var mu3 = mu2 * mu;

      var biasP = (TSelf.One + bias) * (TSelf.One - tension);
      var biasN = (TSelf.One - bias) * (TSelf.One - tension);

      var m0 = (y1 - y0) * biasP / two + (y2 - y1) * biasN / two;
      var m1 = (y2 - y1) * biasP / two + (y3 - y2) * biasN / two;

      var a0 = two * mu3 - three * mu2 + one;
      var a1 = mu3 - two * mu2 + mu;
      var a2 = mu3 - mu2;
      var a3 = -two * mu3 + three * mu2;

      return a0 * y1 + a1 * m0 + a2 * m1 + a3 * y2;
    }

    #endregion Static methods

    #region Implemented interfaces

    public TSelf Interpolate4Node(TSelf y0, TSelf y1, TSelf y2, TSelf y3, TSelf mu)
      => Interpolate(y0, y1, y2, y3, mu, m_tension, m_bias);

    #endregion Implemented interfaces
  }

#else

  /// <summary>Cosine interpolation is a smoother and perhaps simplest function. A suitable orientated piece of a cosine function serves to provide a smooth transition between adjacent segments.</summary>
  /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
  public record class HermiteInterpolation
    : I4NodeInterpolatable
  {
    private readonly double m_bias;
    private readonly double m_tension;

    public HermiteInterpolation(double bias, double tension)
    {
      m_bias = bias;
      m_tension = tension;
    }

    public double Bias { get => m_bias; init => m_bias = value; }
    public double Tension { get => m_tension; init => m_tension = value; }

    #region Static methods

    public static double Interpolate(double y0, double y1, double y2, double y3, double mu, double tension, double bias)
    {
      var one = 1d;
      var two = 2d;
      var three = two + one;

      var mu2 = mu * mu;
      var mu3 = mu2 * mu;

      var biasP = (1 + bias) * (1 - tension);
      var biasN = (1 - bias) * (1 - tension);

      var m0 = (y1 - y0) * biasP / two + (y2 - y1) * biasN / two;
      var m1 = (y2 - y1) * biasP / two + (y3 - y2) * biasN / two;

      var a0 = two * mu3 - three * mu2 + one;
      var a1 = mu3 - two * mu2 + mu;
      var a2 = mu3 - mu2;
      var a3 = -two * mu3 + three * mu2;

      return a0 * y1 + a1 * m0 + a2 * m1 + a3 * y2;
    }

    #endregion Static methods

    #region Implemented interfaces

    public double Interpolate4Node(double y0, double y1, double y2, double y3, double mu)
      => Interpolate(y0, y1, y2, y3, mu, m_tension, m_bias);

    #endregion Implemented interfaces
  }

#endif
}
