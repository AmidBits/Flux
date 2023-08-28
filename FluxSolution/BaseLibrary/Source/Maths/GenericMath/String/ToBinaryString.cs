namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>Creates a binary (base 2) text string from <paramref name="value"/>.</summary>
    /// <remarks>This function evaluates and returns the most fitting string length, e.g. a 32 digit string for a 32-bit integer.</remarks>
    public static string ToBinaryString<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => ToRadixString(value, 2, value.GetBitCount());

#else

    public static string ToBinaryString(this System.Numerics.BigInteger value) => Text.PositionalNotation.Base2.NumberToText(value, value.GetBitCount());
    public static string ToBinaryString(this int value) => Text.PositionalNotation.Base2.NumberToText(value, value.GetBitCount());
    public static string ToBinaryString(this long value) => Text.PositionalNotation.Base2.NumberToText(value, value.GetBitCount());

#endif
  }
}
