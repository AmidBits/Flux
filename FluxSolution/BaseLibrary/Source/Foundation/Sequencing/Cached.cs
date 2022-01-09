//namespace Flux.Numerics
//{
//  public static class CachedSequences
//  {
//    /// <summary>Returns an array of the first (23) colossaly abundant numbers that fits in a 64-bit integer.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Colossally_abundant_number"/>
//    public static System.ReadOnlySpan<long> ColossallyAbundantNumbers23
//      => new long[] { 2, 6, 12, 60, 120, 360, 2520, 5040, 55440, 720720, 1441440, 4324320, 21621600, 367567200, 6983776800, 160626866400, 321253732800, 9316358251200, 288807105787200, 2021649740510400, 6064949221531200, 224403121196654400, 9200527969062830400 };

//    /// <summary>A sequence of all Fermat primes.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Fermat_number#Primality_of_Fermat_numbers"/>
//    public static System.ReadOnlySpan<int> FermatPrimes
//      => new int[] { 3, 5, 17, 257, 65537 };

//    /// <summary>A sequence of all minimal primes.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Minimal_prime_(recreational_mathematics)"/>
//    public static System.ReadOnlySpan<int> MinimalPrimes
//      => new int[] { 2, 3, 5, 7, 11, 19, 41, 61, 89, 409, 449, 499, 881, 991, 6469, 6949, 9001, 9049, 9649, 9949, 60649, 666649, 946669, 60000049, 66000049, 66600049 };

//    /// <summary>A sequence of all right truncatable primes.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Truncatable_prime"/>
//    public static System.ReadOnlySpan<int> RightTruncatablePrimes
//      => new int[] { 2, 3, 5, 7, 23, 29, 31, 37, 53, 59, 71, 73, 79, 233, 239, 293, 311, 313, 317, 373, 379, 593, 599, 719, 733, 739, 797, 2333, 2339, 2393, 2399, 2939, 3119, 3137, 3733, 3739, 3793, 3797, 5939, 7193, 7331, 7333, 7393, 23333, 23339, 23399, 23993, 29399, 31193, 31379, 37337, 37339, 37397, 59393, 59399, 71933, 73331, 73939, 233993, 239933, 293999, 373379, 373393, 593933, 593993, 719333, 739391, 739393, 739397, 739399, 2339933, 2399333, 2939999, 3733799, 5939333, 7393913, 7393931, 7393933, 23399339, 29399999, 37337999, 59393339, 73939133 };

//    /// <summary>A sequence of all stern primes.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Stern_prime"/>
//    public static System.ReadOnlySpan<short> SternPrimes
//      => new short[] { 2, 3, 17, 137, 227, 977, 1187, 1493 };

//    /// <summary>Returns an array of the first (23) superior highly composite numbers that fits in a 64-bit integer.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Superior_highly_composite_number"/>
//    public static System.ReadOnlySpan<long> SuperiorHighlyCompositeNumbers23
//      => new long[] { 2, 6, 12, 60, 120, 360, 2520, 5040, 55440, 720720, 1441440, 4324320, 21621600, 367567200, 6983776800, 13967553600, 321253732800, 2248776129600, 65214507758400, 195643523275200, 6064949221531200, 12129898443062400, 448806242393308800 };

//    /// <summary>A sequence of all supersingular primes.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Supersingular_prime_(moonshine_theory)"/>
//    public static System.ReadOnlySpan<byte> SupersingularPrimes
//      => new byte[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 41, 47, 59, 71 };

//    /// <summary>A sequence of all two-sided primes.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Truncatable_prime"/>
//    public static System.ReadOnlySpan<int> TwoSidedPrimes
//      => new int[] { 2, 3, 5, 7, 23, 37, 53, 73, 313, 317, 373, 797, 3137, 3797, 739397 };

//    /// <summary>A sequence of all unique primes.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Unique_prime"/>
//    public static System.ReadOnlySpan<System.Numerics.BigInteger> UniquePrimes
//      => new System.Numerics.BigInteger[] { 3, 11, 37, 101, 9091, 9901, 333667, 909091, 99990001, 999999000001, 9999999900000001, 909090909090909091, 1111111111111111111, System.Numerics.BigInteger.Parse("11111111111111111111111", System.Globalization.CultureInfo.CurrentCulture), System.Numerics.BigInteger.Parse("900900900900990990990991", System.Globalization.CultureInfo.CurrentCulture), System.Numerics.BigInteger.Parse("909090909090909090909090909091", System.Globalization.CultureInfo.CurrentCulture) };

//    /// <summary>A sequence of all Wilson primes.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Wilson_prime"/>
//    public static System.ReadOnlySpan<short> WilsonPrimes
//      => new short[] { 5, 13, 563 };

//    /// <summary>A sequence of all Wolstenholme primes.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Wolstenholme_prime"/>
//    public static System.ReadOnlySpan<int> WolstenholmePrimes
//      => new int[] { 16843, 2124679 };
//  }
//}
