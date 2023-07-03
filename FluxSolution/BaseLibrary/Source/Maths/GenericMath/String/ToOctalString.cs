namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    public static string ToOctalString<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => ToRadixString(value, 8, Bits.MaxDigitCount(value.GetBitCount(), 8, value.ImplementsSignedNumber()) /*int.DivRem(value.GetBitCount(), 3) is var dr && dr.Remainder > 0 ? dr.Quotient + 1 : dr.Quotient*/);

#else

    public static string ToOctalString(this System.Numerics.BigInteger value) => Text.PositionalNotation.Base8.NumberToText(value, System.Math.DivRem(value.GetBitCount(), 3) is var dr && dr.Remainder > 0 ? dr.Quotient + 1 : dr.Quotient);
    public static string ToOctalString(this int value) => Text.PositionalNotation.Base8.NumberToText(value, System.Math.DivRem(value.GetBitCount(), 3) is var dr && dr.Remainder > 0 ? dr.Quotient + 1 : dr.Quotient);
    public static string ToOctalString(this long value) => Text.PositionalNotation.Base8.NumberToText(value, System.Math.DivRem(value.GetBitCount(), 3) is var dr && dr.Remainder > 0 ? dr.Quotient + 1 : dr.Quotient);

#endif
  }
}
