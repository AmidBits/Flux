namespace Flux
{
  // https://en.wikipedia.org/wiki/Binary_logarithm
  // http://graphics.stanford.edu/~seander/bithacks.html#IntegerLogObvious
  // http://aggregate.org/MAGIC/#Log2%20of%20an%20Integer

  public static partial class BitOps
  {
    public static int MostSignificant1BitIndex(System.Numerics.BigInteger value)
      => value > 255 && value.ToByteArrayEx(out var byteIndex, out var byteValue) is var _
      ? MostSignificant1BitIndex(byteValue) + byteIndex * 8
      : value > 0
      ? MostSignificant1BitIndex((uint)value)
      : 0;

    public static int MostSignificant1BitIndex(int number)
      => MostSignificant1BitIndex(unchecked((uint)number));
    public static int MostSignificant1BitIndex(long number)
      => MostSignificant1BitIndex(unchecked((ulong)number));

    private static readonly byte[] m_deBruijnTableMostSignificant1BitIndex = new byte[] { 0, 1, 16, 2, 29, 17, 3, 22, 30, 20, 18, 11, 13, 4, 7, 23, 31, 15, 28, 21, 19, 10, 12, 6, 14, 27, 9, 5, 26, 8, 25, 24 };

    /// <summary>Converts a power-of-2 value (only a single bit set to 1).</summary>
    [System.CLSCompliant(false)]
    public static int PowerOf2BitToIndex(uint powerOfTwo1Bit)
      => m_deBruijnTableMostSignificant1BitIndex[(powerOfTwo1Bit * 0x06EB14F9) >> 27];
    /// <summary>Converts a power-of-2 value (only a single bit set to 1).</summary>
    [System.CLSCompliant(false)]
    public static int PowerOf2BitToIndex(ulong number)
      => number >= ((ulong)uint.MaxValue + 1)
      ? 32 + PowerOf2BitToIndex((uint)(number >> 32))
      : PowerOf2BitToIndex((uint)number);

    [System.CLSCompliant(false)]
    public static int MostSignificant1BitIndex(uint number)
      => PowerOf2BitToIndex(MostSignificant1Bit(number));
    [System.CLSCompliant(false)]
    public static int MostSignificant1BitIndex(ulong number)
      => PowerOf2BitToIndex(MostSignificant1Bit(number));
  }
}
