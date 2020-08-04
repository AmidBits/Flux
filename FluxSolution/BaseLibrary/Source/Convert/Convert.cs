namespace Flux
{
  public static partial class Convert
  {
    /// <summary>Returns a string with the Unicode code point as a numerical HTML entity (using decimal).</summary>
    /// <param name="utf">Unicode code point.</param>
    public static string ToHtmlEntityNumberDecimal(int utf32)
      => @"&#" + utf32.ToString(System.Globalization.CultureInfo.CurrentCulture) + ';';
    /// <summary>Returns a string with the Unicode code point as a numerical HTML entity (using hexadecimal).</summary>
    /// <param name="utf">Unicode code point.</param>
    public static string ToHtmlEntityNumberHexadecimal(int utf32)
      => @"&#x" + utf32.ToString(@"X2", System.Globalization.CultureInfo.CurrentCulture) + ';';

    /// <summary>Returns a string with the Unicode code point as an escaped "\uXXXX" or "\UXXXXXXXX" depending on the integer bit size.</summary>
    /// <param name="utf32">Unicode code point.</param>
    public static string ToUnicodeBackslashU(int utf32)
      => utf32 < 0x10000 ? @"\u" + utf32.ToString(@"X4", System.Globalization.CultureInfo.CurrentCulture) : @"\U" + utf32.ToString(@"X8", System.Globalization.CultureInfo.CurrentCulture);

    /// <summary>Returns a string with the Unicode code point as a "U+XXXXXX".</summary>
    /// <param name="utf32">Unicode code point.</param>
    public static string ToUnicodeUplus(int utf32)
      => @"U+" + utf32.ToString(@"X4", System.Globalization.CultureInfo.CurrentCulture);

    /// <summary>Creates a new sequence of strings from a sequence of Unicode UTF-32 code points (32-bit integers).</summary>
    /// <returns>A sequence of strings.</returns>
    public static System.Collections.Generic.IEnumerable<string> FromUtf32(System.Collections.Generic.IEnumerable<int> source)
    {
      foreach (var utf32 in source ?? throw new System.ArgumentNullException(nameof(source)))
      {
        yield return char.ConvertFromUtf32(utf32);
      }
    }
    /// <summary>Creates a sequence of Unicode UTF-32 code points (32-bit integers) from the string.</summary>
    /// <returns>A sequence of Unicode code points.</returns>
    public static System.Collections.Generic.IEnumerable<int> ToUtf32(string source)
    {
      for (var index = 0; index < (source?.Length ?? throw new System.NullReferenceException(nameof(source))); index += char.IsSurrogatePair(source, index) ? 2 : 1)
      {
        yield return char.ConvertToUtf32(source, index);
      }
    }
  }
}
