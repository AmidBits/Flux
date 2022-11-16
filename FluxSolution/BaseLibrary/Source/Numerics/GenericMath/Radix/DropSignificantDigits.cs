namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Drop the trailing (least significant) digit of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static TSelf DropLeastSignificantDigit<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => number / AssertRadix(radix);

    /// <summary>Drop <paramref name="count"/> trailing (least significant) digits from <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static TSelf DropLeastSignificantDigits<TSelf>(this TSelf number, TSelf radix, TSelf count)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => number / IntegerPow(AssertRadix(radix), count);

    /// <summary>Drop <paramref name="count"/> leading (most significant) digits of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static TSelf DropMostSignificantDigits<TSelf>(this TSelf number, TSelf radix, TSelf count)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => number % IntegerPow(radix, DigitCount(number, radix) - count);
  }
}
