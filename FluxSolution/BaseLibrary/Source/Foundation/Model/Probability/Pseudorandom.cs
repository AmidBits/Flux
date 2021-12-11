namespace Flux.Probabilities
{
  // A threadsafe, all-static, crypto-randomized wrapper around Random.
  // Still not great, but a slight improvement.
  public static class Pseudorandom
  {
    private static readonly System.Threading.ThreadLocal<System.Random> prng = new(() => new System.Random(BetterRandom.NextInt()));

    public static int NextInt() => (prng.Value ?? throw new System.NotImplementedException()).Next();

    public static double NextDouble() => (prng.Value ?? throw new System.NotImplementedException()).NextDouble();
  }
}
