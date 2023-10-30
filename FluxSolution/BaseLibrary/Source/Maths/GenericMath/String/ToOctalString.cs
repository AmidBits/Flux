namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>Creates an octal (base 8) text string from <paramref name="value"/>.</summary>
    /// <remarks>This function evaluates and returns the most fitting string length, e.g. a 3 digit string for an 8-bit integer.</remarks>
    public static string ToOctalString<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => value.ToRadixString(8, Bits.GetMaxDigitCount(value.GetBitCount(), 8, value.ImplementsSignedNumber()) /*int.DivRem(value.GetBitCount(), 3) is var dr && dr.Remainder > 0 ? dr.Quotient + 1 : dr.Quotient*/);

#else

    public static string ToOctalString(this System.Numerics.BigInteger value) => Text.PositionalNotation.Base8.NumberToText(value, System.Math.DivRem(value.GetBitCount(), 3) is var dr && dr.Remainder > 0 ? dr.Quotient + 1 : dr.Quotient);
    public static string ToOctalString(this int value) => Text.PositionalNotation.Base8.NumberToText(value, System.Math.DivRem(value.GetBitCount(), 3) is var dr && dr.Remainder > 0 ? dr.Quotient + 1 : dr.Quotient);
    public static string ToOctalString(this long value) => Text.PositionalNotation.Base8.NumberToText(value, System.Math.DivRem(value.GetBitCount(), 3) is var dr && dr.Remainder > 0 ? dr.Quotient + 1 : dr.Quotient);

#endif
  }
}
