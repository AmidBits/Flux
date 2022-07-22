namespace Flux
{
  public static partial class BitOps
  {
    // https://en.wikipedia.org/wiki/Bit-length
    // http://aggregate.org/MAGIC/
    // http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan

    /// <summary>Returns the count of bits in the minimal two's-complement representation of the number.</summary>
    /// <remarks>The number of bits needed to represent the number, if value is positive. If value is negative then -1. A value of zero needs 0 bits.</remarks>
    public static int BitLength(this System.Numerics.BigInteger value)
      => value > 0 ? Log2(value) + 1
      : value < 0 ? -1
      : 0;

    /// <summary>Returns the number of bits in the minimal two's-complement representation of the number.</summary>
    public static int BitLength(this int value)
      => value > 0 ? System.Numerics.BitOperations.Log2(unchecked((uint)value)) + 1
      : value < 0 ? 32
      : 0;
    /// <summary>Returns the number of bits in the minimal two's-complement representation of the number.</summary>
    public static int BitLength(this long value)
      => value > 0 ? System.Numerics.BitOperations.Log2(unchecked((ulong)value)) + 1
      : value < 0 ? 64
      : 0;

    ///// <summary>Returns the number of bits in the minimal two's-complement representation of the number.</summary>
    //[System.CLSCompliant(false)]
    //public static int BitLength(uint value)
    //{
    //  var count = 0;

    //  if (value > 0)
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

    //  return count;
    //}

    ///// <summary>Returns the number of bits in the minimal two's-complement representation of the number.</summary>
    /// <remarks>The implementation is relatively fast.</remarks>
    /// <see cref = "https://en.wikipedia.org/wiki/Bit-length" />
    //[System.CLSCompliant(false)]
    //public static int BitLength(ulong value)
    //{
    //  var count = 0;

    //  if (value > 0)
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

    //  return count;
    //}
  }
}
