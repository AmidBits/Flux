namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    /// <summary>Converts <paramref name="number"/> to text using base <paramref name="radix"/>.</summary>
    public static string ToSuperscriptString<TSelf>(this TSelf number, int radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => new Text.PositionalNotation(Text.RuneSequences.SuperscriptDecimalDigitRunes.AsSpan()[..AssertRadix(radix, Text.RuneSequences.SuperscriptDecimalDigitRunes.Length)]).NumberToText(number).ToString();

#else

    /// <summary>Converts <paramref name="number"/> to text using base <paramref name="radix"/>.</summary>
    public static string ToSuperscriptString(this System.Numerics.BigInteger number, int radix)
      => new Text.PositionalNotation(Text.RuneSequences.SuperscriptDecimalDigitRunes.AsSpan()[..AssertRadix(radix, Text.RuneSequences.SuperscriptDecimalDigitRunes.Length)]).NumberToText(number).ToString();

#endif
  }
}
