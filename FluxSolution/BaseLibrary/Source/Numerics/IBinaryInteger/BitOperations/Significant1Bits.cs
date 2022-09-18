#if NET7_0_OR_GREATER
namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class BinaryInteger
  {
    /// <summary>PREVIEW! Extracts the lowest numbered element of a bit set. Given a 2's complement binary integer value, this is the least significant 1 bit.</summary>
    public static TSelf LeastSignificant1Bit<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>, System.Numerics.IBitwiseOperators<TSelf, TSelf, TSelf>
      => value & ((~value) + TSelf.One);

    /// <summary>PREVIEW! Extracts the lowest numbered element of a bit set. Given a 2's complement binary integer value, this is the least significant 1 bit.</summary>
    public static TSelf MostSignificant1Bit<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.One << GetBitLength(value) - 1;
  }
}
#endif
