namespace Flux.Dsp.WaveProcessors
{
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
        m_exponent = double.Clamp(value, -1, 1);

        m_exponentExpanded = 0;

        if (m_exponent is > Numerics.Constants.Epsilon1E7 or < -Numerics.Constants.Epsilon1E7)
        {
          if (m_exponent < 0)
            m_exponentExpanded = m_exponent + 1.11;
          else if (m_exponent > 0)
            m_exponentExpanded = m_exponent * 11.1;
        }
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

    private double Asymmetric(double wave, double exponentIndex) => double.Pow(wave / 2.0 + 0.5, exponentIndex) * 2.0 - 1.0;

    private double Symmetric(double wave, double exponent) => ((double.Pow(exponent, wave + 1.0) - 1.0) / (double.Pow(exponent, 2.0) - 1.0)) * 2.0 - 1.0;

    public double ProcessMonoWave(double wave)
      => (Mode switch
      {
        MonoQuadraticMode.Asymmetric => Asymmetric(wave, m_exponentExpanded), // double.Pow(wave / 2.0 + 0.5, m_exponentExpanded) * 2.0 - 1.0,
        MonoQuadraticMode.InvertedAsymmetric => -Asymmetric(-wave, m_exponentExpanded), // -(double.Pow(-wave / 2.0 + 0.5, m_exponentExpanded) * 2.0 - 1.0),
        MonoQuadraticMode.Symmetric => double.IsNegative(m_exponent) ? -Symmetric(-wave, -m_exponent) : Symmetric(wave, m_exponent),
        MonoQuadraticMode.SymmetricInverse => double.IsNegative(m_exponent) ? -Symmetric(-wave, -m_exponent) : Symmetric(wave, m_exponent),
        _ => wave,
      });

    public Waves.IWaveMono<double> ProcessMonoWave(Waves.IWaveMono<double> mono) => (Waves.WaveMono<double>)ProcessMonoWave(mono.Wave);

    /// <summary>Introduces concave curvature (i.e. more narrow across the x axis) to the waveform signal in the range [0, 2PI] (0 = no change and the closer to 2PI the more narrow).</summary>
    public static double ApplyConcavity(double sample, double amountPi2)
      => double.Pow(sample, 1 + amountPi2 * 1.5);
    /// <summary>Introduces convex curvature (i.e. more width across the x axis) to the waveform signal in the (clamped) range [0, 2PI] (0 = no change and the closer to 2PI the more width).</summary>
    public static double ApplyConvexity(double sample, double amountPi2)
      => double.Pow(sample, 1 - 6 / amountPi2);
  }
}
