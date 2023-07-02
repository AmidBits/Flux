namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    public static string ToHexadecimalString<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => ToRadixString(value, 16, Bits.ToMaxDigitCount(value.GetBitCount(), 16, value.ImplementsSignedNumber()) /*value.GetByteCount() << 1*/);

#else

    public static string ToHexadecimalString(this System.Numerics.BigInteger value) => Text.PositionalNotation.Base16.NumberToText(value, value.GetByteCount() << 1);
    public static string ToHexadecimalString(this int value) => Text.PositionalNotation.Base16.NumberToText(value, value.GetByteCount() << 1);
    public static string ToHexadecimalString(this long value) => Text.PositionalNotation.Base16.NumberToText(value, value.GetByteCount() << 1);

#endif
  }
}
