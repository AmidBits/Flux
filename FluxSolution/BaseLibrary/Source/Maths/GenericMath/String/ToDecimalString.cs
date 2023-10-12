namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>Creates a decimal (base 10) text string from <paramref name="value"/>.</summary>
    /// <remarks>This function evaluates and returns the most fitting string length, e.g. a 10 digit string for an 32-bit integer.</remarks>
    public static string ToDecimalString<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => ToRadixString(value, 10, Bits.GetMaxDigitCount(value.GetBitCount(), 10, value.ImplementsSignedNumber()));

#else

    public static string ToDecimalString(this System.Numerics.BigInteger value) => Text.PositionalNotation.Base10.NumberToText(value, value.GetBitCount());
    public static string ToDecimalString(this int value) => Text.PositionalNotation.Base10.NumberToText(value, value.GetBitCount());
    public static string ToDecimalString(this long value) => Text.PositionalNotation.Base10.NumberToText(value, value.GetBitCount());

#endif
  }
}
