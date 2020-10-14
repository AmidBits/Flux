namespace Flux.Dsp.WaveFilter.LinearTrapezoidal
{
  /// <summary>Calculates all filter frequency functions akk at once.</summary>
  public class MultiSVF
    : IWaveFilterMono, IWaveProcessorMono
  {
    private double m_cutoff;
    /// <value>Typical audio range settings are between 20 to 20,000 Hz, but no restrictions are enforced.</value>
    public double Cutoff { get => m_cutoff; set => SetCoefficients(value, m_Q, m_sampleRate); }

    private double m_Q;
    /// <value>Typical audio range settings are between 0.1 to 10, but no restrictions are enforced.</value>
    public double Q { get => m_Q; set => SetCoefficients(m_cutoff, value, m_sampleRate); }

    private double m_sampleRate;
    /// <summary>Sets the sample rate used for filter calculations.</summary>
    public double SampleRate { get => m_sampleRate; set => SetCoefficients(m_cutoff, m_Q, value); }

    public double AllPass { get; private set; }
    public double BandPass { get; private set; }
    public double HighPass { get; private set; }
    public double LowPass { get; private set; }
    public double Notch { get; private set; }
    public double Peak { get; private set; }

    public MultiSVF(double cutoffFrequency, double Q = 0.5, double sampleRate = 44100.0)
    {
      ClearState();

      SetCoefficients(cutoffFrequency, Q, sampleRate);
    }

    private double ic1eq, ic2eq;

    public void ClearState()
    {
      ic1eq = 0;
      ic2eq = 0;
    }

    private double g, k, a1, a2, a3;

    /// <summary>Abstract method which computes the needed coefficients for the filter in which it is implemented.</summary>
    /// <param name="cutoff">The filter cutoff frequency, in Hz.</param>
    /// <param name="Q">The filter Q [0.0, 1.0].</param>
    /// <param name="sampleRate">The sample rate in Hz, defaults to 44.1 kHz.</param>
    public void SetCoefficients(double cutoff, double Q, double sampleRate)
    {
      m_cutoff = cutoff;
      m_Q = Q;
      m_sampleRate = sampleRate;

      g = System.Math.Tan(System.Math.PI * (cutoff / sampleRate));
      k = 1.0 / Q;
      a1 = 1.0 / (1.0 + g * (g + k));
      a2 = g * a1;
      a3 = g * a2;
    }

    private double v1, v2, v3;

    public double FilterAudioMono(double v0)
    {
      v3 = v0 - ic2eq;
      v1 = a1 * ic1eq + a2 * v3;
      v2 = ic2eq + a2 * ic1eq + a3 * v3;
      ic1eq = 2.0 * v1 - ic1eq;
      ic1eq = 2.0 * v2 - ic2eq;

      LowPass = v2;
      BandPass = v1;
      HighPass = v0 - k * v1 - v2;
      Notch = LowPass + HighPass;
      Peak = LowPass - HighPass;
      AllPass = Notch - k * BandPass;

      return LowPass;
    }

    public double ProcessAudio(double sample)
      => (FilterAudioMono(sample));
  }
}
