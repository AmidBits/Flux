namespace Flux.Dsp.WaveProcessor
{
  public enum MonoRectifierMode
  {
    Bypass,
    /// <summary>Perform a positive full (the wave signal below the threshold is mirrored above the threshold) wave rectification.</summary>
    FullWave,
    /// <summary>Perform a positive half (the wave signal below the threshold is removed) wave rectification.</summary>
    HalfWave,
    /// <summary>Perform a negative full (the wave signal above the threshold is mirrored below the threshold) wave rectification.</summary>
    NegativeFullWave,
    /// <summary>Perform a negative half (the wave signal above the threshold is removed) wave rectification.</summary>
    NegativeHalfWave,
  }

  /// <summary>Rectifies (cuts or mirrors) the wave signal above or below a threshold.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Rectifier"/>
  public record class MonoRectifier
    : IMonoWaveProcessable
  {
    public MonoRectifierMode Mode { get; }

    private double m_threshold;
    /// <summary>The recifier threshold can be set within the constrained range [-1, 1].</summary>
    public double Threshold
    {
      get => m_threshold;
      set => m_threshold = System.Math.Clamp(value, -1.0, 1.0);
    }

    public MonoRectifier(MonoRectifierMode mode, double threshold)
    {
      Mode = mode;
      Threshold = threshold;
    }
    public MonoRectifier()
      : this(MonoRectifierMode.FullWave, 0.0)
    { }

    public double ProcessMonoWave(double wave)
      => (Mode switch
      {
        MonoRectifierMode.NegativeFullWave when wave > m_threshold => System.Math.Max(m_threshold - (wave - m_threshold), -1),
        MonoRectifierMode.NegativeHalfWave when wave > m_threshold => m_threshold,
        MonoRectifierMode.FullWave when wave < m_threshold => System.Math.Min(m_threshold + (m_threshold - wave), 1),
        MonoRectifierMode.HalfWave when wave < m_threshold => m_threshold,
        _ => wave,
      });

    public Waves.IWaveMono<double> ProcessMonoWave(Waves.IWaveMono<double> mono) => new Waves.WaveMono<double>(ProcessMonoWave(mono.Wave));

    public static double RectifyFullWave(double sample, double threshold = 0.0)
      => sample < threshold ? System.Math.Min(threshold + (threshold - sample), 1.0) : sample;
    public static double RectifyHalfWave(double sample, double threshold = 0.0)
      => sample < threshold ? threshold : sample;
  }
}
