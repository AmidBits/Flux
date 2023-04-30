namespace Flux
{
  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    /// <summary>Extracts the lowest numbered element of a bit set. Given a 2's complement binary integer value, this is the least significant 1 bit.</summary>
    /// <see href="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    public static TSelf LeastSignificant1Bit<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => x & ((~x) + TSelf.One);

#else

    /// <summary>Extracts the lowest numbered element of a bit set. Given a 2's complement binary integer value, this is the least significant 1 bit.</summary>
    /// <see href="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    public static System.Numerics.BigInteger LeastSignificant1Bit(this System.Numerics.BigInteger x) => x & ((~x) + System.Numerics.BigInteger.One);

    /// <summary>Extracts the lowest numbered element of a bit set. Given a 2's complement binary integer value, this is the least significant 1 bit.</summary>
    /// <see href="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    public static int LeastSignificant1Bit(this int x) => x & ((~x) + 1);
    /// <summary>Extracts the lowest numbered element of a bit set. Given a 2's complement binary integer value, this is the least significant 1 bit.</summary>
    /// <see href="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    public static long LeastSignificant1Bit(this long x) => x & ((~x) + 1L);

    /// <summary>Extracts the lowest numbered element of a bit set. Given a 2's complement binary integer value, this is the least significant 1 bit.</summary>
    /// <see href="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    [System.CLSCompliant(false)] public static uint LeastSignificant1Bit(this uint x) => x & ((~x) + 1U);
    /// <summary>Extracts the lowest numbered element of a bit set. Given a 2's complement binary integer value, this is the least significant 1 bit.</summary>
    /// <see href="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    [System.CLSCompliant(false)] public static ulong LeastSignificant1Bit(this ulong x) => x & ((~x) + 1UL);

#endif
  }
}
