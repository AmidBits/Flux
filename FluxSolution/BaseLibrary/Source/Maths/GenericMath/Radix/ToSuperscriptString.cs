namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    private static string m_superscriptDecimalDigits = "\u2070\u00B9\u00B2\u00B3\u2074\u2075\u2076\u2077\u2078\u2079";

    /// <summary>Converts <paramref name="number"/> to text using base <paramref name="radix"/>.</summary>
    public static string ToSuperscriptString<TSelf>(this TSelf number, int radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => Text.PositionalNotation.NumberToText(number, m_superscriptDecimalDigits.AsSpan()[..AssertRadix(radix, m_superscriptDecimalDigits.Length)], (char)UnicodeCodepoint.HyphenMinus).ToString();

#else

    /// <summary>Converts <paramref name="number"/> to text using base <paramref name="radix"/>.</summary>
    public static string ToSuperscriptString(this System.Numerics.BigInteger number, int radix)
      => new Text.PositionalNotation(Text.RuneSequences.SuperscriptDecimalDigitRunes.AsSpan()[..AssertRadix(radix, Text.RuneSequences.SuperscriptDecimalDigitRunes.Length)]).NumberToText(number).ToString();

#endif
  }
}
