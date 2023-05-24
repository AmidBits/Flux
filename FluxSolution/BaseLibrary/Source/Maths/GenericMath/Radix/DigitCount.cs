namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    /// <summary>Returns the count of all single digits in <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static TSelf DigitCount<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertRadix(radix);

      var count = TSelf.Zero;

      while (!TSelf.IsZero(number))
      {
        count++;

        number /= radix;
      }

      return count;
    }

#else

    /// <summary>Returns the count of all digits in the value using the specified radix.</summary>
    public static int DigitCount(this System.Numerics.BigInteger value, int radix)
      => IsSingleDigit(value, radix) ? 1 : System.Convert.ToInt32(System.Math.Floor(System.Numerics.BigInteger.Log(System.Numerics.BigInteger.Abs(value), AssertRadix(radix))) + 1);

    /// <summary>Returns the count of all digits in the value using the specified radix.</summary>
    public static int DigitCount(this int value, int radix)
      => IsSingleDigit(value, radix) ? 1 : System.Convert.ToInt32(System.Math.Floor(System.Math.Log(System.Math.Abs(value), AssertRadix(radix))) + 1);
    /// <summary>Returns the count of all digits in the value using the specified radix.</summary>
    public static int DigitCount(this long value, int radix)
      => IsSingleDigit(value, radix) ? 1 : System.Convert.ToInt32(System.Math.Floor(System.Math.Log(System.Math.Abs(value), AssertRadix(radix))) + 1);

#endif
  }
}
