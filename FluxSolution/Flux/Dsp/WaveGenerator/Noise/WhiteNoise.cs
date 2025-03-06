namespace Flux.Dsp.WaveGenerator.Noise
{
  /// <see href="https://en.wikipedia.org/wiki/White_noise"/>
  public sealed class WhiteNoise
    : IMonoWaveUiGeneratable, IMonoWavePi2Generatable
  {
    private readonly System.Random m_rng;

    public WhiteNoise(System.Random? rng) => m_rng = rng ?? System.Random.Shared;

    public WhiteNoise() : this(System.Random.Shared) { }

    public double Sample() => m_rng.NextDouble() * 2 - 1;

    public Waves.IWaveMono<double> GenerateMonoWaveUi(double phaseMu) => (Waves.WaveMono<double>)Sample();

    public Waves.IWaveMono<double> GenerateMonoWavePi2(double phasePi2) => (Waves.WaveMono<double>)Sample();
  }
}
