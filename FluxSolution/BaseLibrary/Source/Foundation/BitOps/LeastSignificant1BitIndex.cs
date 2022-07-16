namespace Flux
{
  // https://en.wikipedia.org/wiki/Binary_logarithm
  // http://graphics.stanford.edu/~seander/bithacks.html#IntegerLogObvious
  // http://aggregate.org/MAGIC/#Log2%20of%20an%20Integer

  public static partial class BitOps
  {
    public static int LeastSignificant1BitIndex(this System.Numerics.BigInteger value)
      => value > 255 && value.ToByteArrayEx(out var byteIndex, out var byteValue) is var _
      ? LeastSignificant1BitIndex(byteValue) + byteIndex * 8
      : value > 0
      ? LeastSignificant1BitIndex((uint)value)
      : 0;

    public static int LeastSignificant1BitIndex(this int number)
      => LeastSignificant1BitIndex(unchecked((uint)number));
    public static int LeastSignificant1BitIndex(this long number)
      => LeastSignificant1BitIndex(unchecked((ulong)number));

    private static readonly byte[] m_deBruijnTableLeastSignificant1BitIndex = new byte[] { 0, 1, 28, 2, 29, 14, 24, 3, 30, 22, 20, 15, 25, 17, 4, 8, 31, 27, 13, 23, 21, 19, 16, 7, 26, 12, 18, 6, 11, 5, 10, 9 };

    [System.CLSCompliant(false)]
    public static int LeastSignificant1BitIndex(this uint number)
      => m_deBruijnTableLeastSignificant1BitIndex[(LeastSignificant1Bit(number) * 0x077CB531U) >> 27];
    [System.CLSCompliant(false)]
    public static int LeastSignificant1BitIndex(this ulong number)
      => number > uint.MaxValue
      ? 32 + LeastSignificant1BitIndex((uint)(number >> 32))
      : LeastSignificant1BitIndex((uint)number);
  }
}
