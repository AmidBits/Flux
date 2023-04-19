namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    /// <summary>Converts <paramref name="number"/> to text using base <paramref name="radix"/>.</summary>
    public static string ToSubscriptString<TSelf>(this TSelf number, int radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => new Text.PositionalNotation(Text.RuneSequences.SubscriptDecimalDigitRunes.AsSpan()[..AssertRadix(radix, Text.RuneSequences.SubscriptDecimalDigitRunes.Length)]).NumberToText(number).ToString();

#endif
  }
}
