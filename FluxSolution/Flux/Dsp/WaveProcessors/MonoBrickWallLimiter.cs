namespace Flux.Dsp.WaveProcessors
{
  // https://stackoverflow.com/questions/376036/algorithm-to-mix-sound

  public record class MonoBrickWallLimiter
    : IMonoWaveProcessable
  {
    private double m_release = 0.001;
    /// <summary>The amount of attenuation to apply each iteration in order to restore attenuation to full.</summary>
    public double Release { get => m_release; set => m_release = double.Clamp(value, XtensionSingle.MaxDefaultTolerance, 1.0); }

    private double m_autoAttenuation = 1.0;
    /// <summary>The amount of attenuation to apply on the output. This is automatically calculated on sample amplitude overflow.</summary>
    public double AutoAttenuation => m_autoAttenuation;

    public double ProcessAudioMono(double sample)
    {
      if (m_autoAttenuation < 1.0)
      {
        m_autoAttenuation = double.Clamp(m_autoAttenuation + m_release, XtensionSingle.MaxDefaultTolerance, 1.0);
      }

      if (sample < -1.0 || sample > 1.0)
      {
        if (1.0 / double.Abs(sample) is var attenuationRequired && attenuationRequired < m_autoAttenuation)
        {
          m_autoAttenuation = attenuationRequired;
        }
      }

      return sample * m_autoAttenuation;
    }

    public static double Limit(double sample) => double.Clamp(sample, -1.0, 1.0);

    public double ProcessMonoWave(double wave)
    {
      if (m_autoAttenuation < 1.0)
      {
        m_autoAttenuation = double.Clamp(m_autoAttenuation + m_release, XtensionSingle.MaxDefaultTolerance, 1);
      }

      if (wave < -1 || wave > 1)
      {
        if (1 / double.Abs(wave) is var attenuationRequired && attenuationRequired < m_autoAttenuation)
        {
          m_autoAttenuation = attenuationRequired;
        }
      }

      return (wave * m_autoAttenuation);
    }

    public Waves.IWaveMono<double> ProcessMonoWave(Waves.IWaveMono<double> mono) => (Waves.WaveMono<double>)ProcessMonoWave(mono.Wave);
  }
}
