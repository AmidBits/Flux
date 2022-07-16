namespace Flux
{
  public static partial class BitOps
  {
    // https://en.wikipedia.org/wiki/Bit_numbering#Least_significant_bit
    // http://aggregate.org/MAGIC/

    /// <summary>Extracts the lowest numbered element of a bit set. Given a 2's complement binary integer value, (value & -value) is the least significant 1 bit. Very fast.</summary>
    public static System.Numerics.BigInteger LeastSignificant1Bit(this System.Numerics.BigInteger value)
      => unchecked(value & -value);

    /// <summary>Extracts the lowest numbered element of a bit set. Given a 2's complement binary integer value, (value & -value) is the least significant 1 bit. Very fast.</summary>
    public static int LeastSignificant1Bit(this int value)
      => unchecked(value & -value); // This is one situation where signed integers has less number of operations than unsigned integers.
    /// <summary>Extracts the lowest numbered element of a bit set. Given a 2's complement binary integer value, (value & -value) is the least significant 1 bit. Very fast.</summary>
    public static long LeastSignificant1Bit(this long value)
      => unchecked(value & -value); // This is one situation where signed integers has less number of operations than unsigned integers.

    /// <summary>Extracts the lowest numbered element of a bit set. Given a 2's complement binary integer value, (value & -value) is the least significant 1 bit. Very fast.</summary>
    [System.CLSCompliant(false)]
    public static uint LeastSignificant1Bit(this uint value)
      => unchecked(value & ((~value) + 1));
    /// <summary>Extracts the lowest numbered element of a bit set. Given a 2's complement binary integer value, (value & -value) is the least significant 1 bit. Very fast.</summary>
    [System.CLSCompliant(false)]
    public static ulong LeastSignificant1Bit(this ulong value)
      => unchecked(value & ((~value) + 1));
  }
}
