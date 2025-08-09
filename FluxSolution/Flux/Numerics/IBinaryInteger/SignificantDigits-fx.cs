namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>Drop the trailing (least significant) digit of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TInteger DropLeastSignificantDigit<TInteger, TRadix>(this TInteger value, TRadix radix)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => value / TInteger.CreateChecked(Units.Radix.AssertMember(radix));

    /// <summary>Drop <paramref name="count"/> trailing (least significant) digits from <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TInteger DropLeastSignificantDigits<TInteger, TRadix>(this TInteger value, TRadix radix, TInteger count)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => value / TInteger.CreateChecked(Units.Radix.AssertMember(radix).FastIntegerPow(count, UniversalRounding.WholeTowardZero, out var _));

    /// <summary>Drop <paramref name="count"/> leading (most significant) digits of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TInteger DropMostSignificantDigits<TInteger, TRadix>(this TInteger value, TRadix radix, TInteger count)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => value % TInteger.CreateChecked(radix.FastIntegerPow(DigitCount(value, radix) - count, UniversalRounding.WholeTowardZero, out var _));

    /// <summary>Retreive <paramref name="count"/> least significant digits of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TInteger KeepLeastSignificantDigit<TInteger, TRadix>(this TInteger value, TRadix radix)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => value % TInteger.CreateChecked(Units.Radix.AssertMember(radix));

    /// <summary>Retreive <paramref name="count"/> least significant digits of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TInteger KeepLeastSignificantDigits<TInteger, TRadix>(this TInteger value, TRadix radix, TInteger count)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => value % TInteger.CreateChecked(Units.Radix.AssertMember(radix).FastIntegerPow(count, UniversalRounding.WholeTowardZero, out var _));

    /// <summary>Drop the leading digit of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TInteger KeepMostSignificantDigits<TInteger, TRadix>(this TInteger value, TRadix radix, TInteger count)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => value / TInteger.CreateChecked(radix.FastIntegerPow(value.DigitCount(radix) - count, UniversalRounding.WholeTowardZero, out var _));
  }
}
