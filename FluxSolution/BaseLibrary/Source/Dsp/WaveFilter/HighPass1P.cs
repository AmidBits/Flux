namespace Flux.Dsp.WaveFilter
{
  /// <see cref="http://www.earlevel.com/main/2012/12/15/a-one-pole-filter/"/>
  public record class HighPass1P
    : IMonoWaveFilterable, IMonoWaveProcessable
  {
    private double m_cutoffFrequency;
    /// <summary>The cutoff frequency is a boundary at which point the frequencies begins to be reduced (attenuated or reflected) rather than passing through.</summary>
    public double CutoffFrequency { get => m_cutoffFrequency; set => DialFilter(value, m_sampleRate); }

    private double m_sampleRate;
    /// <summary>Sets the sample rate used for filter calculations.</summary>
    public double SampleRate { get => m_sampleRate; set => DialFilter(m_cutoffFrequency, value); }

    public HighPass1P(double cutoff = 5000.0, double sampleRate = 44100.0)
      => DialFilter(cutoff, sampleRate);

    private double m_z1;

    public void ClearState()
      => m_z1 = 0.0;

    private double m_a0, m_b1;

    public void DialFilter(double cutoffFrequency, double sampleRate = 44100)
    {
      m_cutoffFrequency = cutoffFrequency;
      m_sampleRate = sampleRate;

      m_b1 = -System.Math.Exp(-Constants.PiX2 * (0.5 - (cutoffFrequency / sampleRate)));
      m_a0 = 1.0 - m_b1;
    }

    public double FilterMonoWave(double sample)
      => m_z1 = sample * m_a0 + m_z1 * m_b1; // Note the assignment.

    public double ProcessMonoWave(double sample)
      => FilterMonoWave(sample);
  }
}
