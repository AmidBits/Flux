namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Drop the trailing (least significant) digit of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TNumber DropLeastSignificantDigit<TNumber, TRadix>(this TNumber value, TRadix radix)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => value / TNumber.CreateChecked(Quantities.Radix.AssertMember(radix));

    /// <summary>Drop <paramref name="count"/> trailing (least significant) digits from <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TNumber DropLeastSignificantDigits<TNumber, TRadix>(this TNumber value, TRadix radix, TNumber count)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => value / TNumber.CreateChecked(Quantities.Radix.AssertMember(radix).FastIntegerPow(count, UniversalRounding.WholeTowardZero, out var _));

    /// <summary>Drop <paramref name="count"/> leading (most significant) digits of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TNumber DropMostSignificantDigits<TNumber, TRadix>(this TNumber value, TRadix radix, TNumber count)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => value % TNumber.CreateChecked(radix.FastIntegerPow(DigitCount(value, radix) - count, UniversalRounding.WholeTowardZero, out var _));

    /// <summary>Retreive <paramref name="count"/> least significant digits of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TNumber KeepLeastSignificantDigit<TNumber, TRadix>(this TNumber value, TRadix radix)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => value % TNumber.CreateChecked(Quantities.Radix.AssertMember(radix));

    /// <summary>Retreive <paramref name="count"/> least significant digits of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TNumber KeepLeastSignificantDigits<TNumber, TRadix>(this TNumber value, TRadix radix, TNumber count)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => value % TNumber.CreateChecked(Quantities.Radix.AssertMember(radix).FastIntegerPow(count, UniversalRounding.WholeTowardZero, out var _));

    /// <summary>Drop the leading digit of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TNumber KeepMostSignificantDigits<TNumber, TRadix>(this TNumber value, TRadix radix, TNumber count)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => value / TNumber.CreateChecked(radix.FastIntegerPow(value.DigitCount(radix) - count, UniversalRounding.WholeTowardZero, out var _));
  }
}
