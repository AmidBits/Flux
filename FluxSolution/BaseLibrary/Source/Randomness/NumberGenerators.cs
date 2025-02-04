namespace Flux.Randomness
{
  public static class NumberGenerators
  {
    public static System.Random Default => Crypto;

    private static readonly System.Threading.ThreadLocal<System.Random> m_crng = new(() => Randomness.Rng64.SscRng.Shared);
    /// <summary>Gets the standard crypto random number generator (System.Security.Cryptography.RandomNumberGenerator).</summary>
    public static System.Random Crypto => m_crng.Value!;

    private static readonly System.Threading.ThreadLocal<System.Random> m_prng = new(() => System.Random.Shared);
    /// <summary>Gets the standard pseudo random number generator (System.Random).</summary>
    public static System.Random Pseudo => m_prng.Value!;

    private static readonly System.Threading.ThreadLocal<System.Random> m_isaac = new(() => Randomness.Rng32.IsaacRandom.Shared);
    /// <summary>Gets the IsaacRandom rng.</summary>
    public static System.Random Isaac => m_isaac.Value!;

    private static readonly System.Threading.ThreadLocal<System.Random> m_simple = new(() => Randomness.Rng32.SimpleRng.Shared);
    /// <summary>Gets the SimpleRng rng.</summary>
    public static System.Random Simple => m_simple.Value!;

    private static readonly System.Threading.ThreadLocal<System.Random> m_pakaRng = new(() => Randomness.Rng64.PakaRng.Shared);
    /// <summary>Gets the SplitMix64 rng.</summary>
    public static System.Random PakaRng => m_pakaRng.Value!;

    private static readonly System.Threading.ThreadLocal<System.Random> m_splitMix64 = new(() => Randomness.Rng64.SplitMix64.Shared);
    /// <summary>Gets the SplitMix64 rng.</summary>
    public static System.Random SplitMix64 => m_splitMix64.Value!;

    private static readonly System.Threading.ThreadLocal<System.Random> m_xoshiro128p = new(() => Randomness.Rng32.Xoshiro128P.Shared);
    /// <summary>Gets the Xoshiro256P rng.</summary>
    public static System.Random Xoshiro128P => m_xoshiro128p.Value!;

    private static readonly System.Threading.ThreadLocal<System.Random> m_xoshiro128ss = new(() => Randomness.Rng32.Xoshiro128SS.Shared);
    /// <summary>Gets the Xoshiro256SS rng.</summary>
    public static System.Random Xoshiro128SS => m_xoshiro128ss.Value!;

    private static readonly System.Threading.ThreadLocal<System.Random> m_xoshiro256p = new(() => Randomness.Rng64.Xoshiro256P.Shared);
    /// <summary>Gets the Xoshiro256P rng.</summary>
    public static System.Random Xoshiro256P => m_xoshiro256p.Value!;

    private static readonly System.Threading.ThreadLocal<System.Random> m_xoshiro256ss = new(() => Randomness.Rng64.Xoshiro256SS.Shared);
    /// <summary>Gets the Xoshiro256SS rng.</summary>
    public static System.Random Xoshiro256SS => m_xoshiro256ss.Value!;
  }
}
