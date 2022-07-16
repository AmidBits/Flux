namespace Flux
{
  // Trailing Zero Count (LZC) for int/long, uint/ulong is kept for backwards compatibility, use System.Numerics.BitOperations.TrailingZeroCount when available.

  public static partial class BitOps
  {
    // http://aggregate.org/MAGIC/
    // http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan
    // https://en.wikipedia.org/wiki/Bit-length

    // https://en.wikipedia.org/wiki/Find_first_set#CTZ

    /// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
    public static int TrailingZeroCount(this System.Numerics.BigInteger value)
      => value > 0
      ? PopCount(LeastSignificant1Bit(value) - 1)
      : -1;

    /// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
    public static int TrailingZeroCount(this int value)
      => TrailingZeroCount(unchecked((uint)value));
    /// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
    public static int TrailingZeroCount(this long value)
      => TrailingZeroCount(unchecked((ulong)value));

    /// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
    [System.CLSCompliant(false)]
    public static int TrailingZeroCount(this uint value)
      => (value == 0)
      ? 32
      : PopCount(LeastSignificant1Bit(value) - 1);
    /// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
    [System.CLSCompliant(false)]
    public static int TrailingZeroCount(this ulong value)
      => (value > uint.MaxValue)
      ? 32 + TrailingZeroCount((uint)(value >> 32))
      : TrailingZeroCount((uint)value);
  }
}
