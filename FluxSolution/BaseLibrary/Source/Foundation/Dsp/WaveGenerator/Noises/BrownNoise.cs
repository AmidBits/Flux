﻿namespace Flux.Dsp.WaveGenerator
{
  /// <see cref="https://en.wikipedia.org/wiki/Brownian_noise"/>
  /// <seealso cref="http://vellocet.com/dsp/noise/VRand.html"/>
  public sealed class BrownNoise
     : IMonoWaveMuGeneratable, IMonoWavePi2Generatable
  {
    private readonly System.Random m_rng;

    private double m_brown;

    public BrownNoise(System.Random rng)
      => m_rng = rng ?? throw new System.ArgumentNullException(nameof(rng));
    public BrownNoise()
      : this(new System.Random())
    { }

    public double Sample()
    {
      while (true)
      {
        var r = m_rng.NextDouble() - 0.5;

        m_brown += r;

        if (m_brown >= -8 && m_brown <= 8)
        {
          break;
        }

        m_brown -= r;
      }

      return (m_brown * 0.125);
    }

    public double GenerateMonoWaveMu(double phaseMu)
      => Sample();
    public double GenerateMonoWavePi2(double phasePi2)
      => Sample();
  }
}