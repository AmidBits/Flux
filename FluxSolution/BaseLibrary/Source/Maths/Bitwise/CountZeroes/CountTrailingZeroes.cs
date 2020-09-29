// <seealso cref="http://aggregate.org/MAGIC/"/>
// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan"/>

namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
    /// <remarks>Given the Least Significant 1 Bit and Population Count (Ones Count) algorithms, it is trivial to combine them to construct a trailing zero count.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Find_first_set#CTZ"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Bit-length"/>
    public static int CountTrailingZeros(System.Numerics.BigInteger value)
      => value > 0 ? Bit1Count((value & -value) - 1) : -1;

    /// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
    /// <remarks>Given the Least Significant 1 Bit and Population Count (Ones Count) algorithms, it is trivial to combine them to construct a trailing zero count.</remarks>
    /// <remarks>The implementation is relatively fast.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Find_first_set#CTZ"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Bit-length"/>
    public static int CountTrailingZeros(int value)
      => (value & 1) == 1 ? 0 : Bit1Count((value & -value) - 1);
    /// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
    /// <remarks>Given the Least Significant 1 Bit and Population Count (Ones Count) algorithms, it is trivial to combine them to construct a trailing zero count.</remarks>
    /// <remarks>The implementation is relatively fast.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Find_first_set#CTZ"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Bit-length"/>
    public static int CountTrailingZeros(long value)
      => (value & 1) == 1 ? 0 : Bit1Count((value & -value) - 1);

    /// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
    /// <remarks>Given the Least Significant 1 Bit and Population Count (Ones Count) algorithms, it is trivial to combine them to construct a trailing zero count.</remarks>
    /// <remarks>The implementation is relatively fast.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Find_first_set#CTZ"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Bit-length"/>
    [System.CLSCompliant(false)]
    public static int CountTrailingZeros(uint value)
      => (value & 1) == 1 ? 0 : Bit1Count(checked(((int)value & -(int)value) - 1));
    /// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
    /// <remarks>Given the Least Significant 1 Bit and Population Count (Ones Count) algorithms, it is trivial to combine them to construct a trailing zero count.</remarks>
    /// <remarks>The implementation is relatively fast.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Find_first_set#CTZ"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Bit-length"/>
    [System.CLSCompliant(false)]
    public static int CountTrailingZeros(ulong value)
      => (value & 1) == 1 ? 0 : Bit1Count(checked(((long)value & -(long)value) - 1));
  }
}
