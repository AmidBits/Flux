namespace Flux.Probability
{
  // A threadsafe, all-static, crypto-randomized wrapper around Random.
  // Still not great, but a slight improvement.
  public static class Pseudorandom
  {
    private readonly static System.Threading.ThreadLocal<System.Random> prng = new System.Threading.ThreadLocal<System.Random>(() => new System.Random(BetterRandom.NextInt()));

    public static int NextInt() => (prng.Value ?? throw new System.NullReferenceException()).Next();

    public static double NextDouble() => (prng.Value ?? throw new System.NullReferenceException()).NextDouble();
  }
}
