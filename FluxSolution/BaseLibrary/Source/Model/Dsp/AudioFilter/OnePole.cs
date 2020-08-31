namespace Flux.Dsp.AudioFilter
{
  /// <see cref="http://www.earlevel.com/main/2012/12/15/a-one-pole-filter/"/>
  public class HighPass1P
    : IAudioFilterMono, IAudioProcessorMono
  {
    private double m_cutoff;
    /// <summary>The cutoff frequency is a boundary at which point the frequencies begins to be reduced (attenuated or reflected) rather than passing through.</summary>
    public double Cutoff { get => m_cutoff; set => SetCoefficient(value, m_sampleRate); }

    private double m_sampleRate;
    /// <summary>Sets the sample rate used for filter calculations.</summary>
    public double SampleRate { get => m_sampleRate; set => SetCoefficient(m_cutoff, value); }

    public HighPass1P(double cutoff = 5000.0, double sampleRate = 44100.0)
      => SetCoefficient(cutoff, sampleRate);

    private double m_z1;

    public void ClearState()
      => m_z1 = 0.0;

    private double m_a0, m_b1;

    public void SetCoefficient(double cutoff, double sampleRate)
    {
      m_cutoff = cutoff;
      m_sampleRate = sampleRate;

      m_b1 = -System.Math.Exp(-2.0 * System.Math.PI * (0.5 - (cutoff / sampleRate)));

      m_a0 = 1.0 - m_b1;
    }

    public double FilterAudioMono(double sample)
      => (m_z1 = sample * m_a0 + m_z1 * m_b1);

    public double ProcessAudio(double sample)
      => (FilterAudioMono(sample));
  }

  /// <see cref="http://www.earlevel.com/main/2012/12/15/a-one-pole-filter/"/>
  public class LowPass1P
    : IAudioFilterMono, IAudioProcessorMono
  {
    private double m_cutoff;
    /// <summary>Sets the cutoff frequency for the filter.</summary>
    public double Cutoff { get => m_cutoff; set => SetCoefficient(value, m_sampleRate); }

    private double m_sampleRate;
    /// <summary>Sets the sample rate used for filter calculations.</summary>
    public double SampleRate { get => m_sampleRate; set => SetCoefficient(m_cutoff, value); }

    public LowPass1P(double cutoff = 200.0, double sampleRate = 44100.0)
      => SetCoefficient(cutoff, sampleRate);

    private double m_z1;

    public void ClearState()
      => m_z1 = 0.0;

    private double m_a0, m_b1;

    public void SetCoefficient(double cutoff, double sampleRate)
    {
      m_cutoff = cutoff;
      m_sampleRate = sampleRate;

      m_b1 = System.Math.Exp(-2.0 * System.Math.PI * (cutoff / sampleRate));

      m_a0 = 1.0 - m_b1;
    }

    public double FilterAudioMono(double sample)
      => (m_z1 = sample * m_a0 + m_z1 * m_b1);

    public double ProcessAudio(double sample)
      => (FilterAudioMono(sample));
  }
}
