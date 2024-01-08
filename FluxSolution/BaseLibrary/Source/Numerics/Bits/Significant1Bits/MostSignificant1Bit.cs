namespace Flux
{
  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    /// <summary>Extracts the highest numbered element of a bit set. Given a 2's complement binary integer value, this is the most significant 1 bit. If <paramref name="value"/> is zero, zero is returned. If <paramref name="value"/> is negative value the min-value of the signed type is returned (this is the highest bit set to 1).</summary>
    /// <remarks>Note that for dynamic types, e.g. <see cref="System.Numerics.BigInteger"/>, the number of bits depends on the storage size used for the <paramref name="value"/>.</remarks>
    public static TSelf MostSignificant1Bit<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsZero(value) ? value : TSelf.One << (value.GetBitLength() - 1);

#else

    /// <summary>Extracts the highest numbered element of a bit set. Given a 2's complement binary integer value, this is the most significant 1 bit.</summary>
    public static System.Numerics.BigInteger MostSignificant1Bit(this System.Numerics.BigInteger x) => x.IsZero ? x : (System.Numerics.BigInteger.One << (GetBitLength(x) - 1));

    /// <summary>Extracts the highest numbered element of a bit set. Given a 2's complement binary integer value, this is the most significant 1 bit.</summary>
    public static int MostSignificant1Bit(this int x) => x == 0 ? x : (1 << (GetBitLength(x) - 1));

    /// <summary>Extracts the highest numbered element of a bit set. Given a 2's complement binary integer value, this is the most significant 1 bit.</summary>
    public static long MostSignificant1Bit(this long x) => x == 0U ? x : (1U << (GetBitLength(x) - 1));

#endif
  }
}
