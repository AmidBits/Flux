namespace Flux.Dsp.AudioProcessor
{
  public enum MonoQuadraticMode
  {
    Bypass,
    /// <summary>Apply the quadratic exponent asymmetrically (both peeks will mathematically react differently to the exponent) to the signal.</summary>
    Asymmetric,
    /// <summary>Apply the quadratic exponent asymmetrically (both peeks will mathematically react differently to the exponent) to the also inverted signal.</summary>
    InvertedAsymmetric,
    /// <summary>Apply the quadratic exponent symmetrically (peeks reacts differently) to both peeks.</summary>
    Symmetric,
    /// <summary>Apply the quadratic exponent symmetrically to both peeks.</summary>
    SymmetricInverse
  }

  /// <summary></summary>
  public class MonoQuadratic
    : IWaveProcessorMono
  {
    public MonoQuadraticMode Mode { get; internal set; }

    private double m_exponent, m_exponentExpanded;
    /// <summary>The quadratic exponent is clamped in the range [-1, 1] and is used to transform the sample resulting in a narrower or wider based on the signal amplitude.</summary>
    public double Exponent
    {
      get => m_exponent;
      set
      {
        m_exponent = System.Math.Clamp(value, -1.0, 1.0);

        m_exponentExpanded = (-m_exponent) switch
        {
          var exp when exp > Flux.Maths.EpsilonCpp32 => 1 + -m_exponent * 99,
          var exp when exp < -Flux.Maths.EpsilonCpp32 => 1 + -m_exponent,
          _ => 0,
        };
      }
    }

    public MonoQuadratic(MonoQuadraticMode mode, double exponent)
    {
      Mode = mode;

      Exponent = exponent;
    }
    public MonoQuadratic()
      : this(MonoQuadraticMode.Asymmetric, 0)
    {
    }

    public double ProcessAudio(double sample)
      => (Mode switch
      {
        MonoQuadraticMode.Asymmetric => (System.Math.Pow(sample / 2 + 0.5, m_exponentExpanded) * 2 - 1),
        MonoQuadraticMode.InvertedAsymmetric => (-(System.Math.Pow(-sample / 2 + 0.5, m_exponentExpanded) * 2 - 1)),
        MonoQuadraticMode.Symmetric => (2.0 * ((System.Math.Pow(m_exponent, sample + 1) - 1) / (System.Math.Pow(m_exponent, 2.0) - 1.0)) - 1),
        MonoQuadraticMode.SymmetricInverse => (sample < 0 ? -(2 * ((System.Math.Pow(m_exponent, -sample + 1) - 1) / (System.Math.Pow(m_exponent, 2) - 1.0)) - 1) : 2.0 * ((System.Math.Pow(m_exponent, sample + 1.0) - 1.0) / (System.Math.Pow(m_exponent, 2.0) - 1.0)) - 1),
        _ => (sample),
      });
  }
}