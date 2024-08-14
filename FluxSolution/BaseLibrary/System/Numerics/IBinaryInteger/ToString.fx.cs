namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Converts <paramref name="number"/> to text using base <paramref name="radix"/>.</summary>
    public static string ToSubscriptString<TSelf>(this TSelf number, int radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => PositionalNotation.NumberToText(number, "\u2080\u2081\u2082\u2083\u2084\u2085\u2086\u2087\u2088\u2089".AsSpan()[..Quantities.Radix.AssertMember(radix, 10)], '\u002D').ToString();

    /// <summary>Converts <paramref name="number"/> to text using base <paramref name="radix"/>.</summary>
    public static string ToSuperscriptString<TSelf>(this TSelf number, int radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => PositionalNotation.NumberToText(number, "\u2070\u00B9\u00B2\u00B3\u2074\u2075\u2076\u2077\u2078\u2079".AsSpan()[..Quantities.Radix.AssertMember(radix, 10)], '\u002D').ToString();
  }
}
