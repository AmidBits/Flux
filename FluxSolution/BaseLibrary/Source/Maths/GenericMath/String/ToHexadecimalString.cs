namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>Creates a hexadecimal (base 16) text string from <paramref name="value"/>.</summary>
    /// <remarks>This function evaluates and returns the most fitting string length, e.g. a 8 digit string for an 32-bit integer.</remarks>
    public static string ToHexadecimalString<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => ToRadixString(value, 16, Bits.GetMaxDigitCount(value.GetBitCount(), 16, value.ImplementsSignedNumber()) /*value.GetByteCount() << 1*/);

#else

    public static string ToHexadecimalString(this System.Numerics.BigInteger value) => Text.PositionalNotation.Base16.NumberToText(value, value.GetByteCount() << 1);
    public static string ToHexadecimalString(this int value) => Text.PositionalNotation.Base16.NumberToText(value, value.GetByteCount() << 1);
    public static string ToHexadecimalString(this long value) => Text.PositionalNotation.Base16.NumberToText(value, value.GetByteCount() << 1);

#endif
  }
}
