namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    // The fold 'left' (or up towards MSB) function, is the opposite of (<see cref="FoldRight"/>), sets all bits from LS1B and 'up' (or 'left'), to 1.
    /// <summary>Recursively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, and all 1's above it.</summary>
    /// <returns>All bits set from LSB up, or -1 if the value is less than zero.</returns>
    public static TSelf BitFoldLeft<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var tzc = TrailingZeroCount(value);

      return BitFoldRight(value << LeadingZeroCount(value)) >> tzc << tzc;
    }

#else

    // The fold 'left' (or up towards MSB) function, is the opposite of (<see cref="FoldRight"/>), sets all bits from LS1B and 'up' (or 'left'), to 1.
    /// <summary>Recursively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, and all 1's above it.</summary>
    /// <returns>All bits set from LSB up, or -1 if the value is less than zero.</returns>
    public static System.Numerics.BigInteger BitFoldLeft(this System.Numerics.BigInteger value)
    {
      var tzc = value.TrailingZeroCount();

      return (value << value.LeadingZeroCount()).BitFoldRight() >> tzc << tzc;
    }

    public static int BitFoldLeft(this int value) => unchecked((int)((uint)value).BitFoldLeft());
    public static long BitFoldLeft(this long value) => unchecked((long)((ulong)value).BitFoldLeft());

    [System.CLSCompliant(false)] public static uint BitFoldLeft(this uint value) => value == 0 ? 0 : (((MostSignificant1Bit(value) - 1) << 1) | 1);
    [System.CLSCompliant(false)] public static ulong BitFoldLeft(this ulong value) => value == 0 ? 0 : (((MostSignificant1Bit(value) - 1) << 1) | 1);

#endif
  }
}
