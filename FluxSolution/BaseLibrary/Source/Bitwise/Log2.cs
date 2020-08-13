using System.Linq;

namespace Flux
{
  public static partial class Bitwise
  {
    //private static System.Collections.Generic.IReadOnlyList<int>? m_byteLog2;
    ///// <summary></summary>
    ///// <remarks>System.Console.WriteLine(string.Join(@",", System.Linq.Enumerable.Range(1, 256).Select(n => (int) System.Math.Ceiling(System.Math.Log(n, 2)))));</remarks>
    //public static System.Collections.Generic.IReadOnlyList<int> ByteLog2
    //  => m_byteLog2 ??= System.Linq.Enumerable.Range(1, 256).Select(n => (int)System.Math.Ceiling(System.Math.Log(n, 2)) - 1).ToList();

    /// <summary>The log base 2 of an integer is the same as the position of the highest bit set (or most significant bit set, MSB).</summary>
    /// <remarks>
    ///  public static long Log2(long value)
    ///  {
    ///    var index = 0;
    ///    while ((value >>= 1) > 0) { index++; }
    ///    return index;
    ///  }
    /// </remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Binary_logarithm"/>
    /// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html#IntegerLogObvious"/>
    /// <seealso cref="http://aggregate.org/MAGIC/#Log2%20of%20an%20Integer"/>
    public static int Log2(System.Numerics.BigInteger value)
      => System.Convert.ToInt32(System.Math.Floor(System.Numerics.BigInteger.Log(value, 2)));
    //=> value > 0 ? (int)System.Math.Ceiling(System.Numerics.BigInteger.Log(value, 2)) - 1 : -1; // This line don't work.
    // Lookup:
    //{
    //  if (value > 255)
    //  {
    //    value.ToByteArrayEx(out var byteIndex, out var byteValue);

    //    return byteIndex * 8 + ByteLog2[byteValue];
    //  }
    //  else if (value > 0) return ByteLog2[(int)value];

    //  return -1;
    //}
    // Looper:
    //{
    //  if (value <= 0) return -1;

    //  var result = 0;

    //  var bits = 128;

    //  while (value >= (System.Numerics.BigInteger.One << bits))
    //  {
    //    value >>= bits;
    //    result += bits;
    //  }

    //  do
    //  {
    //    bits /= 2;

    //    if (value >= (System.Numerics.BigInteger.One << bits))
    //    {
    //      value >>= bits;
    //      result += bits;
    //    }
    //  }
    //  while (bits > 2);

    //  if (value > 0x1) result++;

    //  return result;
    //}

    /// <summary>The log base 2 of an integer, is the same as the position of the highest bit set (or most significant bit set, MSB).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Binary_logarithm"/>
    /// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html#IntegerLogObvious"/>
    /// <seealso cref="http://aggregate.org/MAGIC/#Log2%20of%20an%20Integer"/>
    public static int Log2(int value)
    {
      if (value <= 0) return -1;

      unchecked
      {
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

        if (value > 0x1) count++;

        return count;
      }
    }

    /// <summary>The log base 2 of an integer, is the same as the position of the highest bit set (or most significant bit set, MSB).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Binary_logarithm"/>
    /// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html#IntegerLogObvious"/>
    /// <seealso cref="http://aggregate.org/MAGIC/#Log2%20of%20an%20Integer"/>
    public static int Log2(long value)
    {
      if (value <= 0) return -1;

      unchecked
      {
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

        if (value > 0x1) count++;

        return count;
      }
    }

    /// <summary>The log base 2 of an integer, is the same as the position of the highest bit set (or most significant bit set, MSB).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Binary_logarithm"/>
    /// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html#IntegerLogObvious"/>
    /// <seealso cref="http://aggregate.org/MAGIC/#Log2%20of%20an%20Integer"/>
    [System.CLSCompliant(false)]
    public static int Log2(uint value)
    {
      if (value == 0) return -1;

      unchecked
      {
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

        if (value > 0x1) count++;

        return count;
      }
    }
    /// <summary>The log base 2 of an integer, is the same as the position of the highest bit set (or most significant bit set, MSB).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Binary_logarithm"/>
    /// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html#IntegerLogObvious"/>
    /// <seealso cref="http://aggregate.org/MAGIC/#Log2%20of%20an%20Integer"/>
    [System.CLSCompliant(false)]
    public static int Log2(ulong value)
    {
      if (value == 0) return -1;

      unchecked
      {
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

        if (value > 0x1) count++;

        return count;
      }
    }
  }
}
