#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Converts <paramref name="number"/> to text using base <paramref name="radix"/>.</summary>
    public static System.ReadOnlySpan<char> ToSuperscriptString<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var digits = GetDigits(number, radix);
      var chars = new char[digits.Length];

      for (var index = digits.Length - 1; index >= 0; index--)
        chars[index] = digits[index] switch
        {
          0 => (char)0x2070,
          1 => (char)0x00B9,
          2 => (char)0x00B2,
          3 => (char)0x00B3,
          4 => (char)0x2074,
          5 => (char)0x2075,
          6 => (char)0x2076,
          7 => (char)0x2077,
          8 => (char)0x2078,
          9 => (char)0x2079,
          _ => throw new System.IndexOutOfRangeException()
        };

      return chars;
    }
  }
}
#endif
