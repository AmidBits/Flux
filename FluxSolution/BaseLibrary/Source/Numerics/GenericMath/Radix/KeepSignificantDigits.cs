#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Retreive <paramref name="count"/> least significant digits of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static TSelf KeepLeastSignificantDigit<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => number % AssertRadix(radix);

    /// <summary>PREVIEW! Retreive <paramref name="count"/> least significant digits of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static TSelf KeepLeastSignificantDigits<TSelf>(this TSelf number, TSelf radix, TSelf count)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => number % IntegerPow(AssertRadix(radix), count);

    /// <summary>PREVIEW! Drop the leading digit of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static TSelf KeepMostSignificantDigits<TSelf>(this TSelf number, TSelf radix, TSelf count)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => number / IntegerPow(radix, DigitCount(number, radix) - count);
  }
}
#endif
