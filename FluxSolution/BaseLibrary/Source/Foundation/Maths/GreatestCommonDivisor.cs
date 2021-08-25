namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns the greatest common divisor of a and b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    [System.Obsolete(@"Provided for consistency only. Calls are delegated to System.Numerics.BigInteger.GreatestCommonDivisor().")]
    public static System.Numerics.BigInteger GreatestCommonDivisor(System.Numerics.BigInteger a, System.Numerics.BigInteger b)
      => System.Numerics.BigInteger.GreatestCommonDivisor(a, b);

    /// <summary>Returns the greatest common divisor of a and b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    public static int GreatestCommonDivisor(int a, int b)
    {
      while (b != 0)
      {
        var t = b;
        b = a % b;
        a = t;
      }

      return System.Math.Abs(a);
    }

    /// <summary>Returns the greatest common divisor of a and b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    public static long GreatestCommonDivisor(long a, long b)
    {
      while (b != 0)
      {
        var t = b;
        b = a % b;
        a = t;
      }

      return System.Math.Abs(a);
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
