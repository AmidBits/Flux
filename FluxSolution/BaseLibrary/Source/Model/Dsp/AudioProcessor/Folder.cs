namespace Flux.Dsp.AudioProcessor
{
  public class FolderMono : IAudioProcessorMono
  {
    private double m_polarBias;
    /// <summary>The (polar) bias can be set within the range of [-1, 1].</summary>
    public double PolarBias { get => m_polarBias; set => m_polarBias = Math.Clamp(value, -1.0, 1.0); }

    private double m_multiplier;
    /// <summary>The multiplier can be set within the range of [-1, 1].</summary>
    public double Multiplier
    {
      get => m_multiplier > 1.0 ? (m_multiplier - 1.0) / 9.0 : m_multiplier - 1.0;
      set
      {
        m_multiplier = Math.Clamp(value, -1.0, 1.0);

        if (m_multiplier > Math.EpsilonCpp32)
        {
          m_multiplier = m_multiplier * 9.0 + 1.0;
        }
        else if (m_multiplier < -Math.EpsilonCpp32)
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

    public ISampleMono ProcessAudio(ISampleMono sample) => new MonoSample(Math.Fold(m_multiplier * (sample.FrontCenter + m_polarBias), -1, 1));
  }

  public class FolderStereo : IAudioProcessorStereo
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
    public FolderStereo(double polarBias, double multiplier) : this(polarBias, multiplier, polarBias, multiplier) { }
    public FolderStereo() : this(0.0, 0.0) { }

    public ISampleStereo ProcessAudio(ISampleStereo sample) => new StereoSample(Left.ProcessAudio(new MonoSample(sample.FrontLeft)).FrontCenter, Right.ProcessAudio(new MonoSample(sample.FrontRight)).FrontCenter);
  }
}
