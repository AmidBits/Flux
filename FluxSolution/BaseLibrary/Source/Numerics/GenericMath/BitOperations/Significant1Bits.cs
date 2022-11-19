namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class BitOps
  {
    /// <summary>Extracts the lowest numbered element of a bit set. Given a 2's complement binary integer value, this is the least significant 1 bit.</summary>
    public static TSelf LeastSignificant1Bit<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => x & ((~x) + TSelf.One);

    /// <summary>Extracts the lowest numbered element of a bit set. Given a 2's complement binary integer value, this is the least significant 1 bit.</summary>
    public static TSelf MostSignificant1Bit<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.One << (GetBitLengthEx(x) - 1);
  }
}
