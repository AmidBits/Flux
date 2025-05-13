//namespace Flux.Randomness
//{
//  public static class NumberGenerators
//  {
//    private static readonly System.Threading.ThreadLocal<System.Random> m_crng = new(() => Rng64.SscRng.Shared);
//    /// <summary>Gets the standard crypto random number generator (System.Security.Cryptography.RandomNumberGenerator).</summary>
//    public static System.Random DefaultCrypto => m_crng.Value!;

//    private static readonly System.Threading.ThreadLocal<System.Random> m_prng = new(() => System.Random.Shared);
//    /// <summary>Gets the standard pseudo random number generator (System.Random).</summary>
//    public static System.Random DefaultPseudo => m_prng.Value!;

//    private static readonly System.Threading.ThreadLocal<System.Random> m_isaac = new(() => Rng32.IsaacRandom.Shared);
//    /// <summary>Gets the IsaacRandom rng.</summary>
//    public static System.Random DefaultIsaac => m_isaac.Value!;

//    private static readonly System.Threading.ThreadLocal<System.Random> m_simpleRng = new(() => Rng32.SimpleRng.Shared);
//    /// <summary>Gets the SimpleRng rng.</summary>
//    public static System.Random DefaultSimpleRng => m_simpleRng.Value!;

//    private static readonly System.Threading.ThreadLocal<System.Random> m_lecuyer128 = new(() => new Rng64.Lecuyer128());
//    /// <summary>Gets a Lecuyer128 rng.</summary>
//    public static System.Random DefaultLecuyer128 => m_lecuyer128.Value!;

//    private static readonly System.Threading.ThreadLocal<System.Random> m_pakaRng = new(() => Rng64.PakaRng.Shared);
//    /// <summary>Gets a PakaRng rng.</summary>
//    public static System.Random DefaultPakaRng => m_pakaRng.Value!;

//    private static readonly System.Threading.ThreadLocal<System.Random> m_splitMix64 = new(() => Rng64.SplitMix64.Shared);
//    /// <summary>Gets the SplitMix64 rng.</summary>
//    public static System.Random DefaultSplitMix64 => m_splitMix64.Value!;

//    private static readonly System.Threading.ThreadLocal<System.Random> m_xoshiro128p = new(() => Rng32.Xoshiro128P.Shared);
//    /// <summary>Gets the Xoshiro128P rng.</summary>
//    public static System.Random DefaultXoshiro128P => m_xoshiro128p.Value!;

//    private static readonly System.Threading.ThreadLocal<System.Random> m_xoshiro128ss = new(() => Rng32.Xoshiro128SS.Shared);
//    /// <summary>Gets the Xoshiro128SS rng.</summary>
//    public static System.Random DefaultXoshiro128SS => m_xoshiro128ss.Value!;

//    private static readonly System.Threading.ThreadLocal<System.Random> m_xoshiro256p = new(() => Rng64.Xoshiro256P.Shared);
//    /// <summary>Gets the Xoshiro256P rng.</summary>
//    public static System.Random DefaultXoshiro256P => m_xoshiro256p.Value!;

//    private static readonly System.Threading.ThreadLocal<System.Random> m_xoshiro256ss = new(() => Rng64.Xoshiro256SS.Shared);
//    /// <summary>Gets the Xoshiro256SS rng.</summary>
//    public static System.Random DefaultXoshiro256SS => m_xoshiro256ss.Value!;
//  }
//}
