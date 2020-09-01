namespace Flux.Dsp.AudioProcessor
{
  public class FolderMono
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

    public FolderMono(double polarBias, double multiplier)
    {
      PolarBias = polarBias;

      Multiplier = multiplier;
    }
    public FolderMono() : this(0, 0) { }

    public double ProcessAudio(double sample)
      => (Maths.Fold(m_multiplier * (sample + m_polarBias), -1, 1));
  }

  public class FolderStereo
    : IWaveProcessorStereo
  {
    public FolderMono Left { get; }
    public FolderMono Right { get; }

    /// <summary>The (polar) bias can be set within the range of [-1, 1].</summary>
    public double PolarBias { get => Left.PolarBias; set => Right.PolarBias = Left.PolarBias = value; }

    /// <summary>The multiplier can be set within the range of [-1, 1].</summary>
    public double Multiplier { get => Left.Multiplier; set => Right.Multiplier = Left.Multiplier = value; }

    public FolderStereo(double polarBiasL, double multiplierL, double polarBiasR, double multiplierR)
    {
      Left = new FolderMono(polarBiasL, multiplierL);
      Right = new FolderMono(polarBiasR, multiplierR);
    }
    public FolderStereo(double polarBias, double multiplier)
      : this(polarBias, multiplier, polarBias, multiplier)
    {
    }
    public FolderStereo()
      : this(0.0, 0.0)
    {
    }

    public StereoSample ProcessAudio(StereoSample sample)
      => new StereoSample(Left.ProcessAudio(sample.FrontLeft), Right.ProcessAudio(sample.FrontRight));
  }
}
