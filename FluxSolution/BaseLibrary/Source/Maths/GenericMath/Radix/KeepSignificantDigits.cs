namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    /// <summary>Retreive <paramref name="count"/> least significant digits of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TSelf KeepLeastSignificantDigit<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => value % AssertRadix(radix);

    /// <summary>Retreive <paramref name="count"/> least significant digits of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TSelf KeepLeastSignificantDigits<TSelf>(this TSelf value, TSelf radix, TSelf count)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => value % IntegerPow(AssertRadix(radix), count);

    /// <summary>Drop the leading digit of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TSelf KeepMostSignificantDigits<TSelf>(this TSelf value, TSelf radix, TSelf count)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => value / IntegerPow(radix, DigitCount(value, radix) - count);

#else

    /// <summary>Retreive <paramref name="count"/> least significant digits of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static System.Numerics.BigInteger KeepLeastSignificantDigit(this System.Numerics.BigInteger number, int radix)
      => number % AssertRadix(radix);

    /// <summary>Retreive <paramref name="count"/> least significant digits of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static System.Numerics.BigInteger KeepLeastSignificantDigits(this System.Numerics.BigInteger number, int radix, int count)
      => number % IntegerPow(AssertRadix(radix), count);

    /// <summary>Drop the leading digit of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static System.Numerics.BigInteger KeepMostSignificantDigits(this System.Numerics.BigInteger number, int radix, int count)
      => number / IntegerPow(radix, DigitCount(number, radix) - count);

#endif
  }
}
