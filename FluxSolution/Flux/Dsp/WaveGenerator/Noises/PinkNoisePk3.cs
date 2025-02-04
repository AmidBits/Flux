namespace Flux.Dsp.WaveGenerator
{
  /// <summary>Pink noise oscillator. Can only be used by instance.</summary>
  /// <remarks>his is an approximation to a -10dB/decade filter using a weighted sum of first order filters.It is accurate to within +/-0.05dB above 9.2Hz (44100Hz sampling rate). Unity gain is at Nyquist, but can be adjusted by scaling the numbers at the end of each line.</remarks>
  /// <see cref="http://www.firstpr.com.au/dsp/pink-noise/#Filtering"/>
  public sealed class PinkNoisePk3
    : IMonoWaveUiGeneratable, IMonoWavePi2Generatable
  {
    private readonly System.Random m_rng;

    private double m_b0, m_b1, m_b2, m_b3, m_b4, m_b5, m_b6;

    public PinkNoisePk3(System.Random rng)
      => m_rng = rng ?? System.Random.Shared;
    public PinkNoisePk3()
      : this(System.Random.Shared)
    { }

    /// <summary>A bipolar (-1 to 1) pink noise sample. The phase is ignored.</summary>
    /// <returns>A pink noise sample inthe -1 to 1 range.</returns>
    public double Sample()
    {
      var white = m_rng.NextDouble(); // The variable 'white' was probably intended to be a new random value each time.

      m_b0 = 0.99886 * m_b0 + white * 0.0555179;
      m_b1 = 0.99332 * m_b1 + white * 0.0750759;
      m_b2 = 0.96900 * m_b2 + white * 0.1538520;
      m_b3 = 0.86650 * m_b3 + white * 0.3104856;
      m_b4 = 0.55000 * m_b4 + white * 0.5329522;
      m_b5 = -0.7616 * m_b5 - white * 0.0168980;

      var pink = m_b0 + m_b1 + m_b2 + m_b3 + m_b4 + m_b5 + m_b6 + white * 0.5362;

      m_b6 = white * 0.115926;

      return (pink);
    }

    public IWaveMono<double> GenerateMonoWaveUi(double phaseMu)
      => (WaveMono<double>)Sample();
    public IWaveMono<double> GenerateMonoWavePi2(double phasePi2)
      => (WaveMono<double>)Sample();
  }
}
