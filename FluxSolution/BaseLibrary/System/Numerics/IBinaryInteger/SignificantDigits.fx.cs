namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Drop the trailing (least significant) digit of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TValue DropLeastSignificantDigit<TValue, TRadix>(this TValue value, TRadix radix)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => value / TValue.CreateChecked(Quantities.Radix.AssertMember(radix));

    /// <summary>Drop <paramref name="count"/> trailing (least significant) digits from <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TValue DropLeastSignificantDigits<TValue, TRadix>(this TValue value, TRadix radix, TValue count)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => value / TValue.CreateChecked(Quantities.Radix.AssertMember(radix).IntegerPow(count));

    /// <summary>Drop <paramref name="count"/> leading (most significant) digits of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TValue DropMostSignificantDigits<TValue, TRadix>(this TValue value, TRadix radix, TValue count)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => value % TValue.CreateChecked(radix.IntegerPow(DigitCount(value, radix) - count));

    /// <summary>Retreive <paramref name="count"/> least significant digits of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TValue KeepLeastSignificantDigit<TValue, TRadix>(this TValue value, TRadix radix)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => value % TValue.CreateChecked(Quantities.Radix.AssertMember(radix));

    /// <summary>Retreive <paramref name="count"/> least significant digits of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TValue KeepLeastSignificantDigits<TValue, TRadix>(this TValue value, TRadix radix, TValue count)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => value % TValue.CreateChecked(Quantities.Radix.AssertMember(radix).IntegerPow(count));

    /// <summary>Drop the leading digit of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TValue KeepMostSignificantDigits<TValue, TRadix>(this TValue value, TRadix radix, TValue count)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => value / TValue.CreateChecked(radix.IntegerPow(value.DigitCount(radix) - count));
  }
}
