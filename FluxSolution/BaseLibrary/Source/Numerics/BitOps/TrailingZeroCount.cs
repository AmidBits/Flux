namespace Flux.Numerics
{
  public static partial class BitOps
  {
    // http://aggregate.org/MAGIC/
    // http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan
    // https://en.wikipedia.org/wiki/Bit-length

    // https://en.wikipedia.org/wiki/Find_first_set#CTZ

    /// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
    public static int TrailingZeroCount(System.Numerics.BigInteger value)
      => value > 0
      ? PopCount(LeastSignificant1Bit(value) - 1)
      : -1;

    /// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
    public static int TrailingZeroCount(int value)
      => PopCount(LeastSignificant1Bit(value) - 1);
    /// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
    public static int TrailingZeroCount(long value)
      => PopCount(LeastSignificant1Bit(value) - 1);

    /// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
    [System.CLSCompliant(false)]
    public static int TrailingZeroCount(uint value)
    {
#if NETCOREAPP
      return System.Numerics.BitOperations.TrailingZeroCount(value);
#else
			return (value == 0)
				      ? 32
              : PopCount(LeastSignificant1Bit(value) - 1);
#endif
    }
    /// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
    [System.CLSCompliant(false)]
    public static int TrailingZeroCount(ulong value)
    {
#if NETCOREAPP
      return System.Numerics.BitOperations.TrailingZeroCount(value);
#else
			return ((uint)value == 0)
				      ? 32 + TrailingZeroCount((uint)(value >> 32))
              : TrailingZeroCount((uint)value);
#endif
    }
  }
}
