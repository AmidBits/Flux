namespace Flux
{
  public static partial class Maths
  {
    // System.Math.Pow(double, double) already exists.
    // System.Numerics.BigInteger.Pow(System.Numerics.BigInteger, int) already exists.

    public const string PowExceptionString = @"Invalid operands, value must be greater or equal to zero and exponent must be greater or equal to one.";

    /// <summary>Calculates the power of the specified value and exponent, using exponentiation by repeated squaring. Essentially, we repeatedly double x, and if pow has a 1 bit at that position, we multiply/accumulate that into the return value.</summary>
    /// <see cref="https://tkramesh.wordpress.com/2011/04/17/numerical-computations-in-c-exponentiation-by-repeated-squaring/"/>
    public static int Pow(int value, int exponent)
    {
      if (exponent >= 1)
      {
        var result = 1;

        checked
        {
          while (exponent > 0)
          {
            if ((exponent & 1) == 1) result *= value;

            value *= value;

            exponent >>= 1;
          }

          return result;
        }
      }
      else if (exponent == 0) return 1;

      throw new System.ArithmeticException(PowExceptionString);
    }
    /// <summary>Calculates the power of the specified value and exponent, using exponentiation by repeated squaring. Essentially, we repeatedly double x, and if pow has a 1 bit at that position, we multiply/accumulate that into the return value.</summary>
    /// <see cref="https://tkramesh.wordpress.com/2011/04/17/numerical-computations-in-c-exponentiation-by-repeated-squaring/"/>
    public static long Pow(long value, int exponent)
    {
      if (exponent >= 1)
      {
        var result = 1L;

        checked
        {
          while (exponent > 0)
          {
            if ((exponent & 1) == 1)
            {
              result *= value;
            }

            value *= value;

            exponent >>= 1;
          }

          return result;
        }
      }
      else if (exponent == 0) return 1;

      throw new System.ArithmeticException(PowExceptionString);
    }

    /// <summary>Calculates the power of the specified value and exponent, using exponentiation by repeated squaring. Essentially, we repeatedly double x, and if pow has a 1 bit at that position, we multiply/accumulate that into the return value.</summary>
    [System.CLSCompliant(false)]
    public static uint Pow(uint value, int exponent)
    {
      if (exponent >= 1)
      {
        var result = 1U;

        checked
        {
          while (exponent > 0)
          {
            if ((exponent & 1) == 1)
            {
              result *= value;
            }

            value *= value;

            exponent >>= 1;
          }

          return result;
        }
      }
      else if (exponent == 0) return 1;

      throw new System.ArithmeticException(PowExceptionString);
    }
    /// <summary>Calculates the power of the specified value and exponent, using exponentiation by repeated squaring. Essentially, we repeatedly double x, and if pow has a 1 bit at that position, we multiply/accumulate that into the return value.</summary>
    [System.CLSCompliant(false)]
    public static ulong Pow(ulong value, int exponent)
    {
      if (exponent >= 1)
      {
        var result = 1UL;

        checked
        {
          while (exponent > 0)
          {
            if ((exponent & 1) == 1)
            {
              result *= value;
            }

            value *= value;

            exponent >>= 1;
          }

          return result;
        }
      }
      else if (exponent == 0) return 1;

      throw new System.ArithmeticException(PowExceptionString);
    }
  }
}
