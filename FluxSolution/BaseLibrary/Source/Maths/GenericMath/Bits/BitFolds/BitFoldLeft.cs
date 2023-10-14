namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    /// <summary>Recursively "folds" the lower bits into the upper bits (left). The process yields a bit vector with the same least significant 1 as the value, and all 1's above it.</summary>
    /// <returns>All bits set from LSB up, or -1 if the value is less than zero.</returns>
    public static TSelf BitFoldLeft<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var tzc = value.GetTrailingZeroCount();

      return BitFoldRight(value << value.GetLeadingZeroCount()) >> tzc << tzc;
    }

#else

    // The fold 'left' (or up towards MSB) function, is the opposite of (<see cref="FoldRight"/>), sets all bits from LS1B and 'up' (or 'left'), to 1.
    /// <summary>Recursively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, and all 1's above it.</summary>
    /// <returns>All bits set from LSB up, or -1 if the value is less than zero.</returns>
    public static System.Numerics.BigInteger BitFoldLeft(this System.Numerics.BigInteger value)
    {
      var tzc = value.GetTrailingZeroCount();

      return (value << value.GetLeadingZeroCount()).BitFoldRight() >> tzc << tzc;

      //for (var i = value.GetBitCount() >> 1; i > 0; i--)
      //  value |= value >> i;
      //return value;
    }

    public static int BitFoldLeft(this int value) => unchecked((int)((uint)value).BitFoldLeft());
    public static long BitFoldLeft(this long value) => unchecked((long)((ulong)value).BitFoldLeft());

    [System.CLSCompliant(false)]
    public static uint BitFoldLeft(this uint value)
    {
      var tzc = value.GetTrailingZeroCount();

      return (value << value.GetLeadingZeroCount()).BitFoldRight() >> tzc << tzc;

      //value |= (value << 1);
      //value |= (value << 2);
      //value |= (value << 4);
      //value |= (value << 8);
      //value |= (value << 16);
      //return value;
    }
    [System.CLSCompliant(false)]
    public static ulong BitFoldLeft(this ulong value)
    {
      var tzc = value.GetTrailingZeroCount();

      return (value << value.GetLeadingZeroCount()).BitFoldRight() >> tzc << tzc;

      //value |= (value << 1);
      //value |= (value << 2);
      //value |= (value << 4);
      //value |= (value << 8);
      //value |= (value << 16);
      //value |= (value << 32);
      //return value;
    }

#endif
  }
}
