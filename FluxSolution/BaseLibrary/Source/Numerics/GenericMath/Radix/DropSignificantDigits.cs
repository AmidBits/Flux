namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Drop the trailing (least significant) digit of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static TSelf DropLeastSignificantDigit<TSelf, TRadix>(this TSelf number, TRadix radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => number / TSelf.CreateChecked(AssertRadix(radix));

    /// <summary>Drop <paramref name="count"/> trailing (least significant) digits from <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static TSelf DropLeastSignificantDigits<TSelf, TRadix, TCount>(this TSelf number, TRadix radix, TCount count)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TCount : System.Numerics.IBinaryInteger<TCount>
      => number / IntegerPow(TSelf.CreateChecked(AssertRadix(radix)), count);

    /// <summary>Drop <paramref name="count"/> leading (most significant) digits of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static TSelf DropMostSignificantDigits<TSelf, TRadix, TCount>(this TSelf number, TRadix radix, TCount count)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TCount : System.Numerics.IBinaryInteger<TCount>
      => number % IntegerPow(TSelf.CreateChecked(AssertRadix(radix)), DigitCount(number, radix) - TSelf.CreateChecked(count));
  }
}
