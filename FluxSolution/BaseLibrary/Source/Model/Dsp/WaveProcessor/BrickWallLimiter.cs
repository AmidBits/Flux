namespace Flux.Dsp.AudioProcessor
{
  // https://stackoverflow.com/questions/376036/algorithm-to-mix-sound

  public class BrickWallLimiterMono
    : IWaveProcessorMono
  {
    private double m_release = 0.001;
    /// <summary>The amount of attenuation to apply each iteration in order to restore attenuation to full.</summary>
    public double Release { get => m_release; set => m_release = Maths.Clamp(value, Maths.EpsilonCpp32, 1.0); }

    private double m_autoAttenuation = 1.0;
    /// <summary>The amount of attenuation to apply on the output. This is automatically calculated on sample amplitude overflow.</summary>
    public double AutoAttenuation => m_autoAttenuation;

    public double ProcessAudioMono(double sample)
    {
      if (m_autoAttenuation < 1.0)
      {
        m_autoAttenuation = Maths.Clamp(m_autoAttenuation + m_release, Maths.EpsilonCpp32, 1.0);
      }

      if (sample < -1.0 || sample > 1.0)
      {
        if (1.0 / System.Math.Abs(sample) is var attenuationRequired && attenuationRequired < m_autoAttenuation)
        {
          m_autoAttenuation = attenuationRequired;
        }
      }

      return sample * m_autoAttenuation;
    }

    public static double Limit(double sample) => Flux.Maths.Clamp(sample, -1.0, 1.0);

    public double ProcessAudio(double sample)
    {
      if (m_autoAttenuation < 1.0)
      {
        m_autoAttenuation = Maths.Clamp(m_autoAttenuation + m_release, Maths.EpsilonCpp32, 1);
      }

      if (sample < -1 || sample > 1)
      {
        if (1 / System.Math.Abs(sample) is var attenuationRequired && attenuationRequired < m_autoAttenuation)
        {
          m_autoAttenuation = attenuationRequired;
        }
      }

      return (sample * m_autoAttenuation);
    }
  }

  public class BrickWallLimiterStereo
    : IWaveProcessorStereo
  {
    public BrickWallLimiterMono Left { get; }
    public BrickWallLimiterMono Right { get; }

    public double Release { get => Left.Release; set => Right.Release = Left.Release = value; }

    public BrickWallLimiterStereo()
    {
      Left = new BrickWallLimiterMono();
      Right = new BrickWallLimiterMono();
    }

    public StereoSample ProcessAudio(StereoSample sample)
      => new StereoSample(Left.ProcessAudioMono(sample.FrontLeft), Right.ProcessAudioMono(sample.FrontRight));
  }
}
