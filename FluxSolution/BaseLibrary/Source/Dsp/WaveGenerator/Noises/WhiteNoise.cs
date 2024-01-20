namespace Flux.Dsp.WaveGenerator
{
  /// <see cref="https://en.wikipedia.org/wiki/White_noise"/>
  public sealed class WhiteNoise
    : IMonoWaveUiGeneratable, IMonoWavePi2Generatable
  {
    private readonly System.Random m_rng;

    public WhiteNoise(System.Random? rng)
      => m_rng = rng ?? System.Random.Shared;
    public WhiteNoise()
      : this(System.Random.Shared)
    { }

    public double Sample()
      => m_rng.NextDouble() * 2 - 1;

    public IWaveMono<double> GenerateMonoWaveUi(double phaseMu)
      => (WaveMono<double>)Sample();
    public IWaveMono<double> GenerateMonoWavePi2(double phasePi2)
      => (WaveMono<double>)Sample();
  }
}
