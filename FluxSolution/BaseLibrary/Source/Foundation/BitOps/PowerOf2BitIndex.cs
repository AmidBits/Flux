//namespace Flux
//{
//  // https://en.wikipedia.org/wiki/Binary_logarithm
//  // http://graphics.stanford.edu/~seander/bithacks.html#IntegerLogObvious
//  // http://aggregate.org/MAGIC/#Log2%20of%20an%20Integer

//  public static partial class BitOps
//  {
//    public static int PowerOf2BitIndex(System.Numerics.BigInteger value)
//      => value > 255 && value.ToByteArrayEx(out var byteIndex, out var byteValue) is var _
//      ? PowerOf2BitIndex((uint)byteValue) + byteIndex * 8
//      : value > 0
//      ? PowerOf2BitIndex((uint)value)
//      : 0;

//    public static int PowerOf2BitIndex(int value)
//      => PowerOf2BitIndex(unchecked((uint)value));
//    public static int PowerOf2BitIndex(long value)
//      => PowerOf2BitIndex(unchecked((ulong)value));

//    private static readonly byte[] m_deBruijnTablePowerOf2BitIndex = new byte[] { 0, 1, 16, 2, 29, 17, 3, 22, 30, 20, 18, 11, 13, 4, 7, 23, 31, 15, 28, 21, 19, 10, 12, 6, 14, 27, 9, 5, 26, 8, 25, 24 };

//    /// <summary>Converts a power-of-2 value (only a single bit set to 1).</summary>
//    [System.CLSCompliant(false)]
//    public static int PowerOf2BitIndex(uint value)
//      => IsPowerOf2(value) ? m_deBruijnTablePowerOf2BitIndex[(value * 0x06EB14F9) >> 27] : throw new System.ArgumentOutOfRangeException(nameof(value));

//    /// <summary>Converts a power-of-2 value (only a single bit set to 1).</summary>
//    [System.CLSCompliant(false)]
//    public static int PowerOf2BitIndex(ulong value)
//      => value > uint.MaxValue
//      ? 32 + PowerOf2BitIndex((uint)(value >> 32))
//      : PowerOf2BitIndex((uint)value);
//  }
//}
