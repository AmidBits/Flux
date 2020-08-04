// <seealso cref="http://aggregate.org/MAGIC/"/>
// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan"/>

namespace Flux
{
  public static partial class Math
  {
    /// <summary>Extracts the lowest numbered element of a bit set. Given a 2's complement binary integer value, (value & -value) is the least significant 1 bit.</summary>
    /// <remarks>The implementation is extremely fast for huge BigInteger values.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Bit_numbering#Least_significant_bit"/>
    /// <seealso cref="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    public static System.Numerics.BigInteger LeastSignificant1Bit(System.Numerics.BigInteger value)
      => unchecked(value & -value);

    /// <summary>Extracts the lowest numbered element of a bit set. Given a 2's complement binary integer value, (value & -value) is the least significant 1 bit.</summary>
    /// <remarks>The implementation is extremely fast for huge BigInteger values.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Bit_numbering#Least_significant_bit"/>
    /// <seealso cref="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    public static int LeastSignificant1Bit(int value)
      => unchecked(value & -value);
    /// <summary>Extracts the lowest numbered element of a bit set. Given a 2's complement binary integer value, (value & -value) is the least significant 1 bit.</summary>
    /// <remarks>The implementation is extremely fast for huge BigInteger values.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Bit_numbering#Least_significant_bit"/>
    /// <seealso cref="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    public static long LeastSignificant1Bit(long value)
      => unchecked(value & -value);

    /// <summary>Extracts the lowest numbered element of a bit set. Given a 2's complement binary integer value, (value & -value) is the least significant 1 bit.</summary>
    /// <remarks>The implementation is extremely fast for huge BigInteger values.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Bit_numbering#Least_significant_bit"/>
    /// <seealso cref="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    [System.CLSCompliant(false)]
    public static uint LeastSignificant1Bit(uint value)
      => checked((uint)((int)value & -(int)value));
    /// <summary>Extracts the lowest numbered element of a bit set. Given a 2's complement binary integer value, (value & -value) is the least significant 1 bit.</summary>
    /// <remarks>The implementation is extremely fast for huge BigInteger values.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Bit_numbering#Least_significant_bit"/>
    /// <seealso cref="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    [System.CLSCompliant(false)]
    //public static ulong LeastSignificant1Bit(ulong value) => value > long.MaxValue && (System.Numerics.BigInteger)value is var bi ? (ulong)(bi & -bi) : unchecked((ulong)((long)value & -(long)value));
    public static ulong LeastSignificant1Bit(ulong value)
      => checked((ulong)((long)value & -(long)value));
  }
}
