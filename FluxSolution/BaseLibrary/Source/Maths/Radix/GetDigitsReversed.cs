using System.Linq;

namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns the place value digits of a number, in reverse order.</summary>
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetDigitsReversed(System.Numerics.BigInteger value, int radix)
    {
      if (radix < 2) throw new System.ArgumentOutOfRangeException(nameof(radix));
      if (value < 0) value = -value;

      while (value > 0)
      {
        yield return value % radix;
        value /= radix;
      }
    }

    /// <summary>Returns the place value digits of a number, in reverse order.</summary>
    public static System.Collections.Generic.IEnumerable<int> GetDigitsReversed(int value, int radix)
    {
      if (radix < 2) throw new System.ArgumentOutOfRangeException(nameof(radix));
      if (value < 0) value = -value;

      while (value > 0)
      {
        yield return value % radix;
        value /= radix;
      }
    }
    /// <summary>Returns the place value digits of a number, in reverse order.</summary>
    public static System.Collections.Generic.IEnumerable<long> GetDigitsReversed(long value, int radix)
    {
      if (radix < 2) throw new System.ArgumentOutOfRangeException(nameof(radix));
      if (value < 0) value = -value;

      while (value > 0)
      {
        yield return value % radix;
        value /= radix;
      }
    }

    /// <summary>Returns the place value digits of a number, in reverse order.</summary>
    [System.CLSCompliant(false)]
    public static System.Collections.Generic.IEnumerable<uint> GetDigitsReversed(uint value, int radix)
    {
      if (radix < 2) throw new System.ArgumentOutOfRangeException(nameof(radix));

      while (value > 0)
      {
        yield return value % (uint)radix;
        value /= (uint)radix;
      }
    }
    /// <summary>Returns the place value digits of a number, in reverse order.</summary>
    [System.CLSCompliant(false)]
    public static System.Collections.Generic.IEnumerable<ulong> GetDigitsReversed(ulong value, int radix)
    {
      if (radix < 2) throw new System.ArgumentOutOfRangeException(nameof(radix));

      while (value > 0)
      {
        yield return value % (ulong)radix;
        value /= (ulong)radix;
      }
    }
  }
}
