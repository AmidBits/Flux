namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    private static string m_subscriptDecimalDigits = "\u2080\u2081\u2082\u2083\u2084\u2085\u2086\u2087\u2088\u2089";

    /// <summary>Converts <paramref name="number"/> to text using base <paramref name="radix"/>.</summary>
    public static string ToSubscriptString<TSelf>(this TSelf number, int radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => PositionalNotation.NumberToText(number, m_subscriptDecimalDigits.AsSpan()[..AssertRadix(radix, m_subscriptDecimalDigits.Length)], (char)UnicodeCodepoint.HyphenMinus).ToString();

#else

    /// <summary>Converts <paramref name="number"/> to text using base <paramref name="radix"/>.</summary>
    public static string ToSubscriptString(this System.Numerics.BigInteger number, int radix)
      => new PositionalNotation(Text.RuneSequences.SubscriptDecimalDigitRunes.AsSpan()[..AssertRadix(radix, Text.RuneSequences.SubscriptDecimalDigitRunes.Length)]).NumberToText(number).ToString();

#endif
  }
}
