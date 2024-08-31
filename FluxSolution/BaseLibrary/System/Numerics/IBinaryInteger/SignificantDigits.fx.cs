namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Drop the trailing (least significant) digit of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TValue DropLeastSignificantDigit<TValue>(this TValue value, TValue radix)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => value / Quantities.Radix.AssertMember(radix);

    /// <summary>Drop <paramref name="count"/> trailing (least significant) digits from <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TValue DropLeastSignificantDigits<TValue>(this TValue value, TValue radix, TValue count)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => value / Quantities.Radix.AssertMember(radix).IntegerPow(count);

    /// <summary>Drop <paramref name="count"/> leading (most significant) digits of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TValue DropMostSignificantDigits<TValue>(this TValue value, TValue radix, TValue count)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => value % radix.IntegerPow(DigitCount(value, radix) - count);

    /// <summary>Retreive <paramref name="count"/> least significant digits of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TValue KeepLeastSignificantDigit<TValue>(this TValue value, TValue radix)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => value % Quantities.Radix.AssertMember(radix);

    /// <summary>Retreive <paramref name="count"/> least significant digits of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TValue KeepLeastSignificantDigits<TValue>(this TValue value, TValue radix, TValue count)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => value % Quantities.Radix.AssertMember(radix).IntegerPow(count);

    /// <summary>Drop the leading digit of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TValue KeepMostSignificantDigits<TValue>(this TValue value, TValue radix, TValue count)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => value / radix.IntegerPow(DigitCount(value, radix) - count);
  }
}
