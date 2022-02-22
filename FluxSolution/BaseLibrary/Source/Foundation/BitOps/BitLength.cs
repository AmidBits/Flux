namespace Flux
{
  public static partial class BitOps
  {
    // https://en.wikipedia.org/wiki/Bit-length
    // http://aggregate.org/MAGIC/
    // http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan

    /// <summary></summary>
    public static System.Collections.Generic.IReadOnlyList<byte> ByteBitLength
      => System.Linq.Enumerable.ToList(System.Linq.Enumerable.Select(System.Linq.Enumerable.Range(0, 256), i => (byte)BitLength((uint)i)));

    /// <summary>Returns the count of bits in the minimal two's-complement representation of the number.</summary>
    /// <remarks>BitLength(value) is equal to 1 + Log2(value).</remarks>
    public static int BitLength(System.Numerics.BigInteger value)
      => value < 0
      ? 0
      : Log2(value) + 1;
    //{
    //  if (value > 255)
    //  {
    //    value.ToByteArrayEx(out var byteIndex, out var byteValue);

    //    return ByteBitLength[byteValue] + byteIndex * 8;
    //  }
    //  else if (value > 0) return ByteBitLength[(int)value];

    //  return 0;
    //}

    /// <summary>Returns the number of bits in the minimal two's-complement representation of the number.</summary>
    public static int BitLength(int value)
      => BitLength(unchecked((uint)value));
    /// <summary>Returns the number of bits in the minimal two's-complement representation of the number.</summary>
    public static int BitLength(long value)
      => BitLength(unchecked((ulong)value));

    /// <summary>Returns the number of bits in the minimal two's-complement representation of the number.</summary>
    [System.CLSCompliant(false)]
    public static int BitLength(uint value)
      => value <= 0
      ? (value == 0 ? 0 : 32)
      : System.Numerics.BitOperations.Log2(value) + 1;
    //{
    //  var count = 0;

    //  if (value > 0)
    //  {
    //    unchecked
    //    {
    //      if (value > 0xFFFF)
    //      {
    //        count += 16;
    //        value >>= 16;
    //      }

    //      if (value > 0xFF)
    //      {
    //        count += 8;
    //        value >>= 8;
    //      }

    //      if (value > 0xF)
    //      {
    //        count += 4;
    //        value >>= 4;
    //      }

    //      if (value > 0x3)
    //      {
    //        count += 2;
    //        value >>= 2;
    //      }

    //      if (value > 0x1)
    //      {
    //        count++;
    //        value >>= 1;
    //      }

    //      if (value > 0)
    //        count++;
    //    }
    //  }

    //  return count;
    //}
    /// <summary>Returns the number of bits in the minimal two's-complement representation of the number.</summary>
    /// <remarks>The implementation is relatively fast.</remarks>
    /// <see cref = "https://en.wikipedia.org/wiki/Bit-length" />
    [System.CLSCompliant(false)]
    public static int BitLength(ulong value)
      => value <= 0
      ? (value == 0 ? 0 : 64)
      : System.Numerics.BitOperations.Log2(value) + 1;
    //{
    //  var count = 0;

    //  if (value > 0)
    //  {
    //    unchecked
    //    {
    //      if (value > 0xFFFFFFFF)
    //      {
    //        count += 32;
    //        value >>= 32;
    //      }

    //      if (value > 0xFFFF)
    //      {
    //        count += 16;
    //        value >>= 16;
    //      }

    //      if (value > 0xFF)
    //      {
    //        count += 8;
    //        value >>= 8;
    //      }

    //      if (value > 0xF)
    //      {
    //        count += 4;
    //        value >>= 4;
    //      }

    //      if (value > 0x3)
    //      {
    //        count += 2;
    //        value >>= 2;
    //      }

    //      if (value > 0x1)
    //      {
    //        count++;
    //        value >>= 1;
    //      }

    //      if (value > 0)
    //        count++;
    //    }
    //  }

    //  return count;
    //}
  }
}
