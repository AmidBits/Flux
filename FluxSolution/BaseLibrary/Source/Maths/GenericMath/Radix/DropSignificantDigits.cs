namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

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

#else

    /// <summary>Drop the trailing digit of the number.</summary>
    public static System.Numerics.BigInteger DropLeastSignificantDigit(this System.Numerics.BigInteger source, int radix)
      => source / AssertRadix(radix);

    /// <summary>Drop the trailing digit of the number.</summary>
    public static int DropLeastSignificantDigit(this int source, int radix)
      => source / AssertRadix(radix);

    /// <summary>Drop the trailing digit of the number.</summary>
    public static long DropLeastSignificantDigit(this long source, int radix)
      => source / AssertRadix(radix);

    /// <summary>Drop the trailing digit of the number.</summary>
    public static System.Numerics.BigInteger DropLeastSignificantDigits(this System.Numerics.BigInteger source, int radix, int count)
      => source / System.Numerics.BigInteger.Pow(AssertRadix(radix), count);

    /// <summary>Drop the trailing digit of the number.</summary>
    public static int DropLeastSignificantDigits(this int source, int radix, int count)
      => source / (int)IntegerPow(AssertRadix(radix), count);

    /// <summary>Drop the trailing digit of the number.</summary>
    public static long DropLeastSignificantDigits(this long source, int radix, int count)
      => source / (long)IntegerPow(AssertRadix(radix), count);

    /// <summary>Drop the leading digit of the number.</summary>
    public static System.Numerics.BigInteger DropMostSignificantDigits(this System.Numerics.BigInteger source, int radix, int count)
      => IsSingleDigit(source, radix) ? 0 : source % System.Numerics.BigInteger.Pow(radix, DigitCount(source, radix) - count);

    /// <summary>Drop the leading digit of the number.</summary>
    public static int DropMostSignificantDigits(this int source, int radix, int count)
      => IsSingleDigit(source, radix) ? 0 : source % (int)IntegerPow(radix, DigitCount(source, radix) - count);

    /// <summary>Drop the leading digit of the number.</summary>
    public static long DropMostSignificantDigits(this long source, int radix, int count)
      => IsSingleDigit(source, radix) ? 0 : source % (long)IntegerPow(radix, DigitCount(source, radix) - count);

#endif
  }
}
