namespace Flux.Dsp.WaveFilter.LinearTrapezoidal
{
  /// <summary>A linear trapezoidal integrated state variable filter (SVF) collection</summary>
  /// <see cref="https://cytomic.com/index.php?q=technical-papers"/>
  /// <seealso cref="https://cytomic.com/files/dsp/SvfLinearTrapOptimised2.pdf"/>
  public class SingleSvf
    : IWaveFilterMono, IWaveProcessorMono
  {
    public enum SingleSvfFrequencyFunction
    {
      AllPass = Dsp.FrequencyFunction.AllPass,
      BandPass = Dsp.FrequencyFunction.BandPass,
      Bell = Dsp.FrequencyFunction.Bell,
      HighPass = Dsp.FrequencyFunction.HighPass,
      HighShelf = Dsp.FrequencyFunction.HighShelf,
      LowPass = Dsp.FrequencyFunction.LowPass,
      LowShelf = Dsp.FrequencyFunction.LowShelf,
      Notch = Dsp.FrequencyFunction.Notch,
      Peak = Dsp.FrequencyFunction.Peak,
    }

    public SingleSvfFrequencyFunction Function { get; private set; }

    private double m_cutoff;
    /// <value>Typical audio range settings are between 20 to 20,000 Hz, but no restrictions are enforced.</value>
    public double Cutoff { get => m_cutoff; set => SetCoefficients(value, m_Q, m_gain, m_sampleRate); }

    private double m_gain;
    /// <value>Typical audio range settings are between -30 to 30 dB, but no restrictions are enforced.</value>
    public double Gain { get => m_gain; set => SetCoefficients(m_cutoff, m_Q, value, m_sampleRate); }

    private double m_Q;
    /// <value>Typical audio range settings are between 0.1 to 10, but no restrictions are enforced.</value>
    public double Q { get => m_Q; set => SetCoefficients(m_cutoff, value, m_gain, m_sampleRate); }

    private double m_sampleRate;
    /// <summary>Sets the sample rate used for filter calculations.</summary>
    public double SampleRate { get => m_sampleRate; set => SetCoefficients(m_cutoff, m_Q, m_gain, value); }

    public SingleSvf(SingleSvfFrequencyFunction frequencyFunction, double cutoffFrequency, double Q = 0.5, double gain = 0.0, double sampleRate = 44100.0)
    {
      ClearState();

      Function = frequencyFunction;

      SetCoefficients(cutoffFrequency, Q, gain, sampleRate);
    }

    private double ic1eq, ic2eq;

    public void ClearState()
    {
      ic1eq = 0;
      ic2eq = 0;
    }

    private double a1, a2, a3, m0, m1, m2;

    /// <summary>Abstract method which computes the needed coefficients for the filter in which it is implemented.</summary>
    /// <param name="cutoff">The filter cutoff frequency, in Hz.</param>
    /// <param name="Q">The filter Q [0.0, 1.0].</param>
    /// <param name="gain">Gain in dB, where negative numbers are for cut, and positive numbers for boost.</param>
    /// <param name="sampleRate">The sample rate in Hz, defaults to 44.1 kHz.</param>
    private void SetCoefficients(double cutoff, double Q, double gain, double sampleRate)
    {
      m_cutoff = cutoff;
      m_Q = Q;
      m_gain = gain;
      m_sampleRate = sampleRate;

      double g = System.Math.Tan(System.Math.PI * (cutoff / sampleRate));
      double k = 1.0 / Q;

      double A;

      switch (Function)
      {
        case SingleSvfFrequencyFunction.AllPass:
          a1 = 1.0 / (1.0 + g * (g + k));
          a2 = g * a1;
          a3 = g * a2;
          m0 = 1.0;
          m1 = -2.0 * k;
          m2 = 0.0;
          break;
        case SingleSvfFrequencyFunction.BandPass:
          a1 = 1.0 / (1.0 + g * (g + k));
          a2 = g * a1;
          a3 = g * a2;
          m0 = 0.0;
          m1 = 1.0;
          m2 = 0.0;
          break;
        case SingleSvfFrequencyFunction.Bell:
          A = System.Math.Pow(10.0, gain / 40.0);
          k = 1.0 / (Q * A);
          a1 = 1.0 / (1.0 + g * (g + k));
          a2 = g * a1;
          a3 = g * a2;
          m0 = 1.0;
          m1 = k * (A * A - 1.0);
          m2 = 0.0;
          break;
        case SingleSvfFrequencyFunction.HighPass:
          a1 = 1.0 / (1.0 + g * (g + k));
          a2 = g * a1;
          a3 = g * a2;
          m0 = 1.0;
          m1 = -k;
          m2 = -1.0;
          break;
        case SingleSvfFrequencyFunction.HighShelf:
          A = System.Math.Pow(10.0, gain / 40.0);
          g /= System.Math.Sqrt(A);
          a1 = 1.0 / (1.0 + g * (g + k));
          a2 = g * a1;
          a3 = g * a2;
          m0 = A * A;
          m1 = k * (1.0 - A) * A;
          m2 = (1.0 - A * A);
          break;
        case SingleSvfFrequencyFunction.LowPass:
          a1 = 1.0 / (1.0 + g * (g + k));
          a2 = g * a1;
          a3 = g * a2;
          m0 = 0.0;
          m1 = 0.0;
          m2 = 1.0;
          break;
        case SingleSvfFrequencyFunction.LowShelf:
          A = System.Math.Pow(10.0, gain / 40.0);
          g /= System.Math.Sqrt(A);
          a1 = 1.0 / (1.0 + g * (g + k));
          a2 = g * a1;
          a3 = g * a2;
          m0 = 1.0;
          m1 = k * (A - 1.0);
          m2 = (A * A - 1.0);
          break;
        case SingleSvfFrequencyFunction.Notch:
          a1 = 1.0 / (1.0 + g * (g + k));
          a2 = g * a1;
          a3 = g * a2;
          m0 = 1.0;
          m1 = -k;
          m2 = 0.0;
          break;
        case SingleSvfFrequencyFunction.Peak:
          a1 = 1.0 / (1.0 + g * (g + k));
          a2 = g * a1;
          a3 = g * a2;
          m0 = 1;
          m1 = -k;
          m2 = -2;
          break;
        default:
          throw new System.NotImplementedException($"{nameof(Function)}={Function}");
      }
    }

    private double v1, v2, v3;

    public double FilterAudioMono(double v0)
    {
      v3 = v0 - ic2eq;
      v1 = a1 * ic1eq + a2 * v3;
      v2 = ic2eq + a2 * ic1eq + a3 * v3;
      ic1eq = 2.0 * v1 - ic1eq;
      ic2eq = 2.0 * v2 - ic2eq;

      return m0 * v0 + m1 * v1 + m2 * v2;
    }

    public double ProcessAudio(double sample) => (FilterAudioMono(sample));
  }
}
