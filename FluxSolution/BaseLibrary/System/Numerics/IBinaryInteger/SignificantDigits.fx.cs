namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Drop the trailing (least significant) digit of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static TSelf DropLeastSignificantDigit<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => number / Quantities.Radix.AssertMember(radix);

    /// <summary>Drop <paramref name="count"/> trailing (least significant) digits from <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static TSelf DropLeastSignificantDigits<TSelf>(this TSelf number, TSelf radix, TSelf count)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => number / Quantities.Radix.AssertMember(radix).IntegerPow(count);

    /// <summary>Drop <paramref name="count"/> leading (most significant) digits of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static TSelf DropMostSignificantDigits<TSelf>(this TSelf number, TSelf radix, TSelf count)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => number % radix.IntegerPow(DigitCount(number, radix) - count);

    /// <summary>Retreive <paramref name="count"/> least significant digits of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static TSelf KeepLeastSignificantDigit<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => number % Quantities.Radix.AssertMember(radix);

    /// <summary>Retreive <paramref name="count"/> least significant digits of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static TSelf KeepLeastSignificantDigits<TSelf>(this TSelf number, TSelf radix, TSelf count)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => number % Quantities.Radix.AssertMember(radix).IntegerPow(count);

    /// <summary>Drop the leading digit of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static TSelf KeepMostSignificantDigits<TSelf>(this TSelf number, TSelf radix, TSelf count)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => number / radix.IntegerPow(DigitCount(number, radix) - count);
  }
}
