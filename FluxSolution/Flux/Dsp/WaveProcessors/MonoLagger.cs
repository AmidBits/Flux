namespace Flux.Dsp.WaveProcessors
{
  /// <summary>A simple lag function, where the signal is lagged by interpolating the signal over time.</summary>
  public record class MonoLagger
    : IMonoWaveProcessable
  {
    private double m_amount;
    /// <summary>The amount of lag desired in the range [0, 1], where 0 is no lag and 1 is the most possible.</summary>
    public double Amount
    {
      get => m_amount;
      set => m_amount = double.Clamp(value, 0.0, 1.0);
    }

    private double m_previousSample;

    public MonoLagger(double amount)
    {
      m_amount = amount;

      m_previousSample = 0;
    }

    public MonoLagger()
      : this(0.75)
    { }

    public double ProcessMonoWave(double wave)
      => (m_amount > SingleExtensions.MaxDefaultTolerance ? m_previousSample = IFloatingPoint.InterpolateCosine(wave, m_previousSample, m_amount) : wave);

    public Waves.IWaveMono<double> ProcessMonoWave(Waves.IWaveMono<double> mono) => (Waves.WaveMono<double>)ProcessMonoWave(mono.Wave);
  }
}
