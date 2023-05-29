namespace Flux.Dsp.WaveFilter
{
  public record class TripleEq
    : IMonoWaveFilterable, IMonoWaveProcessable
  {
    private const double vsa = (1.0 / uint.MaxValue); // Very small amount (Denormal Fix)

    private readonly double m_lpfCutoff;
    private double m_lpfPole1;
    private double m_lpfPole2;
    private double m_lpfPole3;
    private double m_lpfPole4;

    private readonly double m_hpfCutoff;
    private double m_hpfPole1;
    private double m_hpfPole2;
    private double m_hpfPole3;
    private double m_hpfPole4;

    // Sample history buffer
    private double m_history1; // Sample data minus 1
    private double m_history2; // 2
    private double m_history3; // 3

    private double m_highGain;
    private double m_lowGain;
    private double m_midGain;

    public TripleEq(double lpfCutoff = 880, double hpfCutoff = 5000, double sampleRate = 44100)
    {
      m_lpfCutoff = 2 * System.Math.Sin(System.Math.PI * (lpfCutoff / sampleRate));
      m_lpfPole1 = 0;
      m_lpfPole2 = 0;
      m_lpfPole3 = 0;
      m_lpfPole4 = 0;

      m_hpfCutoff = 2 * System.Math.Sin(System.Math.PI * (hpfCutoff / sampleRate));
      m_hpfPole1 = 0;
      m_hpfPole2 = 0;
      m_hpfPole3 = 0;
      m_hpfPole4 = 0;

      m_history1 = 0;
      m_history2 = 0;
      m_history3 = 0;

      m_highGain = 0;
      m_midGain = 0;
      m_lowGain = 0;
    }

    public double GainHPF { get => m_highGain; set => m_highGain = value; }
    public double GainLPF { get => m_lowGain; set => m_lowGain = value; }
    public double GainMPF { get => m_midGain; set => m_midGain = value; }

    public double FilterMonoWave(double wave)
    {
      m_lpfPole1 += (m_lpfCutoff * (wave - m_lpfPole1)) + vsa;
      m_lpfPole2 += (m_lpfCutoff * (m_lpfPole1 - m_lpfPole2));
      m_lpfPole3 += (m_lpfCutoff * (m_lpfPole2 - m_lpfPole3));
      m_lpfPole4 += (m_lpfCutoff * (m_lpfPole3 - m_lpfPole4));

      double low = m_lpfPole4;

      m_hpfPole1 += (m_hpfCutoff * (wave - m_hpfPole1)) + vsa;
      m_hpfPole2 += (m_hpfCutoff * (m_hpfPole1 - m_hpfPole2));
      m_hpfPole3 += (m_hpfCutoff * (m_hpfPole2 - m_hpfPole3));
      m_hpfPole4 += (m_hpfCutoff * (m_hpfPole3 - m_hpfPole4));

      double high = m_history3 - m_hpfPole4;

      double mid = m_history3 - (high + low); // calculate midrange (signal - (low + high))

      // Scale, Combine and store
      low *= m_lowGain;
      mid *= m_midGain;
      high *= m_highGain;

      // Shuffle history buffer 
      m_history3 = m_history2;
      m_history2 = m_history1;
      m_history1 = wave;

      return low + mid + high;
    }

    public IWaveMono<double> FilterMonoWave(IWaveMono<double> mono) => (WaveMono<double>)FilterMonoWave(mono.Wave);

    public IWaveMono<double> ProcessMonoWave(IWaveMono<double> mono) => FilterMonoWave(mono);
  }
}
