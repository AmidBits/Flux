namespace Flux
{
  // https://en.wikipedia.org/wiki/Binary_logarithm
  // http://graphics.stanford.edu/~seander/bithacks.html#IntegerLogObvious
  // http://aggregate.org/MAGIC/#Log2%20of%20an%20Integer

  public static partial class BitOps
  {
    public static int MostSignificant1BitIndex(this System.Numerics.BigInteger value)
      => value > 255 && value.ToByteArrayEx(out var byteIndex, out var byteValue) is var _
      ? MostSignificant1BitIndex(byteValue) + byteIndex * 8
      : value > 0
      ? MostSignificant1BitIndex((uint)value)
      : 0;

    public static int MostSignificant1BitIndex(this int value)
      => MostSignificant1BitIndex(unchecked((uint)value));
    public static int MostSignificant1BitIndex(this long value)
      => MostSignificant1BitIndex(unchecked((ulong)value));

    private static readonly byte[] m_deBruijnTableMostSignificant1BitIndex = new byte[] { 0, 1, 16, 2, 29, 17, 3, 22, 30, 20, 18, 11, 13, 4, 7, 23, 31, 15, 28, 21, 19, 10, 12, 6, 14, 27, 9, 5, 26, 8, 25, 24 };

    /// <summary>Converts a power-of-2 value (only a single bit set to 1).</summary>
    [System.CLSCompliant(false)]
    public static int PowerOf2BitToIndex(this uint powerOfTwo1Bit)
      => m_deBruijnTableMostSignificant1BitIndex[(powerOfTwo1Bit * 0x06EB14F9) >> 27];
    /// <summary>Converts a power-of-2 value (only a single bit set to 1).</summary>
    [System.CLSCompliant(false)]
    public static int PowerOf2BitToIndex(this ulong value)
      => value > uint.MaxValue
      ? 32 + PowerOf2BitToIndex((uint)(value >> 32))
      : PowerOf2BitToIndex((uint)value);

    [System.CLSCompliant(false)]
    public static int MostSignificant1BitIndex(uint value)
      => PowerOf2BitToIndex(MostSignificant1Bit(value));
    [System.CLSCompliant(false)]
    public static int MostSignificant1BitIndex(ulong value)
      => PowerOf2BitToIndex(MostSignificant1Bit(value));
  }
}
