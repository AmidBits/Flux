namespace Flux.Numerics
{
  public static partial class BitOps
  {
    // https://en.wikipedia.org/wiki/Bit_numbering#Most_significant_bit
    // http://aggregate.org/MAGIC/

    /// <summary>Extracts the most significant 1 bit (highest numbered element of a bit set) by clearing the least significant 1 bit in each iteration of a loop.</summary>
    public static System.Numerics.BigInteger MostSignificant1Bit(System.Numerics.BigInteger value)
      => System.Numerics.BigInteger.One << Log2(value);

    /// <summary>Extracts the most significant 1 bit (highest numbered element of a bit set).</summary>
    public static int MostSignificant1Bit(int value)
      => value >= 0
      ? unchecked((int)MostSignificant1Bit((uint)value))
      : int.MinValue;
    /// <summary>Extracts the most significant 1 bit (highest numbered element of a bit set).</summary>
    public static long MostSignificant1Bit(long value)
      => value >= 0
      ? unchecked((long)MostSignificant1Bit((ulong)value))
      : long.MinValue;

    /// <summary>Extracts the most significant 1 bit (highest numbered element of a bit set).</summary>
    [System.CLSCompliant(false)]
    public static uint MostSignificant1Bit(uint value)
    {
#if NETCOREAPP
      return 1U << (LeadingZeroCount(value) ^ 31);
#else
      FoldRight(ref value);

			return value & ~(value >> 1);
#endif
    }
    /// <summary>Extracts the most significant 1 bit (highest numbered element of a bit set).</summary>
    [System.CLSCompliant(false)]
    public static ulong MostSignificant1Bit(ulong value)
    {
#if NETCOREAPP
      return 1UL << (LeadingZeroCount(value) ^ 63);
#else
      FoldRight(ref value);

      return value & ~(value >> 1);
#endif
    }
  }
}
