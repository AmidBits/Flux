namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    /// <summary>Often called 'Count Leading Zeros' (clz), counts the number of zero bits preceding the most significant one bit.</summary>
    /// <remarks>Returns a number representing the number of leading zeros of the binary representation of the value. Since BigInteger is arbitrary this version finds and subtracts from the nearest power-of-two bit-length that the value fits in.</remarks>
    public static int LeadingZeroCount<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => int.CreateChecked(TSelf.LeadingZeroCount(value));

#else

    /// <summary>Often called 'Count Leading Zeros' (clz), counts the number of zero bits preceding the most significant one bit.</summary>
    /// <remarks>Returns a number representing the number of leading zeros of the binary representation of the value. Since BigInteger is arbitrary in size there is a required bit width to measure against.</remarks>
    /// <param name="bitWidth">The number of bits in the set. E.g. 32, 64 or 128 for built-in integer data type sizes.</param>
    public static int LeadingZeroCount(this System.Numerics.BigInteger value, int bitWidth) => bitWidth - value.BitLength();

    /// <summary>Often called 'Count Leading Zeros' (clz), counts the number of zero bits preceding the most significant one bit.</summary>
    /// <remarks>Returns a number representing the number of leading zeros of the binary representation of the value. Since BigInteger is arbitrary this version finds and subtracts from the nearest power-of-two bit-length that the value fits in.</remarks>
    public static int LeadingZeroCount(this System.Numerics.BigInteger value)
    {
      if (value > 255) return BitLength(value) is var bitLength ? ((1 << BitLength(bitLength)) - bitLength) : throw new System.ArithmeticException();
      else if (value > 0) return 8 - BitLength(value);
      else return -1;
    }

    public static int LeadingZeroCount(this int value) => unchecked((uint)value).LeadingZeroCount();
    public static int LeadingZeroCount(this long value) => unchecked((ulong)value).LeadingZeroCount();

    public static int LeadingZeroCount(this uint value) => System.Numerics.BitOperations.LeadingZeroCount(value);
    public static int LeadingZeroCount(this ulong value) => System.Numerics.BitOperations.LeadingZeroCount(value);

#endif
  }
}
