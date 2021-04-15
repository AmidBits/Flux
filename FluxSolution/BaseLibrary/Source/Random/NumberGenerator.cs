namespace Flux.Random
{
  public static class NumberGenerator
  {
    private static readonly System.Threading.ThreadLocal<System.Random> m_crng = new System.Threading.ThreadLocal<System.Random>(() => new Flux.Random.Cryptographic());
    /// <summary>Get or set the static pseudo random number generator.</summary>
    public static System.Random Crypto
      => m_crng.Value!;

    private static readonly System.Threading.ThreadLocal<System.Random> m_prng = new System.Threading.ThreadLocal<System.Random>(() => new System.Random());
    /// <summary>Get or set the static pseudo random number generator.</summary>
    public static System.Random Pseudo
      => m_prng.Value!;

    private static readonly System.Threading.ThreadLocal<System.Random> m_isaac = new System.Threading.ThreadLocal<System.Random>(() => new Flux.Random.IsaacRandom());
    /// <summary>Get or set the static pseudo random number generator.</summary>
    public static System.Random Isaac
      => m_isaac.Value!;

    private static readonly System.Threading.ThreadLocal<System.Random> m_simple = new System.Threading.ThreadLocal<System.Random>(() => new Flux.Random.SimpleRng());
    /// <summary>Get or set the static pseudo random number generator.</summary>
    public static System.Random Simple
      => m_simple.Value!;

    private static readonly System.Threading.ThreadLocal<System.Random> m_splitMix64 = new System.Threading.ThreadLocal<System.Random>(() => new Flux.Random.SplitMix64());
    /// <summary>Get or set the static pseudo random number generator.</summary>
    public static System.Random SplitMix64
      => m_splitMix64.Value!;

    private static readonly System.Threading.ThreadLocal<System.Random> m_xoshiro256p = new System.Threading.ThreadLocal<System.Random>(() => new Flux.Random.Xoshiro256P());
    /// <summary>Get or set the static pseudo random number generator.</summary>
    public static System.Random Xoshiro256P
      => m_xoshiro256p.Value!;

    private static readonly System.Threading.ThreadLocal<System.Random> m_xoshiro256ss = new System.Threading.ThreadLocal<System.Random>(() => new Flux.Random.Xoshiro256SS());
    /// <summary>Get or set the static pseudo random number generator.</summary>
    public static System.Random Xoshiro256SS
      => m_xoshiro256ss.Value!;
  }
}
