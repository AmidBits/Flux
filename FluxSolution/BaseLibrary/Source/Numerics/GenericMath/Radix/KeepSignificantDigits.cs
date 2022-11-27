namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Retreive <paramref name="count"/> least significant digits of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static TSelf KeepLeastSignificantDigit<TSelf, TRadix>(this TSelf number, TRadix radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => number % AssertRadix(radix, out TSelf tradix);

    /// <summary>Retreive <paramref name="count"/> least significant digits of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static TSelf KeepLeastSignificantDigits<TSelf, TRadix, TCount>(this TSelf number, TRadix radix, TCount count)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TCount : System.Numerics.IBinaryInteger<TCount>
      => number % IntegerPow(AssertRadix(radix, out TSelf _), count);

    /// <summary>Drop the leading digit of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static TSelf KeepMostSignificantDigits<TSelf, TRadix, TCount>(this TSelf number, TRadix radix, TCount count)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TCount : System.Numerics.IBinaryInteger<TCount>
      => number / IntegerPow(TSelf.CreateChecked(radix), DigitCount(number, radix) - TSelf.CreateChecked(count));
  }
}
