namespace Flux.Dsp.WaveGenerator
{
  /// <see cref="https://en.wikipedia.org/wiki/Brownian_noise"/>
  /// <seealso cref="http://vellocet.com/dsp/noise/VRand.html"/>
  public class BrownNoise
    : WhiteNoise
  {
    private double m_brown;

    public BrownNoise(System.Random rng) : base(rng) { }
    public BrownNoise() : base(null) { }

    public override ISampleMono GenerateWave(double phase)
    {
      while (true)
      {
        var r = Rng.NextDouble() - 0.5;

        m_brown += r;

        if (m_brown >= -8 && m_brown <= 8)
        {
          break;
        }

        m_brown -= r;
      }

      return new MonoSample(m_brown * 0.125);
    }
  }
}
