
namespace Flux
{
  public static partial class Math
  {
    /// <summary>Canonical modulus. The result has the sign of the divisor.</summary>
    public static System.Numerics.BigInteger Mod(System.Numerics.BigInteger dividend, System.Numerics.BigInteger divisor) => ((dividend % divisor) + divisor) % divisor;
    /// <summary>Canonical modulus. The result has the sign of the divisor.</summary>
    public static long Mod(long dividend, long divisor) => ((dividend % divisor) + divisor) % divisor;
    /// <summary>Canonical modulus. The result has the sign of the divisor.</summary>
    public static long Mod(int dividend, int divisor) => ((dividend % divisor) + divisor) % divisor;

    /// <summary>Results in a inverse modulo.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Modular_multiplicative_inverse"/>
    public static long ModInv(long value, long modulus)
    {
      value %= modulus;

      for (var counter = 1L; counter < modulus; counter++)
      {
        if ((value * counter) % modulus == 1)
        {
          return counter;
        }
      }

      throw new System.ArithmeticException();
    }
    /// <summary>Results in a inverse modulo.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Modular_multiplicative_inverse"/>
    public static int ModInv(int value, int modulus)
    {
      value %= modulus;

      for (var counter = 1; counter < modulus; counter++)
      {
        if ((value * counter) % modulus == 1)
        {
          return counter;
        }
      }

      throw new System.ArithmeticException();
    }

    /// <summary>Computes modular multiplication on unsigned integers not larger than 63 bits, without overflow of the transient operations.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Modular_arithmetic#Example_implementations"/>
    [System.CLSCompliant(false)]
    public static ulong ModMul(ulong a, ulong b, ulong m)
    {
      ulong d = 0, mp2 = m >> 1;

      if (a >= m) a %= m;
      if (b >= m) b %= m;

      for (var i = 0; i < 64; ++i)
      {
        d = (d > mp2) ? (d << 1) - m : d << 1;
        if ((a & 0x8000000000000000UL) != 0) d += b;
        if (d >= m) d -= m;
        a <<= 1;
      }

      return d;
    }
    /// <summary>Computes modular multiplication on unsigned integers not larger than 63 bits, without overflow of the transient operations.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Modular_arithmetic#Example_implementations"/>
    [System.CLSCompliant(false)]
    public static uint ModMul(uint a, uint b, uint m)
    {
      uint d = 0, mp2 = m >> 1;

      if (a >= m) a %= m;
      if (b >= m) b %= m;

      for (var i = 0; i < 64; ++i)
      {
        d = (d > mp2) ? (d << 1) - m : d << 1;
        if ((a & 0x8000000000000000UL) != 0) d += b;
        if (d >= m) d -= m;
        a <<= 1;
      }

      return d;
    }

    /// <summary>Computes modular exponentiation on unsigned integers not larger than 63 bits, without overflow of the transient operations.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Modular_arithmetic#Example_implementations"/>
    [System.CLSCompliant(false)]
    public static ulong ModPow(ulong a, ulong b, ulong m)
    {
      ulong r = m == 1 ? 0UL : 1UL;

      while (b > 0)
      {
        if ((b & 1) != 0) r = ModMul(r, a, m);
        b = b >> 1;
        a = ModMul(a, a, m);
      }

      return r;
    }
    /// <summary>Computes modular exponentiation on unsigned integers not larger than 63 bits, without overflow of the transient operations.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Modular_arithmetic#Example_implementations"/>
    [System.CLSCompliant(false)]
    public static uint ModPow(uint a, uint b, uint m)
    {
      uint r = m == 1U ? 0U : 1U;

      while (b > 0)
      {
        if ((b & 1) != 0) r = ModMul(r, a, m);
        b = b >> 1;
        a = ModMul(a, a, m);
      }

      return r;
    }

    /// <summary>Canonical modulus. The result has the sign of the divisor.</summary>
    public static System.Numerics.BigInteger ModRem(System.Numerics.BigInteger dividend, System.Numerics.BigInteger divisor, out System.Numerics.BigInteger remainder) => ((remainder = dividend % divisor) + divisor) % divisor;
    /// <summary>Canonical modulus. The result has the sign of the divisor.</summary>
    public static long ModRem(long dividend, long divisor, out long remainder) => ((remainder = dividend % divisor) + divisor) % divisor;
    /// <summary>Canonical modulus. The result has the sign of the divisor.</summary>
    public static int ModRem(int dividend, int divisor, out int remainder) => ((remainder = dividend % divisor) + divisor) % divisor;
  }
}
