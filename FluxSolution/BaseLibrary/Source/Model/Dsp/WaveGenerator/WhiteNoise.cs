namespace Flux.Dsp.WaveGenerator
{
  /// <see cref="https://en.wikipedia.org/wiki/White_noise"/>
  public class WhiteNoise
    : IWaveGenerator
  {
    protected System.Random Rng { get; set; }

    public WhiteNoise(System.Random? rng)
      => this.Rng = rng ?? new Flux.Random.Xoshiro256SS();
    public WhiteNoise()
      => Rng = new Flux.Random.Xoshiro256SS();

    public virtual MonoSample GenerateWave(double phase2Pi)
      => new MonoSample(Rng.NextDouble() * 2 - 1);
  }
}
