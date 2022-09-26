namespace Flux
{
  public static partial class BitOps
  {
    // https://en.wikipedia.org/wiki/Bit_numbering#Most_significant_bit
    // http://aggregate.org/MAGIC/

    /// <summary>Extracts the most significant 1 bit (highest numbered element of a bit set) by clearing the least significant 1 bit in each iteration of a loop.</summary>
    public static System.Numerics.BigInteger MostSignificant1Bit(System.Numerics.BigInteger value)
      => value == 0
      ? 0
      : System.Numerics.BigInteger.One << Log2(value);

    /// <summary>Extracts the most significant 1 bit (highest numbered element of a bit set).</summary>
    public static int MostSignificant1Bit(int value)
      => value < 0
      ? int.MinValue
      : unchecked((int)MostSignificant1Bit((uint)value));
    /// <summary>Extracts the most significant 1 bit (highest numbered element of a bit set).</summary>
    public static long MostSignificant1Bit(long value)
      => value < 0
      ? long.MinValue
      : unchecked((long)MostSignificant1Bit((ulong)value));

    /// <summary>Extracts the most significant 1 bit (highest numbered element of a bit set).</summary>
    [System.CLSCompliant(false)]
    public static uint MostSignificant1Bit(uint value)
      => value == 0
      ? 0
      : 1U << System.Numerics.BitOperations.Log2(value);
    /// <summary>Extracts the most significant 1 bit (highest numbered element of a bit set).</summary>
    [System.CLSCompliant(false)]
    public static ulong MostSignificant1Bit(ulong value)
      => value == 0
      ? 0
      : 1UL << System.Numerics.BitOperations.Log2(value);
  }
}
