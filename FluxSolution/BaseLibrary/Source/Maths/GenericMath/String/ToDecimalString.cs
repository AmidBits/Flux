namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    public static string ToDecimalString<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => ToRadixString(value, 10, Bits.MaxDigitCount(value.GetBitCount(), 10, value.ImplementsSignedNumber()));

#else

    public static string ToDecimalString(this System.Numerics.BigInteger value) => Text.PositionalNotation.Base10.NumberToText(value, value.GetBitCount());
    public static string ToDecimalString(this int value) => Text.PositionalNotation.Base10.NumberToText(value, value.GetBitCount());
    public static string ToDecimalString(this long value) => Text.PositionalNotation.Base10.NumberToText(value, value.GetBitCount());

#endif
  }
}
