namespace Flux
{
  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    /// <summary>The next away-from-zero (i.e. larger if positive) power-of-2.</summary>
    /// <see href="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    public static TSelf NextLargerPowerOf2<TSelf>(this TSelf x) where TSelf : System.Numerics.IBinaryInteger<TSelf>
      //=> x.BitFoldRight() + TSelf.One;
      => TSelf.CreateChecked(RoundToPow2(x, true).pow2AwayFromZero);

#else

    /// <summary>The next largest power of 2. A negative <paramref name="x"/> results in 0.</summary>
    /// <see href="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    public static System.Numerics.BigInteger NextLargerPowerOf2(this System.Numerics.BigInteger x)
      => x.BitFoldRight() + System.Numerics.BigInteger.One;

    /// <summary>The next largest power of 2. A negative <paramref name="x"/> results in 0.</summary>
    /// <see href="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    public static int NextLargerPowerOf2(this int x)
      => x.BitFoldRight() + 1;

    /// <summary>The next largest power of 2. A negative <paramref name="x"/> results in 0.</summary>
    /// <see href="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    public static long NextLargerPowerOf2(this long x)
      => x.BitFoldRight() + 1;

#endif
  }
}
