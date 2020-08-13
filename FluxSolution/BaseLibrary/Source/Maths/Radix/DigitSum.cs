namespace Flux
{
  public static partial class Maths
  {
    // https://en.wikipedia.org/wiki/Digit_sum

    /// <summary>Returns the sum of all digits in the value using the specified radix.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Digit_sum"/>
    public static System.Numerics.BigInteger DigitSum(System.Numerics.BigInteger value, int radix)
    {
      var sum = System.Numerics.BigInteger.Zero;

      while (value != System.Numerics.BigInteger.Zero)
      {
        value = System.Numerics.BigInteger.DivRem(value, radix, out var remainder);

        sum += remainder;
      }

      return sum;
    }

    /// <summary>Returns the sum of all digits in the value using the specified radix.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Digit_sum"/>
    public static int DigitSum(int value, int radix)
    {
      var sum = 0;

      while (value != 0)
      {
        value = System.Math.DivRem(value, radix, out var remainder);

        sum += remainder;
      }

      return sum;
    }
    /// <summary>Returns the sum of all digits in the value using the specified radix.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Digit_sum"/>
    public static long DigitSum(long value, int radix)
    {
      var sum = 0L;

      while (value != 0)
      {
        value = System.Math.DivRem(value, radix, out var remainder);

        sum += remainder;
      }

      return sum;
    }

    /// <summary>Returns the sum of all digits in the value using the specified radix.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Digit_sum"/>
    [System.CLSCompliant(false)]
    public static int DigitSum(uint value, int radix)
    {
      var sum = 0U;

      while (value != 0)
      {
        sum += value % (uint)radix;

        value /= (uint)radix;
      }

      return unchecked((int)sum);
    }
    /// <summary>Returns the sum of all digits in the value using the specified radix.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Digit_sum"/>
    [System.CLSCompliant(false)]
    public static int DigitSum(ulong value, int radix)
    {
      var sum = 0UL;

      while (value != 0L)
      {
        sum += value % (ulong)radix;

        value /= (ulong)radix;
      }

      return unchecked((int)sum);
    }
  }
}
