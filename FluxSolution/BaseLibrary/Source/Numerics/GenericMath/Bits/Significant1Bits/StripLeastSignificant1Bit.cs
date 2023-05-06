namespace Flux
{
  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    /// <summary>Strips x of its least significant 1 bit.</summary>
    /// <see href="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    public static TSelf StripLeastSignificant1Bit<TSelf>(this TSelf x) where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => x & (x - TSelf.One);

#else

#endif
  }
}
