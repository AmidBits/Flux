// <seealso cref="http://aggregate.org/MAGIC/"/>
// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan"/>

using System.Linq;

namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Determines the index position of the specified power-of-two bit value.</summary>
    /// <remarks>The value must be power of two, i.e. only a single bit set, or the result will be -1. For the same result of an arbitrary number, use Log2() or MostSignificant1Bit().</remarks>
    public static int BitIndex(System.Numerics.BigInteger value)
      => IsPowerOf2(value) ? Log2(value) : -1;

    /// <summary>Determines the index position of the specified power-of-two bit value.</summary>
    /// <remarks>The value must be power of two, i.e. only a single bit set, or the result will be -1. For the same result of an arbitrary number, use Log2() or MostSignificant1Bit().</remarks>
    public static int BitIndex(int value)
      => value > 0 ? BitIndex(unchecked((uint)value)) : -1;
    /// <summary>Determines the index position of the specified power-of-two bit value.</summary>
    /// <remarks>The value must be power of two, i.e. only a single bit set, or the result will be -1. For the same result of an arbitrary number, use Log2() or MostSignificant1Bit().</remarks>
    public static int BitIndex(long value)
      => value > 0 ? BitIndex(unchecked((ulong)value)) : -1;

    /// <summary>Determines the index position of the specified power-of-two bit value.</summary>
    /// <remarks>The value must be power of two, i.e. only a single bit set, or the result will be -1. For the same result of an arbitrary number, use Log2() or MostSignificant1Bit().</remarks>
    [System.CLSCompliant(false)]
    public static int BitIndex(uint value)
    {
      if (value == 0 || (value & unchecked(value - 1)) != 0)
        return -1;

      var count = 0;

      if (value > 0x0000FFFF)
      {
        count += 16;
        value >>= 16;
      }

      if (value > 0x00FF)
      {
        count += 8;
        value >>= 8;
      }

      if (value > 0x0F)
      {
        count += 4;
        value >>= 4;
      }

      if (value > 0x3)
      {
        count += 2;
        value >>= 2;
      }

      if (value > 0x1)
        count++;

      return count;
    }
    /// <summary>Determines the index position of the specified power-of-two bit value.</summary>
    /// <remarks>The value must be power of two, i.e. only a single bit set, or the result will be -1. For the same result of an arbitrary number, use Log2() or MostSignificant1Bit().</remarks>
    [System.CLSCompliant(false)]
    public static int BitIndex(ulong value)
    {
      if (value == 0 || (value & unchecked(value - 1)) != 0)
        return -1;

      var count = 0;

      if (value > 0x00000000FFFFFFFF)
      {
        count += 32;
        value >>= 32;
      }

      if (value > 0x0000FFFF)
      {
        count += 16;
        value >>= 16;
      }

      if (value > 0x00FF)
      {
        count += 8;
        value >>= 8;
      }

      if (value > 0x0F)
      {
        count += 4;
        value >>= 4;
      }

      if (value > 0x3)
      {
        count += 2;
        value >>= 2;
      }

      if (value > 0x1)
        count++;

      return count;
    }
  }
}
