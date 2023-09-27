namespace Flux
{
  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    /// <summary>Strips x of its least significant 1 bit.</summary>
    /// <see href="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    public static TSelf StripLeastSignificant1Bit<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsZero(x) ? x : x & (x - TSelf.One);

#else

    /// <summary>Strips x of its least significant 1 bit.</summary>
    /// <see href="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    public static System.Numerics.BigInteger StripLeastSignificant1Bit(this System.Numerics.BigInteger x)
      => x & (x - System.Numerics.BigInteger.One);

    /// <summary>Strips x of its least significant 1 bit.</summary>
    /// <see href="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    public static int StripLeastSignificant1Bit(this int x)
      => x & (x - 1);

    /// <summary>Strips x of its least significant 1 bit.</summary>
    /// <see href="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    public static long StripLeastSignificant1Bit(this long x)
      => x & (x - 1L);

#endif
  }
}
