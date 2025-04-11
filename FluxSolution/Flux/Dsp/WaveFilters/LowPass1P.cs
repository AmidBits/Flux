namespace Flux.Dsp.WaveFilters
{
  /// <see cref="http://www.earlevel.com/main/2012/12/15/a-one-pole-filter/"/>
  public record class LowPass1P
    : IMonoWaveFilterable, WaveProcessors.IMonoWaveProcessable
  {
    private double m_cutoffFrequency;
    /// <summary>Sets the cutoff frequency for the filter.</summary>
    public double CutoffFrequency { get => m_cutoffFrequency; set => DialFilter(value, m_sampleRate); }

    private double m_sampleRate;
    /// <summary>Sets the sample rate used for filter calculations.</summary>
    public double SampleRate { get => m_sampleRate; set => DialFilter(m_cutoffFrequency, value); }

    public LowPass1P(double cutoffFrequency = 200.0, double sampleRate = 44100)
      => DialFilter(cutoffFrequency, sampleRate);

    private double m_z1;

    public void ClearState()
      => m_z1 = 0.0;

    private double m_a0, m_b1;

    public void DialFilter(double cutoffFrequency, double sampleRate = 44100)
    {
      m_cutoffFrequency = cutoffFrequency;
      m_sampleRate = sampleRate;

      m_b1 = double.Exp(-double.Tau * (m_cutoffFrequency / m_sampleRate));
      m_a0 = 1.0 - m_b1;
    }

    public double FilterMonoWave(double wave)
      => m_z1 = wave * m_a0 + m_z1 * m_b1; // Note the assignment.

    public Waves.IWaveMono<double> FilterMonoWave(Waves.IWaveMono<double> mono) => (Waves.WaveMono<double>)FilterMonoWave(mono.Wave);

    public Waves.IWaveMono<double> ProcessMonoWave(Waves.IWaveMono<double> mono) => FilterMonoWave(mono);
  }
}
