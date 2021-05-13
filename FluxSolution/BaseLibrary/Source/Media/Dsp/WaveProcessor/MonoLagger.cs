namespace Flux.Media.Dsp.AudioProcessor
{
  /// <summary>A simple lag function, where the signal is lagged by interpolating the signal over time.</summary>
  public class MonoLagger
    : IWaveProcessorMono
  {
    private double m_amount;
    /// <summary>The amount of lag desired in the range [0, 1], where 0 is no lag and 1 is the most possible.</summary>
    public double Amount { get => m_amount; set => m_amount = System.Math.Clamp(value, 0.0, 1.0); }

    private double m_previousSample;

    public MonoLagger(double amount)
      => m_amount = amount;
    public MonoLagger()
      : this(0.75)
    {
    }

    public double ProcessAudio(double sample)
      => (m_amount > Maths.EpsilonCpp32 ? m_previousSample = Maths.InterpolateCosine(sample, m_previousSample, m_amount) : sample);
  }
}
