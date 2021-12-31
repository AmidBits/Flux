namespace Flux.Dsp.AudioProcessor
{
  // https://stackoverflow.com/questions/376036/algorithm-to-mix-sound

  public sealed class MonoBrickWallLimiter
    : IMonoWaveProcessable
  {
    private double m_release = 0.001;
    /// <summary>The amount of attenuation to apply each iteration in order to restore attenuation to full.</summary>
    public double Release { get => m_release; set => m_release = System.Math.Clamp(value, Maths.EpsilonCpp32, 1.0); }

    private double m_autoAttenuation = 1.0;
    /// <summary>The amount of attenuation to apply on the output. This is automatically calculated on sample amplitude overflow.</summary>
    public double AutoAttenuation => m_autoAttenuation;

    public double ProcessAudioMono(double sample)
    {
      if (m_autoAttenuation < 1.0)
      {
        m_autoAttenuation = System.Math.Clamp(m_autoAttenuation + m_release, Maths.EpsilonCpp32, 1.0);
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

    public static double Limit(double sample) => System.Math.Clamp(sample, -1.0, 1.0);

    public double ProcessMonoWave(double sample)
    {
      if (m_autoAttenuation < 1.0)
      {
        m_autoAttenuation = System.Math.Clamp(m_autoAttenuation + m_release, Maths.EpsilonCpp32, 1);
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
}
