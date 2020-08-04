// <seealso cref="http://aggregate.org/MAGIC/"/>
// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan"/>

namespace Flux
{
  public static partial class Math
  {
    /// <summary>Count Leading Zeros (clz) counts the number of zero bits preceding the most significant one bit.</summary>
    /// <remarks>Returns a number representing the number of leading zeros of the binary representation of the value. Since BigInteger is arbitrary in size there is a required bit width to measure against.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Find_first_set#CLZ"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Bit-length"/>
    //public static int CountLeadingZeros(System.Numerics.BigInteger value, int bitWidth) => bitWidth - BitLength(value);
    /// <summary>Count Leading Zeros (clz) counts the number of zero bits preceding the most significant one bit.</summary>
    /// <remarks>Returns a number representing the number of leading zeros of the binary representation of the value. Since BigInteger is arbitrary this version finds and subtracts from the nearest power-of-two bit-length that the value fits in.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Find_first_set#CLZ"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Bit-length"/>
    public static int CountLeadingZeros(System.Numerics.BigInteger value)
    {
      if (value > 255) return BitLength(value) is var bitLength ? (int)((System.Numerics.BigInteger.One << BitLength(bitLength)) - bitLength) : throw new System.ArithmeticException();
      else if (value > 0) return 8 - BitLength(value);
      else return -1;
    }

    /// <summary>Count Leading Zeros (clz) counts the number of zero bits preceding the most significant one bit.</summary>
    /// <remarks>The bit position can easily be calculated by subtracting 1 from the resulting return value.</remarks>
    /// <param name="bitWidth">The number of bits in the set. E.g. 32, 64 or 128 for built-in integer data type sizes.</param>
    /// <see cref="https://en.wikipedia.org/wiki/Find_first_set#CLZ"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Bit-length"/>
    /// <remarks>Pretty fast.</remarks>
    public static int CountLeadingZeros(int value)
      => value < 0 ? 0 : CountLeadingZeros(unchecked((uint)value));
    /// <summary>Count Leading Zeros (clz) counts the number of zero bits preceding the most significant one bit.</summary>
    /// <remarks>The bit position can easily be calculated by subtracting 1 from the resulting return value.</remarks>
    /// <param name="bitWidth">The number of bits in the set. E.g. 32, 64 or 128 for built-in integer data type sizes.</param>
    /// <see cref="https://en.wikipedia.org/wiki/Find_first_set#CLZ"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Bit-length"/>
    /// <remarks>Pretty fast.</remarks>
    public static int CountLeadingZeros(long value)
      => value < 0 ? 0 : CountLeadingZeros(unchecked((ulong)value));

    /// <summary>Count Leading Zeros (clz) counts the number of zero bits preceding the most significant one bit.</summary>
    /// <remarks>The bit position can easily be calculated by subtracting 1 from the resulting return value.</remarks>
    /// <param name="bitWidth">The number of bits in the set. E.g. 32, 64 or 128 for built-in integer data type sizes.</param>
    /// <see cref="https://en.wikipedia.org/wiki/Find_first_set#CLZ"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Bit-length"/>
    /// <remarks>Pretty fast.</remarks>
    [System.CLSCompliant(false)]
    public static int CountLeadingZeros(uint value)
    {
      if (value == 0) return 32;

      var count = 0;

      if (value > 0)
      {
        if ((value & 0xFFFF0000) == 0)
        {
          count += 16;
          value <<= 16;
        }

        if ((value & 0xFF000000) == 0)
        {
          count += 8;
          value <<= 8;
        }

        if ((value & 0xF0000000) == 0)
        {
          count += 4;
          value <<= 4;
        }

        if ((value & 0xC0000000) == 0)
        {
          count += 2;
          value <<= 2;
        }

        if ((value & 0x80000000) == 0)
        {
          count += 1;
        }
      }

      return count;
    }
    /// <summary>Count Leading Zeros (clz) counts the number of zero bits preceding the most significant one bit.</summary>
    /// <remarks>The bit position can easily be calculated by subtracting 1 from the resulting return value.</remarks>
    /// <param name="bitWidth">The number of bits in the set. E.g. 32, 64 or 128 for built-in integer data type sizes.</param>
    /// <see cref="https://en.wikipedia.org/wiki/Find_first_set#CLZ"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Bit-length"/>
    /// <remarks>Pretty fast.</remarks>
    [System.CLSCompliant(false)]
    public static int CountLeadingZeros(ulong value)
    {
      if (value == 0) return 64;

      var count = 0;

      if (value > 0)
      {
        if ((value & 0xFFFFFFFF00000000) == 0)
        {
          count += 32;
          value <<= 32;
        }

        if ((value & 0xFFFF000000000000) == 0)
        {
          count += 16;
          value <<= 16;
        }

        if ((value & 0xFF00000000000000) == 0)
        {
          count += 8;
          value <<= 8;
        }

        if ((value & 0xF000000000000000) == 0)
        {
          count += 4;
          value <<= 4;
        }

        if ((value & 0xC000000000000000) == 0)
        {
          count += 2;
          value <<= 2;
        }

        if ((value & 0x8000000000000000) == 0)
        {
          count += 1;
        }
      }

      return count;
    }
  }
}
