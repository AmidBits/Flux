namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class BitOps
  {
    /// <summary>Often called 'Count Leading Zeros' (clz), counts the number of zero bits preceding the most significant one bit.</summary>
    /// <remarks>Returns a number representing the number of leading zeros of the binary representation of the value. Since BigInteger is arbitrary this version finds and subtracts from the nearest power-of-two bit-length that the value fits in.</remarks>
    public static int GetLeadingZeroCount<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => int.CreateChecked(TSelf.LeadingZeroCount(value)); // value < TSelf.Zero ? 0 : (value.GetByteCount() * 8) - value.GetShortestBitLength();

    /// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
    public static int GetTrailingZeroCount<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => int.CreateChecked(TSelf.TrailingZeroCount(value)); // LeastSignificant1Bit(value).GetShortestBitLength() - 1;
  }
}
