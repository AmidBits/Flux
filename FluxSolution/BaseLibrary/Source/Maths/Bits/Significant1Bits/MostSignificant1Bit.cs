namespace Flux
{
  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    /// <summary>Extracts the highest numbered element of a bit set. Given a 2's complement binary integer value, this is the most significant 1 bit. If <paramref name="x"/> is zero, zero is returned. If <paramref name="x"/> is negative value the min-value of the signed type is returned (this is the highest bit set to 1).</summary>
    public static TSelf MostSignificant1Bit<TSelf>(this TSelf x) where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsZero(x) ? x : TSelf.One << (x.BitLengthN() - 1);

#else

    /// <summary>Extracts the highest numbered element of a bit set. Given a 2's complement binary integer value, this is the most significant 1 bit.</summary>
    public static System.Numerics.BigInteger MostSignificant1Bit(this System.Numerics.BigInteger x) => x.IsZero ? x : (System.Numerics.BigInteger.One << (BitLengthN(x) - 1));

    /// <summary>Extracts the highest numbered element of a bit set. Given a 2's complement binary integer value, this is the most significant 1 bit.</summary>
    public static int MostSignificant1Bit(this int x) => unchecked((int)((uint)x).MostSignificant1Bit());

    /// <summary>Extracts the highest numbered element of a bit set. Given a 2's complement binary integer value, this is the most significant 1 bit.</summary>
    public static long MostSignificant1Bit(this long x) => unchecked((long)((ulong)x).MostSignificant1Bit());

    /// <summary>Extracts the highest numbered element of a bit set. Given a 2's complement binary integer value, this is the most significant 1 bit.</summary>
    [System.CLSCompliant(false)] public static uint MostSignificant1Bit(this uint x) => x == 0U ? x : (1U << (BitLengthN(x) - 1));

    /// <summary>Extracts the highest numbered element of a bit set. Given a 2's complement binary integer value, this is the most significant 1 bit.</summary>
    [System.CLSCompliant(false)] public static ulong MostSignificant1Bit(this ulong x) => x == 0UL ? x : (1UL << (BitLengthN(x) - 1));

#endif
  }
}