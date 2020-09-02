namespace Flux.Dsp.AudioProcessor
{
  public class MonoFolder
    : IWaveProcessorMono
  {
    private double m_polarBias;
    /// <summary>The (polar) bias can be set within the range of [-1, 1].</summary>
    public double PolarBias { get => m_polarBias; set => m_polarBias = Maths.Clamp(value, -1.0, 1.0); }

    private double m_multiplier;
    /// <summary>The multiplier can be set within the range of [-1, 1].</summary>
    public double Multiplier
    {
      get => m_multiplier > 1.0 ? (m_multiplier - 1.0) / 9.0 : m_multiplier - 1.0;
      set
      {
        m_multiplier = Maths.Clamp(value, -1.0, 1.0);

        if (m_multiplier > Maths.EpsilonCpp32)
        {
          m_multiplier = m_multiplier * 9.0 + 1.0;
        }
        else if (m_multiplier < -Maths.EpsilonCpp32)
        {
          m_multiplier += 1.0;
        }
      }
    }

    public MonoFolder(double polarBias, double multiplier)
    {
      PolarBias = polarBias;

      Multiplier = multiplier;
    }
    public MonoFolder() : this(0, 0) { }

    public double ProcessAudio(double sample)
      => (Maths.Fold(m_multiplier * (sample + m_polarBias), -1, 1));
  }
}
