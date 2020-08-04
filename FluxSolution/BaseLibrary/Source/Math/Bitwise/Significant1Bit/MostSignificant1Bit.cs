// <seealso cref="http://aggregate.org/MAGIC/"/>
// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan"/>

namespace Flux
{
  public static partial class Math
  {
    /// <summary>Extracts the most significant 1 bit (highest numbered element of a bit set) by clearing the least significant 1 bit in each iteration of a loop.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bit_numbering#Most_significant_bit"/>
    /// <seealso cref="http://aggregate.org/MAGIC/"/>
    public static System.Numerics.BigInteger MostSignificant1Bit(System.Numerics.BigInteger value)
      => System.Numerics.BigInteger.One << Log2(value);

    /// <summary>Extracts the most significant 1 bit (highest numbered element of a bit set).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bit_numbering#Most_significant_bit"/>
    /// <seealso cref="http://aggregate.org/MAGIC/"/>
    public static int MostSignificant1Bit(int value)
    {
      value |= (value >> 1);
      value |= (value >> 2);
      value |= (value >> 4);
      value |= (value >> 8);
      value |= (value >> 16);

      return value & ~(value >> 1);
    }
    /// <summary>Extracts the most significant 1 bit (highest numbered element of a bit set).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bit_numbering#Most_significant_bit"/>
    /// <seealso cref="http://aggregate.org/MAGIC/"/>
    public static long MostSignificant1Bit(long value)
    {
      value |= (value >> 1);
      value |= (value >> 2);
      value |= (value >> 4);
      value |= (value >> 8);
      value |= (value >> 16);
      value |= (value >> 32);

      return value & ~(value >> 1);
    }

    /// <summary>Extracts the most significant 1 bit (highest numbered element of a bit set).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bit_numbering#Most_significant_bit"/>
    /// <seealso cref="http://aggregate.org/MAGIC/"/>
    [System.CLSCompliant(false)]
    public static uint MostSignificant1Bit(uint value)
    {
      value |= (value >> 1);
      value |= (value >> 2);
      value |= (value >> 4);
      value |= (value >> 8);
      value |= (value >> 16);

      return value & ~(value >> 1);
    }
    /// <summary>Extracts the most significant 1 bit (highest numbered element of a bit set).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bit_numbering#Most_significant_bit"/>
    /// <seealso cref="http://aggregate.org/MAGIC/"/>
    [System.CLSCompliant(false)]
    public static ulong MostSignificant1Bit(ulong value)
    {
      value |= (value >> 1);
      value |= (value >> 2);
      value |= (value >> 4);
      value |= (value >> 8);
      value |= (value >> 16);
      value |= (value >> 32);

      return value & ~(value >> 1);
    }
  }
}
