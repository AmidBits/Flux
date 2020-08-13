// <seealso cref="http://aggregate.org/MAGIC/"/>
// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan"/>

namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Determines if the number is a power of the specified radix.</summary>
    /// <remarks>A non-negative binary integer value is a power of 2 iff (x&(x-1)) is 0 using 2's complement arithmetic.</remarks>
    public static bool IsPowerOf(System.Numerics.BigInteger value, int radix)
    {
      if (value < 0 || radix < 0)
      {
        return IsPowerOf(value < 0 ? -value : value, radix < 0 ? -radix : radix);
      }
      else if (value == radix)
      {
        return true;
      }
      else if (radix == 1)
      {
        return false;
      }
      else if (radix == 2)
      {
        return (value > 0) && ((value & unchecked(value - 1)) == 0);
      }
      else
      {
        if (value > 1)
        {
          while (System.Numerics.BigInteger.DivRem(value, radix, out var remainder) is var quotient && remainder == 0)
          {
            value = quotient;
          }
        }

        return value == 1;
      }
    }

    /// <summary>Determines if the number is a power of the specified radix.</summary>
    /// <remarks>A non-negative binary integer value is a power of 2 iff (x&(x-1)) is 0 using 2's complement arithmetic.</remarks>
    public static bool IsPowerOf(int value, int radix)
    {
      if (value < 0) throw new System.ArgumentOutOfRangeException(nameof(value));
      else if (radix < 2) throw new System.ArgumentOutOfRangeException(nameof(radix));
      else if (value == radix) return true;
      else if (radix == 1) return false;
      else if (radix == 2) return (value != 0) && ((value & unchecked(value - 1)) == 0);
      else
      {
        if (value > 1)
        {
          while (System.Math.DivRem(value, radix, out var remainder) is var quotient && remainder == 0)
          {
            value = quotient;
          }
        }

        return value == 1;
      }
    }
    /// <summary>Determines if the number is a power of the specified radix.</summary>
    /// <remarks>A non-negative binary integer value is a power of 2 iff (x&(x-1)) is 0 using 2's complement arithmetic.</remarks>
    public static bool IsPowerOf(long value, int radix)
    {
      if (value < 0) throw new System.ArgumentOutOfRangeException(nameof(value));
      else if (radix < 2) throw new System.ArgumentOutOfRangeException(nameof(radix));
      else if (value == radix) return true;
      else if (radix == 1) return false;
      else if (radix == 2) return (value != 0L) && ((value & unchecked(value - 1L)) == 0L);
      else
      {
        if (value > 1L)
        {
          while (System.Math.DivRem(value, radix, out var remainder) is var quotient && remainder == 0L)
          {
            value = quotient;
          }
        }

        return value == 1L;
      }
    }

    /// <summary>Determines if the number is a power of the specified radix.</summary>
    /// <remarks>A non-negative binary integer value is a power of 2 iff (x&(x-1)) is 0 using 2's complement arithmetic.</remarks>
    [System.CLSCompliant(false)]
    public static bool IsPowerOf(uint value, int radix)
    {
      if (radix < 2) throw new System.ArgumentOutOfRangeException(nameof(radix));
      else if (value == (uint)radix) return true;
      else if (radix == 1) return false;
      else if (radix == 2) return (value != 0) && ((value & unchecked(value - 1)) == 0);
      else
      {
        if (value > 1)
        {
          while (value % (uint)radix == 0)
          {
            value /= (uint)radix;
          }
        }

        return value == 1;
      }
    }
    /// <summary>Determines if the number is a power of the specified radix.</summary>
    /// <remarks>A non-negative binary integer value is a power of 2 iff (x&(x-1)) is 0 using 2's complement arithmetic.</remarks>
    [System.CLSCompliant(false)]
    public static bool IsPowerOf(ulong value, int radix)
    {
      if (radix < 2) throw new System.ArgumentOutOfRangeException(nameof(radix));
      else if (value == (ulong)radix) return true;
      else if (radix == 1) return false;
      else if (radix == 2) return (value != 0L) && ((value & unchecked(value - 1L)) == 0L);
      else
      {
        if (value > 1L)
        {
          while (value % (ulong)radix == 0)
          {
            value /= (ulong)radix;
          }
        }

        return value == 1L;
      }
    }
  }
}
