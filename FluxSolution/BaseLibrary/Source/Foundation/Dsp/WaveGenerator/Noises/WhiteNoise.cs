namespace Flux.Dsp.WaveGenerator
{
  /// <see cref="https://en.wikipedia.org/wiki/White_noise"/>
  public sealed class WhiteNoise
    : IMonoWaveUiGeneratable, IMonoWavePi2Generatable
  {
    private readonly System.Random m_rng;

    public WhiteNoise(System.Random? rng)
      => m_rng = rng ?? new System.Random();
    public WhiteNoise()
      : this(new System.Random())
    { }

    public double Sample()
      => m_rng.NextDouble() * 2 - 1;

    public double GenerateMonoWaveUi(double phaseMu)
      => Sample();
    public double GenerateMonoWavePi2(double phasePi2)
      => Sample();
  }
}
