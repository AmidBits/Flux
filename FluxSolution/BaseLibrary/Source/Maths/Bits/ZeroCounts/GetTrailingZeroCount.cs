namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    /// <summary>Using the built-in <see cref="System.Numerics.IBinaryInteger{TSelf}.TrailingZeroCount(TSelf)"/>.</summary>
    public static int GetTrailingZeroCount<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => int.CreateChecked(TSelf.TrailingZeroCount(value));

#else

    /// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit. In other words, the number of least significant 0 bits.</summary>
    public static int GetTrailingZeroCount(this System.Numerics.BigInteger value)
      => value > 0 ? PopCount((value & -value) - 1) : -1;

    /// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit. In other words, the number of least significant 0 bits.</summary>
    public static int GetTrailingZeroCount(this int value)
      => unchecked(System.Numerics.BitOperations.TrailingZeroCount((uint)value));

    /// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit. In other words, the number of least significant 0 bits.</summary>
    public static int GetTrailingZeroCount(this long value)
      => unchecked(System.Numerics.BitOperations.TrailingZeroCount((ulong)value));

    /// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit. In other words, the number of least significant 0 bits.</summary>
    [System.CLSCompliant(false)]
    public static int GetTrailingZeroCount(this uint value)
      => System.Numerics.BitOperations.TrailingZeroCount(value);

    /// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit. In other words, the number of least significant 0 bits.</summary>
    [System.CLSCompliant(false)]
    public static int GetTrailingZeroCount(this ulong value)
      => System.Numerics.BitOperations.TrailingZeroCount(value);

#endif
  }
}
