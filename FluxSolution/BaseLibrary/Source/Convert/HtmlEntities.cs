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
  }
}
