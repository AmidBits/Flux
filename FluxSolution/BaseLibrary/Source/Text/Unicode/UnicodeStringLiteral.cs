using System.Linq;

namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Parses the string for a Unicode notation expression.</summary>
    public static System.Collections.Generic.IEnumerable<System.Text.Rune> ParseUnicodeStringLiterals(this string source)
      => Text.UnicodeStringLiteral.Parse(source);
  }

  namespace Text
  {
    /// <summary>The functionality of this class relates to \uxxxx and \UXXXXXXXX style formatting.</summary>
    public static class UnicodeStringLiteral
    {
      public static readonly System.Text.RegularExpressions.Regex ParseRegex = new System.Text.RegularExpressions.Regex(@"((?<=\\u)[0-9a-f]{4}|(?<=\\U)[0-9A-F]{8,})", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase);

      public static System.Collections.Generic.IEnumerable<System.Text.Rune> Parse(string expression)
        => ParseRegex.Matches(expression).Where(m => m.Success).Select(m => new System.Text.Rune(int.Parse(m.Value, System.Globalization.NumberStyles.HexNumber, null)));
      public static bool TryParse(string text, out System.Collections.Generic.List<System.Text.Rune> result)
      {
        try
        {
          result = Parse(text).ToList();
          return true;
        }
#pragma warning disable CA1031 // Do not catch general exception types
        catch { }
#pragma warning restore CA1031 // Do not catch general exception types

        result = default!;
        return false;
      }

      /// <summary>Convert the Unicode codepoint to a string literal format, i.e. "\uxxxx" (four hex characters) or "\UXXXXXXXX" (eight hex characters).</summary>
      public static string ToString(int codePoint)
        => codePoint < 0 ? throw new System.ArgumentOutOfRangeException(nameof(codePoint)) : codePoint <= 0xFFFF ? $@"\u{codePoint:x4}" : $@"\U{codePoint:X8}";
    }
  }
}