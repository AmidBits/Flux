namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Converts <paramref name="number"/> to text using base <paramref name="radix"/>.</summary>
    public static string ToSuperscriptString<TSelf>(this TSelf number, int radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => new Text.PositionalNotation(Text.RuneSequences.SuperscriptDecimalDigitRunes.AsSpan().Slice(0, AssertRadix(radix, Text.RuneSequences.SuperscriptDecimalDigitRunes.Length))).NumberToText(number).ToString();
  }
}
