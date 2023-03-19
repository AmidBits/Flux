namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Converts <paramref name="number"/> to text using base <paramref name="radix"/>.</summary>
    public static System.ReadOnlySpan<System.Text.Rune> ToSuperscriptString<TSelf>(this TSelf number, int radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => new Text.PositionalNotation(Text.RuneSequences.SuperscriptDecimalDigitRunes.AsSpan().Slice(AssertRadix(radix, Text.RuneSequences.SuperscriptDecimalDigitRunes.Length))).NumberToText(number).AsReadOnlySpan();
  }
}
