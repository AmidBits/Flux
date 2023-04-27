namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    /// <summary>Indicates whether the <paramref name="number"/> is single digit using the base <paramref name="radix"/>, i.e. in the range [-<paramref name="radix"/>, <paramref name="radix"/>].</summary>
    public static bool IsSingleDigit<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertRadix(radix);

      return (TSelf.IsZero(number) || (TSelf.IsPositive(number) && number < radix) || (TSelf.IsNegative(number) && number > -radix));
    }

#else

    /// <summary>Indicates whether the instance is single digit, i.e. in the range [-9, 9].</summary>
    public static bool IsSingleDigit(this System.Numerics.BigInteger number, System.Numerics.BigInteger radix)
    {
      AssertRadix(radix);

      return (number == 0 || (number >= 0 && number < radix) || (number < 0 && number > -radix));
    }

    /// <summary>Indicates whether the instance is single digit, i.e. in the range [-9, 9].</summary>
    public static bool IsSingleDigit(this int number, int radix)
    {
      AssertRadix(radix);

      return (number == 0 || (number >= 0 && number < radix) || (number < 0 && number > -radix));
    }

    /// <summary>Indicates whether the instance is single digit, i.e. in the range [-9, 9].</summary>
    public static bool IsSingleDigit(this long number, long radix)
    {
      AssertRadix(radix);

      return (number == 0 || (number >= 0 && number < radix) || (number < 0 && number > -radix));
    }

#endif
  }
}
