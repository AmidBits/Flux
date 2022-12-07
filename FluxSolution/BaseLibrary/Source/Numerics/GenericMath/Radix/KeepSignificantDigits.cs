namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Retreive <paramref name="count"/> least significant digits of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static TSelf KeepLeastSignificantDigit<TSelf, TRadix>(this TSelf number, TRadix radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => number % TSelf.CreateChecked(AssertRadix(radix));

    /// <summary>Retreive <paramref name="count"/> least significant digits of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static TSelf KeepLeastSignificantDigits<TSelf, TRadix, TCount>(this TSelf number, TRadix radix, TCount count)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TCount : System.Numerics.IBinaryInteger<TCount>
      => number % IntegerPow(TSelf.CreateChecked(AssertRadix(radix)), count);

    /// <summary>Drop the leading digit of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static TSelf KeepMostSignificantDigits<TSelf, TRadix, TCount>(this TSelf number, TRadix radix, TCount count)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TCount : System.Numerics.IBinaryInteger<TCount>
      => number / IntegerPow(TSelf.CreateChecked(radix), DigitCount(number, radix) - TSelf.CreateChecked(count));
  }
}
