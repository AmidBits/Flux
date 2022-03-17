namespace Flux
{
  /// <summary>Cosine interpolation is a smoother and perhaps simplest function. A suitable orientated piece of a cosine function serves to provide a smooth transition between adjacent segments.</summary>
  /// <param name="v1">Source point.</param>
  /// <param name="v2">Target point.</param>
  /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points, the mu range is [0, 1]. Values of mu outside the range result in extrapolation.</param>
  /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
  public class CosineInterpolation
    : IInterpolatable
  {
    private readonly double m_v1, m_v2;

    public CosineInterpolation(double v1, double v2)
    {
      m_v1 = v1;
      m_v2 = v2;
    }

    public double V1
      => m_v1;
    public double V2
      => m_v2;

    public double GetInterpolation(double mu)
      => Interpolate(m_v1, m_v2, mu);

    public static double Interpolate(double v1, double v2, double mu)
    {
      var mu2 = (1.0 - System.Math.Cos(mu * System.Math.PI)) / 2.0;
      
      return v1 * (1.0 - mu2) + v2 * mu2;
    }
  }
}
