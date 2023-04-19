namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    /// <summary>Returns the sum of all single digits in <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Digit_sum"/>
    public static TSelf DigitSum<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertRadix(radix);

      var sum = TSelf.Zero;

      while (!TSelf.IsZero(number))
      {
        sum += number % radix;

        number /= radix;
      }

      return sum;
    }

#else

    /// <summary>Returns the sum of all digits in the value using the specified radix.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Digit_sum"/>
    public static System.Numerics.BigInteger DigitSum(this System.Numerics.BigInteger value, int radix)
    {
      var sum = System.Numerics.BigInteger.Zero;

      while (value != 0)
      {
        sum += value % radix;
        value /= radix;
      }

      return sum;
    }

    /// <summary>Returns the sum of all digits in the value using the specified radix.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Digit_sum"/>
    public static int DigitSum(this int value, int radix)
    {
      var sum = 0;

      while (value != 0)
      {
        sum += value % radix;
        value /= radix;
      }

      return sum;
    }

    /// <summary>Returns the sum of all digits in the value using the specified radix.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Digit_sum"/>
    public static long DigitSum(this long value, int radix)
    {
      var sum = 0L;

      while (value != 0)
      {
        sum += value % radix;
        value /= radix;
      }

      return sum;
    }

#endif
  }
}
