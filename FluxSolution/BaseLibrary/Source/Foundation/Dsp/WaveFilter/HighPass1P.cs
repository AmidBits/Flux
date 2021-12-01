namespace Flux.Dsp.WaveFilter
{
  /// <see cref="http://www.earlevel.com/main/2012/12/15/a-one-pole-filter/"/>
#if NET5_0
  public sealed class HighPass1P
#else
  public record class HighPass1P
#endif
    : IWaveFilterMono, IWaveProcessorMono
  {
    private double m_cutoffFrequency;
    /// <summary>The cutoff frequency is a boundary at which point the frequencies begins to be reduced (attenuated or reflected) rather than passing through.</summary>
    public double CutoffFrequency { get => m_cutoffFrequency; set => SetCoefficient(value, m_sampleRate); }

    private double m_sampleRate;
    /// <summary>Sets the sample rate used for filter calculations.</summary>
    public double SampleRate { get => m_sampleRate; set => SetCoefficient(m_cutoffFrequency, value); }

    public HighPass1P(double cutoff = 5000.0, double sampleRate = 44100.0)
      => SetCoefficient(cutoff, sampleRate);

    private double m_z1;

    public void ClearState()
      => m_z1 = 0.0;

    private double m_a0, m_b1;

    public void SetCoefficient(double cutoffFrequency, double sampleRate)
    {
      m_cutoffFrequency = cutoffFrequency;
      m_sampleRate = sampleRate;

      m_b1 = -System.Math.Exp(-Maths.PiX2 * (0.5 - (cutoffFrequency / sampleRate)));
      m_a0 = 1.0 - m_b1;
    }

    public double FilterAudioMono(double sample)
      => m_z1 = sample * m_a0 + m_z1 * m_b1; // Note the assignment.

    public double ProcessAudio(double sample)
      => FilterAudioMono(sample);

    #region Object overrides.
    public override string ToString()
      => $"{GetType().Name}";
    #endregion Object overrides.
  }
}
