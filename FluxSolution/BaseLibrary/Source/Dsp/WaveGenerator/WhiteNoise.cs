namespace Flux.Dsp.WaveGenerator
{
  /// <see cref="https://en.wikipedia.org/wiki/White_noise"/>
  public class WhiteNoise
    : IWaveGenerator
  {
    protected System.Random Rng { get; set; }

    public WhiteNoise(System.Random? rng)
      => Rng = rng ?? Flux.Random.NumberGenerator.Crypto;
    public WhiteNoise()
      => Rng = new Flux.Random.Xoshiro256SS();

    public virtual double GenerateWave(double phase2Pi)
      => (Rng.NextDouble() * 2 - 1);
  }
}
