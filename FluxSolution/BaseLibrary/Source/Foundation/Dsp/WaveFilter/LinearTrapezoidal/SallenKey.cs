namespace Flux.Dsp.WaveFilter.LinearTrapezoidal
{
  /// <summary>A linear trapezoidal integrated Sallen Key filter (SKF) collection</summary>
  /// <summary>A low-pass filter is used to cut unwanted high-frequency signals.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Low-pass_filter"/>
  /// <see cref="https://cytomic.com/files/dsp/SkfLinearTrapOptimised2.pdf"/>
  /// <seealso cref="https://cytomic.com/index.php?q=technical-papers"/>
  public class SallenKey
    : IWaveFilterMono, IWaveProcessorMono
  {
    private double m_cutoff;
    /// <value>Typical audio range settings are between 20 and 20,000 Hz, but no restrictions are enforced.</value>
    public double Cutoff { get => m_cutoff; set => SetCoefficients(value, m_resonance, m_sampleRate); }

    private double m_resonance;
    /// <value>Typical audio range settings are between 0.1 to 10, but no restrictions are enforced.</value>
    public double Resonance { get => m_resonance; set => SetCoefficients(m_cutoff, value, m_sampleRate); }

    private double m_sampleRate;
    /// <summary>Sets the sample rate in Hz, used for filter calculations.</summary>
    public double SampleRate { get => m_sampleRate; set => SetCoefficients(m_cutoff, m_resonance, value); }

    public SallenKey(double cutoff = 200.0, double resonance = 0.5, double sampleRate = 44100.0)
    {
      ClearState();

      SetCoefficients(cutoff, resonance, sampleRate);
    }

    private double ic1eq, ic2eq;

    public void ClearState()
    {
      ic1eq = 0;
      ic2eq = 0;
    }

    private double k, a0, a1, a2, a3, a4, a5;

    /// <summary>Abstract method which computes the needed coefficients for the filter in which it is implemented.</summary>
    /// <param name="cutoff">The filter cutoff frequency, in Hz.</param>
    /// <param name="resonance">The filter resonance [0.0, 1.0].</param>
    /// <param name="sampleRate">The sample rate in Hz, defaults to 44.1 kHz.</param>
    public void SetCoefficients(double cutoff, double resonance, double sampleRate)
    {
      m_cutoff = cutoff;
      m_resonance = resonance;
      m_sampleRate = sampleRate;

      var g = System.Math.Tan(System.Math.PI * (cutoff / sampleRate));
      k = 2.0 * resonance;
      a0 = 1.0 / (System.Math.Pow(1.0 + g, 2.0) - g * k);
      a1 = k * a0;
      a2 = (1.0 + g) * a0;
      a3 = g * a2;
      a4 = g * a0;
      a5 = g * a4;
    }

    private double v1, v2;

    public double FilterAudioMono(double v0)
    {
      v1 = a1 * ic2eq + a2 * ic1eq + a3 * v0;
      v2 = a2 * ic2eq + a4 * ic1eq + a5 * v0;
      ic1eq = 2.0 * (v1 - k * v2) - ic1eq;
      ic2eq = 2.0 * (v2) - ic2eq;

      return v2;
    }

    public double ProcessAudio(double sample)
      => (FilterAudioMono(sample));
  }
}