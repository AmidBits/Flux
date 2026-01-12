namespace Flux.Dsp.WaveProcessors
{
  /// <summary>Folds the wave by overscaling the wave range [-1, 1] and folding the remainder like an accordion.</summary>
  public record class MonoFolder
    : IMonoWaveProcessable
  {
    private double m_polarBias;
    /// <summary>The (polar) bias can be set within the range of [-1, 1].</summary>
    public double PolarBias
    {
      get => m_polarBias;
      set => m_polarBias = double.Clamp(value, -1.0, 1.0);
    }

    private double m_multiplier;
    /// <summary>The multiplier can be set within the range of [-1, 1].</summary>
    public double Multiplier
    {
      get => m_multiplier > 1.0 ? (m_multiplier - 1.0) / 9.0 : m_multiplier - 1.0;
      set
      {
        m_multiplier = double.Clamp(value, -1.0, 1.0);

        if (m_multiplier > SingleExtensions.MaxDefaultTolerance)
          m_multiplier = m_multiplier * 9.0 + 1.0;
        else if (m_multiplier < SingleExtensions.MinDefaultTolerance)
          m_multiplier += 1.0;
      }
    }

    public MonoFolder(double polarBias, double multiplier)
    {
      PolarBias = polarBias;
      Multiplier = multiplier;
    }
    public MonoFolder() : this(0, 0) { }

    public double ProcessMonoWave(double wave)
      => Numbers.FoldAcross(m_multiplier * (wave + m_polarBias), -1, 1);

    public Waves.IWaveMono<double> ProcessMonoWave(Waves.IWaveMono<double> mono) => (Waves.WaveMono<double>)ProcessMonoWave(mono.Wave);

    public static double ApplyFolder(double sample, double polarBias, double multiplier)
      => Numbers.FoldAcross(multiplier * (sample + polarBias), -1, 1);
  }
}
