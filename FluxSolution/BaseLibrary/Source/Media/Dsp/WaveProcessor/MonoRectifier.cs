namespace Flux.Dsp.AudioProcessor
{
  public enum MonoRectifierMode
  {
    Bypass,
    /// <summary>Perform a positive full wave rectification.</summary>
    FullWave,
    /// <summary>Perform a positive half wave rectification.</summary>
    HalfWave,
    /// <summary>Perform a negative full wave rectification.</summary>
    NegativeFullWave,
    /// <summary>Perform a negative half wave rectification.</summary>
    NegativeHalfWave,
  }

  /// <see cref="https://en.wikipedia.org/wiki/Rectifier"/>
  public class MonoRectifier
    : IWaveProcessorMono
  {
    public MonoRectifierMode Mode { get; }

    private double m_threshold;
    /// <summary>The recifier threshold can be set within the constrained range [-1, 1].</summary>
    public double Threshold { get => m_threshold; set => m_threshold = Maths.Clamp(value, -1.0, 1.0); }

    public MonoRectifier(MonoRectifierMode mode, double threshold)
    {
      Mode = mode;
      Threshold = threshold;
    }
    public MonoRectifier()
      : this(MonoRectifierMode.FullWave, 0.0)
    {
    }

    public double ProcessAudio(double sample)
      => (Mode switch
      {
        MonoRectifierMode.NegativeFullWave when sample > m_threshold => (System.Math.Max(m_threshold - (sample - m_threshold), -1)),
        MonoRectifierMode.NegativeHalfWave when sample > m_threshold => (m_threshold),
        MonoRectifierMode.FullWave when sample < m_threshold => (System.Math.Min(m_threshold + (m_threshold - sample), 1)),
        MonoRectifierMode.HalfWave when sample < m_threshold => (m_threshold),
        _ => (sample),
      });

    public static double RectifyFullWave(double sample, double threshold = 0.0)
      => sample < threshold ? System.Math.Min(threshold + (threshold - sample), 1.0) : sample;
    public static double RectifyHalfWave(double sample, double threshold = 0.0)
      => sample < threshold ? threshold : sample;
  }
}
