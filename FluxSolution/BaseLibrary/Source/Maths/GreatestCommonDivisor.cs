using System.Linq;

namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns the greatest common divisor of a and b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    public static int GreatestCommonDivisor(int a, int b)
    {
      // Handles negative numbers:

      while (b != 0)
      {
        var t = b;
        b = a % b;
        a = t;
      }

      return a >= 0 ? a : -a;

      /*
        // Cannot handle negative numbers.

        if (a < 0) throw new System.ArgumentOutOfRangeException(nameof(a));
        if (b < 0) throw new System.ArgumentOutOfRangeException(nameof(b));

        while (a != 0 && b != 0)
        {
          if (a > b)
            a %= b;
          else
            b %= a;
        }

        return a | b;
      */
    }

    /// <summary>Returns the greatest common divisor of a and b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    public static long GreatestCommonDivisor(long a, long b)
    {
      // Handles negative numbers:

      while (b != 0)
      {
        var t = b;
        b = a % b;
        a = t;
      }

      return a >= 0 ? a : -a;

      /*
        // Cannot handle negative numbers.

        if (a < 0) throw new System.ArgumentOutOfRangeException(nameof(a));
        if (b < 0) throw new System.ArgumentOutOfRangeException(nameof(b));

        while (a != 0 && b != 0)
        {
          if (a > b)
            a %= b;
          else
            b %= a;
        }

        return a | b;
      */
    }

    /// <summary>Returns the greatest common divisor of a and b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    [System.CLSCompliant(false)]
    public static uint GreatestCommonDivisor(uint a, uint b)
    {
      while (b != 0)
      {
        var t = b;
        b = a % b;
        a = t;
      }

      return a;
    }
    /// <summary>Returns the greatest common divisor of a and b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    [System.CLSCompliant(false)]
    public static ulong GreatestCommonDivisor(ulong a, ulong b)
    {
      while (b != 0)
      {
        var t = b;
        b = a % b;
        a = t;
      }

      return a;
    }
  }
}
