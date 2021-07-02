namespace Flux.Randomization
{
  public static class NumberGenerator
  {
    private static readonly System.Threading.ThreadLocal<System.Random> m_crng = new System.Threading.ThreadLocal<System.Random>(() => new Randomization.Cryptographic());
    /// <summary>Gets the standard crypto random number generator (System.Security.Cryptography.RandomNumberGenerator).</summary>
    public static System.Random Crypto
      => m_crng.Value!;

    private static readonly System.Threading.ThreadLocal<System.Random> m_prng = new System.Threading.ThreadLocal<System.Random>(() => new System.Random());
    /// <summary>Gets the standard pseudo random number generator (System.Random).</summary>
    public static System.Random Pseudo
      => m_prng.Value!;

    private static readonly System.Threading.ThreadLocal<System.Random> m_isaac = new System.Threading.ThreadLocal<System.Random>(() => new Randomization.IsaacRandom());
    /// <summary>Gets the IsaacRandom rng.</summary>
    public static System.Random Isaac
      => m_isaac.Value!;

    private static readonly System.Threading.ThreadLocal<System.Random> m_simple = new System.Threading.ThreadLocal<System.Random>(() => new Randomization.SimpleRng());
    /// <summary>Gets the SimpleRng rng.</summary>
    public static System.Random Simple
      => m_simple.Value!;

    private static readonly System.Threading.ThreadLocal<System.Random> m_splitMix64 = new System.Threading.ThreadLocal<System.Random>(() => new Randomization.SplitMix64());
    /// <summary>Gets the SplitMix64 rng.</summary>
    public static System.Random SplitMix64
      => m_splitMix64.Value!;

    private static readonly System.Threading.ThreadLocal<System.Random> m_xoshiro256p = new System.Threading.ThreadLocal<System.Random>(() => new Randomization.Xoshiro256P());
    /// <summary>Gets the Xoshiro256P rng.</summary>
    public static System.Random Xoshiro256P
      => m_xoshiro256p.Value!;

    private static readonly System.Threading.ThreadLocal<System.Random> m_xoshiro256ss = new System.Threading.ThreadLocal<System.Random>(() => new Randomization.Xoshiro256SS());
    /// <summary>Gets the Xoshiro256SS rng.</summary>
    public static System.Random Xoshiro256SS
      => m_xoshiro256ss.Value!;
  }
}
