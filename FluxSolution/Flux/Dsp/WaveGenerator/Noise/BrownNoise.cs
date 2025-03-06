namespace Flux.Dsp.WaveGenerator.Noise
{
  /// <see href="https://en.wikipedia.org/wiki/Brownian_noise"/>
  /// <seealso cref="http://vellocet.com/dsp/noise/VRand.html"/>
  public sealed class BrownNoise
    : IMonoWaveUiGeneratable, IMonoWavePi2Generatable
  {
    private readonly System.Random m_rng;

    private double m_brown;

    public BrownNoise(System.Random rng) => m_rng = rng ?? System.Random.Shared;

    public BrownNoise() : this(System.Random.Shared) { }

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

    public Waves.IWaveMono<double> GenerateMonoWaveUi(double phaseMu) => (Waves.WaveMono<double>)Sample();

    public Waves.IWaveMono<double> GenerateMonoWavePi2(double phasePi2) => (Waves.WaveMono<double>)Sample();
  }
}
