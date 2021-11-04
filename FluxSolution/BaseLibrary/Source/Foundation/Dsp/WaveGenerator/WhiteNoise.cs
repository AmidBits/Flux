namespace Flux.Dsp.WaveGenerator
{
  /// <see cref="https://en.wikipedia.org/wiki/White_noise"/>
  public sealed class WhiteNoise
    : IWaveGenerator
  {
    private System.Random m_rng;

    public WhiteNoise(System.Random? rng)
      => m_rng = rng ?? throw new System.ArgumentNullException(nameof(rng));
    public WhiteNoise()
      : this(new System.Random())
    { }

    public double GenerateWave(double phase2Pi)
      => m_rng.NextDouble() * 2 - 1;

    public static double Sample()
      => 1 - 2 * Randomization.NumberGenerator.Crypto.NextDouble();
  }
}
