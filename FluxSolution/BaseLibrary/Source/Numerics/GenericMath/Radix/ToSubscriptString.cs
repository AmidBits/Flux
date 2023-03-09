namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Converts <paramref name="number"/> to text using base <paramref name="radix"/>.</summary>
    public static System.ReadOnlySpan<char> ToSubscriptString<TSelf, TRadix>(this TSelf number, TRadix radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var digits = GetDigits(number, radix);
      var chars = new char[digits.Count];

      for (var index = digits.Count - 1; index >= 0; index--)
        chars[index] = digits[index] switch
        {
          0 => (char)0x2080,
          1 => (char)0x2081,
          2 => (char)0x2082,
          3 => (char)0x2083,
          4 => (char)0x2084,
          5 => (char)0x2085,
          6 => (char)0x2086,
          7 => (char)0x2087,
          8 => (char)0x2088,
          9 => (char)0x2089,
          _ => throw new System.IndexOutOfRangeException()
        };

      return chars;
    }
  }
}
