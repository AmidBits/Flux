// Researching filters while travelling... potantially interesting format filter.
// https://www.musicdsp.org/en/latest/Filters/110-formant-filter.html
//double[][] coeff = {
//  new double[] { 8.11044e-06, 8.943665402, -36.83889529, 92.01697887, -154.337906, 181.6233289, -151.8651235, 89.09614114, -35.10298511, 8.388101016, -0.923313471 }, // A
//  new double[] { 4.36215e-06, 8.90438318, -36.55179099, 91.05750846, -152.422234, 179.1170248, -149.6496211, 87.78352223, -34.60687431, 8.282228154, -0.914150747 }, // E
//  new double[] { 3.33819e-06, 8.893102966, -36.49532826, 90.96543286, -152.4545478, 179.4835618, -150.315433, 88.43409371, -34.98612086, 8.407803364, -0.932568035 }, // I
//  new double[] { 1.13572e-06, 8.994734087, -37.2084849, 93.22900521, -156.6929844, 184.596544, -154.3755513, 90.49663749, -35.58964535, 8.478996281, -0.929252233}, // O
//  new double[] { 4.09431e-07, 8.997322763, -37.20218544, 93.11385476, -156.2530937, 183.7080141, -153.2631681, 89.59539726, -35.12454591, 8.338655623, -0.910251753}, // U
//};

namespace Flux.Dsp.WaveFilters
{
  // https://github.com/vinniefalco/DSPFilters/blob/master/shared/DSPFilters/source/PoleFilter.cpp
  // http://musicdsp.org/files/Audio-EQ-Cookbook.txt

  // http://musicdsp.org/showArchiveComment.php?ArchiveID=240 // Karlsen 24 dB Ladder
  public record class KarlsenFastLadder4P
    : IMonoWaveFilterable, WaveProcessors.IMonoWaveProcessable
  {
    private double m_cutoffFrequency;
    /// <value>Typical audio range settings are between 20 to 20,000 Hz, but no restrictions are enforced.</value>
    public double CutoffFrequency { get => m_cutoffFrequency; set => DialFilter(value, m_resonance, m_sampleRate); }

    private double m_resonance;
    /// <value>Typical audio range settings are between 0.1 to 10, but no restrictions are enforced.</value>
    public double Resonance { get => m_resonance; set => DialFilter(m_cutoffFrequency, value, m_sampleRate); }

    private double m_sampleRate;
    /// <summary>Sets the sample rate used for filter calculations.</summary>
    public double SampleRate { get => m_sampleRate; set => DialFilter(m_cutoffFrequency, m_resonance, value); }

    public KarlsenFastLadder4P(double cutoffFrequency, double resonance, double sampleRate = 44100.0)
    {
      ClearState();

      DialFilter(cutoffFrequency, resonance, sampleRate);
    }

    private double m_buf1, m_buf2, m_buf3, m_buf4;

    public void ClearState()
      => m_buf1 = m_buf2 = m_buf3 = m_buf4 = 0;

    private double m_normalizedCutoffFrequency;

    /// <summary>Abstract method which computes the needed coefficients for the filter in which it is implemented.</summary>
    /// <param name="cutoffFrequency">The filter cutoff frequency, in Hz.</param>
    /// <param name="resonance">Typical audio range settings are between 0.1 to 10, but no restrictions are enforced.</param>
    /// <param name="sampleRate">The sample rate in Hz, defaults to 44.1 kHz.</param>
    public void DialFilter(double cutoffFrequency, double resonance, double sampleRate = 44100)
    {
      m_cutoffFrequency = cutoffFrequency;
      m_resonance = resonance;
      m_sampleRate = sampleRate;

      m_normalizedCutoffFrequency = (double.Pi / 2) * (m_cutoffFrequency / m_sampleRate);
    }

    public double FilterMonoWave(double wave)
    {
      var resclp = m_buf4 > 1 ? 1 : m_buf4; // Clip resonance buffer, if needed.

      // rclp = (-rclp * _resonance) + value; // Asymmetrical clipping (original version by Ove Karlsen).
      resclp = wave - (resclp * m_resonance); // Symmetrical clipping (by Peter Schoffhauzer).

      m_buf1 = (resclp - m_buf1) * m_normalizedCutoffFrequency + m_buf1;
      m_buf2 = (m_buf1 - m_buf2) * m_normalizedCutoffFrequency + m_buf2;
      m_buf3 = (m_buf2 - m_buf3) * m_normalizedCutoffFrequency + m_buf3;
      m_buf4 = (m_buf3 - m_buf4) * m_normalizedCutoffFrequency + m_buf4;

      return m_buf4;
    }

    public Waves.IWaveMono<double> FilterMonoWave(Waves.IWaveMono<double> mono) => (Waves.WaveMono<double>)FilterMonoWave(mono.Wave);

    public Waves.IWaveMono<double> ProcessMonoWave(Waves.IWaveMono<double> mono) => FilterMonoWave(mono);
  }
}
