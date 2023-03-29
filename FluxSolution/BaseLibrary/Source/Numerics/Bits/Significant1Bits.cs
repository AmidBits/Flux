namespace Flux
{
  public static partial class Bits
  {
    /// <summary>Extracts the lowest numbered element of a bit set. Given a 2's complement binary integer value, this is the least significant 1 bit.</summary>
    /// <see href="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    public static TSelf LeastSignificant1Bit<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => x & ((~x) + TSelf.One);

    /// <summary>Extracts the highest numbered element of a bit set. Given a 2's complement binary integer value, this is the most significant 1 bit.</summary>
    public static TSelf MostSignificant1Bit<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsZero(x) ? x : (TSelf.One << (x.GetShortestBitLength() - 1));
  }
}
