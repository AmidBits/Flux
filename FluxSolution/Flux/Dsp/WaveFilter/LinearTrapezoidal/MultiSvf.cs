namespace Flux.Dsp.WaveFilter.LinearTrapezoidal
{
  /// <summary>Calculates all filter frequency functions all at once.</summary>
  public record class MultiSvf
    : IMonoWaveFilterable, IMonoWaveProcessable
  {
    /// <summary>Indicates which function to apply to the auto chain through interfaces. All frequency functions are still always computed regardless of this setting.</summary>
    public MultiSvfFrequencyFunction AutoFunction { get; private set; }

    private double m_cutoff;
    /// <value>Typical audio range settings are between 20 to 20,000 Hz, but no restrictions are enforced.</value>
    public double Cutoff { get => m_cutoff; set => DialFilter(value, m_Q, m_sampleRate); }

    private double m_Q;
    /// <value>Typical audio range settings are between 0.1 to 10, but no restrictions are enforced.</value>
    public double Q { get => m_Q; set => DialFilter(m_cutoff, value, m_sampleRate); }

    private double m_sampleRate;
    /// <summary>Sets the sample rate used for filter calculations.</summary>
    public double SampleRate { get => m_sampleRate; set => DialFilter(m_cutoff, m_Q, value); }

    public double m_allPass;
    public double m_bandPass;
    public double m_highPass;
    public double m_lowPass;
    public double m_notch;
    public double m_peak;

    public MultiSvf(double cutoffFrequency, double Q = 0.5, double sampleRate = 44100)
    {
      ClearState();

      DialFilter(cutoffFrequency, Q, sampleRate);
    }

    public double AllPass { get => m_allPass; init => m_allPass = value; }
    public double BandPass { get => m_bandPass; init => m_bandPass = value; }
    public double HighPass { get => m_highPass; init => m_highPass = value; }
    public double LowPass { get => m_lowPass; init => m_lowPass = value; }
    public double Notch { get => m_notch; init => m_notch = value; }
    public double Peak { get => m_peak; init => m_peak = value; }

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
    public void DialFilter(double cutoff, double Q, double sampleRate = 44100)
    {
      m_cutoff = cutoff;
      m_Q = Q;
      m_sampleRate = sampleRate;

      g = double.TanPi(cutoff / sampleRate);
      k = 1.0 / Q;
      a1 = 1.0 / (1.0 + g * (g + k));
      a2 = g * a1;
      a3 = g * a2;
    }

    private double v1, v2, v3;

    public double FilterMonoWave(double v0)
    {
      v3 = v0 - ic2eq;
      v1 = a1 * ic1eq + a2 * v3;
      v2 = ic2eq + a2 * ic1eq + a3 * v3;
      ic1eq = 2.0 * v1 - ic1eq;
      ic1eq = 2.0 * v2 - ic2eq;

      m_lowPass = v2;
      m_bandPass = v1;
      m_highPass = v0 - k * v1 - v2;
      m_notch = LowPass + HighPass;
      m_peak = LowPass - HighPass;
      m_allPass = Notch - k * BandPass;

      return AutoFunction switch
      {
        MultiSvfFrequencyFunction.AllPass => m_allPass,
        MultiSvfFrequencyFunction.BandPass => m_bandPass,
        MultiSvfFrequencyFunction.HighPass => m_highPass,
        MultiSvfFrequencyFunction.LowPass => m_lowPass,
        MultiSvfFrequencyFunction.Notch => m_notch,
        MultiSvfFrequencyFunction.Peak => m_peak,
        _ => throw new NotImplementedException(),
      };
    }

    public IWaveMono<double> FilterMonoWave(IWaveMono<double> mono) => (WaveMono<double>)FilterMonoWave(mono.Wave);

    public IWaveMono<double> ProcessMonoWave(IWaveMono<double> mono) => FilterMonoWave(mono);
  }
}
