namespace Flux.Dsp.WaveFilter
{
  // https://github.com/vinniefalco/DSPFilters/blob/master/shared/DSPFilters/source/PoleFilter.cpp
  // http://musicdsp.org/files/Audio-EQ-Cookbook.txt

  // http://musicdsp.org/showArchiveComment.php?ArchiveID=240 // Karlsen 24 dB Ladder
  public record class KarlsenFastLadder4P
    : IMonoWaveFilterable, IMonoWaveProcessable
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

      m_normalizedCutoffFrequency = Maths.PiOver2 * (m_cutoffFrequency / m_sampleRate);
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

    public IWaveMono<double> FilterMonoWave(IWaveMono<double> mono) => (WaveMono<double>)FilterMonoWave(mono.Wave);

    public IWaveMono<double> ProcessMonoWave(IWaveMono<double> mono) => FilterMonoWave(mono);
  }
}
