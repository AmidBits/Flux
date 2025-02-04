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
  public record class MonoQuadratic
    : IMonoWaveProcessable
  {
    public MonoQuadraticMode Mode { get; internal set; }

    private double m_exponent, m_exponentExpanded;
    /// <summary>The quadratic exponent is clamped in the range [-1, 1] and is used to transform the sample resulting in a narrower or wider based on the signal amplitude.</summary>
    public double Exponent
    {
      get => m_exponent;
      set
      {
        m_exponent = System.Math.Clamp(value, -1, 1);

        m_exponentExpanded = (-m_exponent) switch
        {
          var exp when exp > Numerics.Constants.EpsilonCpp32 => 1 + -m_exponent * 99,
          var exp when exp < -Numerics.Constants.EpsilonCpp32 => 1 + -m_exponent,
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
    { }

    public double ProcessMonoWave(double wave)
      => (Mode switch
      {
        MonoQuadraticMode.Asymmetric => System.Math.Pow(wave / 2 + 0.5, m_exponentExpanded) * 2 - 1,
        MonoQuadraticMode.InvertedAsymmetric => -(System.Math.Pow(-wave / 2 + 0.5, m_exponentExpanded) * 2 - 1),
        MonoQuadraticMode.Symmetric => 2.0 * ((System.Math.Pow(m_exponent, wave + 1) - 1) / (System.Math.Pow(m_exponent, 2.0) - 1.0)) - 1,
        MonoQuadraticMode.SymmetricInverse => wave < 0 ? -(2 * ((System.Math.Pow(m_exponent, -wave + 1) - 1) / (System.Math.Pow(m_exponent, 2) - 1.0)) - 1) : 2.0 * ((System.Math.Pow(m_exponent, wave + 1.0) - 1.0) / (System.Math.Pow(m_exponent, 2.0) - 1.0)) - 1,
        _ => wave,
      });

    public IWaveMono<double> ProcessMonoWave(IWaveMono<double> mono) => (WaveMono<double>)ProcessMonoWave(mono.Wave);

    /// <summary>Introduces concave curvature (i.e. more narrow across the x axis) to the waveform signal in the range [0, 2PI] (0 = no change and the closer to 2PI the more narrow).</summary>
    public static double ApplyConcavity(double sample, double amountPi2)
      => System.Math.Pow(sample, 1 + Tools.AbsolutePhasePi2(amountPi2) * 1.5);
    /// <summary>Introduces convex curvature (i.e. more width across the x axis) to the waveform signal in the (clamped) range [0, 2PI] (0 = no change and the closer to 2PI the more width).</summary>
    public static double ApplyConvexity(double sample, double amountPi2)
      => System.Math.Pow(sample, 1 - 6 / Tools.AbsolutePhasePi2(amountPi2));
  }
}
