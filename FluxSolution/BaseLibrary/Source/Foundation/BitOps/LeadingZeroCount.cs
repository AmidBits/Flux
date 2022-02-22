namespace Flux
{
  public static partial class BitOps
  {
    // http://aggregate.org/MAGIC/
    // http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan
    // https://en.wikipedia.org/wiki/Bit-length

    // https://en.wikipedia.org/wiki/Find_first_set#CLZ

    /// <summary>Often called 'Count Leading Zeros' (clz), counts the number of zero bits preceding the most significant one bit.</summary>
    /// <remarks>Returns a number representing the number of leading zeros of the binary representation of the value. Since BigInteger is arbitrary in size there is a required bit width to measure against.</remarks>
    /// <param name="bitWidth">The number of bits in the set. E.g. 32, 64 or 128 for built-in integer data type sizes.</param>
    public static int LeadingZeroCount(System.Numerics.BigInteger value, int bitWidth)
      => bitWidth - BitLength(value);
    /// <summary>Often called 'Count Leading Zeros' (clz), counts the number of zero bits preceding the most significant one bit.</summary>
    /// <remarks>Returns a number representing the number of leading zeros of the binary representation of the value. Since BigInteger is arbitrary this version finds and subtracts from the nearest power-of-two bit-length that the value fits in.</remarks>
    public static int LeadingZeroCount(System.Numerics.BigInteger value)
    {
      var bitLength = BitLength(value);

      return (1 << BitLength(bitLength)) - bitLength;
    }

    /// <summary>Often called 'Count Leading Zeros' (clz), counts the number of zero bits preceding the most significant one bit.</summary>
    public static int LeadingZeroCount(int value)
      => LeadingZeroCount(unchecked((uint)value));
    /// <summary>Often called 'Count Leading Zeros' (clz), counts the number of zero bits preceding the most significant one bit.</summary>
    public static int LeadingZeroCount(long value)
      => LeadingZeroCount(unchecked((ulong)value));

    /// <summary>Often called 'Count Leading Zeros' (clz), counts the number of zero bits preceding the most significant one bit.</summary>
    [System.CLSCompliant(false)]
    public static int LeadingZeroCount(uint value)
      => System.Numerics.BitOperations.LeadingZeroCount(value); // (value == 0) ? 32 : 31 - Log2(value);
    /// <summary>Often called 'Count Leading Zeros' (clz), counts the number of zero bits preceding the most significant one bit.</summary>
    [System.CLSCompliant(false)]
    public static int LeadingZeroCount(ulong value)
      => System.Numerics.BitOperations.LeadingZeroCount(value); // (value == 0) ? 64 : 63 - Log2(value); // (value <= uint.MaxValue) ? 32 + LeadingZeroCount((uint)value) : LeadingZeroCount((uint)(value >> 32));
  }
}
