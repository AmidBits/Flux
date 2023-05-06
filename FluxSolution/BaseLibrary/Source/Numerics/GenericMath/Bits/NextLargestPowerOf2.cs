namespace Flux
{
  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    /// <summary>The next largest power of 2. A negative <paramref name="x"/> results in 0.</summary>
    /// <see href="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    public static TSelf NextLargestPowerOf2<TSelf>(this TSelf x) where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => x.BitFoldRight() + TSelf.One;

#else

#endif
  }
}
