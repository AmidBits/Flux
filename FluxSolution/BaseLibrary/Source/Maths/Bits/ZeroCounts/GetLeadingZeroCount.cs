namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    /// <summary>Using the built-in <see cref="System.Numerics.IBinaryInteger{TSelf}.LeadingZeroCount(TSelf)"/>.</summary>
    public static int GetLeadingZeroCount<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => int.CreateChecked(TSelf.LeadingZeroCount(value));

#else

    /// <summary>Often called 'Count Leading Zeros' (clz), counts the number of zero bits preceding the most significant one bit. In other words, the number of most significant 0 bits.</summary>
    /// <remarks>Returns a number representing the number of leading zeros of the binary representation of the value. Since BigInteger is arbitrary in size there is a required bit width to measure against.</remarks>
    /// <param name="bitWidth">The number of bits in the set. E.g. 32, 64 or 128 for built-in integer data type sizes.</param>
    public static int GetLeadingZeroCount(this System.Numerics.BigInteger value, int bitWidth) => bitWidth - value.GetShortestBitLength();

    /// <summary>Often called 'Count Leading Zeros' (clz), counts the number of zero bits preceding the most significant one bit. In other words, the number of most significant 0 bits.</summary>
    /// <remarks>Returns a number representing the number of leading zeros of the binary representation of the value. Since BigInteger is arbitrary this version finds and subtracts from the nearest power-of-two bit-length that the value fits in.</remarks>
    public static int GetLeadingZeroCount(this System.Numerics.BigInteger value)
    {
      if (value < 0) return -1;
      else if (value.IsZero) return 0;
      else
      {
        var bitLength = value.GetShortestBitLength();

        return (1 << bitLength.GetShortestBitLength()) - bitLength;
      }
    }

    /// <summary>Often called 'Count Leading Zeros' (clz), counts the number of zero bits preceding the most significant one bit. In other words, the number of most significant 0 bits.</summary>
    public static int GetLeadingZeroCount(this int value)
      => System.Numerics.BitOperations.LeadingZeroCount(unchecked((uint)value));

    /// <summary>Often called 'Count Leading Zeros' (clz), counts the number of zero bits preceding the most significant one bit. In other words, the number of most significant 0 bits.</summary>
    public static int GetLeadingZeroCount(this long value)
      => System.Numerics.BitOperations.LeadingZeroCount(unchecked((ulong)value));

    /// <summary>Often called 'Count Leading Zeros' (clz), counts the number of zero bits preceding the most significant one bit. In other words, the number of most significant 0 bits.</summary>
    [System.CLSCompliant(false)]
    public static int GetLeadingZeroCount(this uint value)
      => System.Numerics.BitOperations.LeadingZeroCount(value);

    /// <summary>Often called 'Count Leading Zeros' (clz), counts the number of zero bits preceding the most significant one bit. In other words, the number of most significant 0 bits.</summary>
    [System.CLSCompliant(false)]
    public static int GetLeadingZeroCount(this ulong value)
      => System.Numerics.BitOperations.LeadingZeroCount(value);

#endif
  }
}
