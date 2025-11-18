namespace Flux
{
  public enum CsEscapeOption
  {
    /// <summary>\u = Unicode escape sequence (UTF-16) \uHHHH (range: 0000 - FFFF; example: \u00E7 = "ç")</summary>
    UTF16,
    /// <summary>\U = Unicode escape sequence (UTF-32) \U00HHHHHH (range: 000000 - 10FFFF; example: \U0001F47D = "👽")</summary>
    UTF32,
    /// <summary>\x = Unicode escape sequence similar to "\u" except with variable length \xH[H][H][H] (range: 0 - FFFF; example: \x00E7 or \x0E7 or \xE7 = "ç")</summary>
    Variable
  }
  public static partial class Unicode
  {
    [System.Text.RegularExpressions.GeneratedRegex(@"((?<Prefix>\\u)(?<Number>[0-9A-Fa-f]{4})|(?<Prefix>\\U)(?<Number>[0-9A-Fa-f]{8})|(?<Prefix>\\x)(?<Number>[0-9A-Fa-f]{1,8}))", System.Text.RegularExpressions.RegexOptions.Compiled)]
    public static partial System.Text.RegularExpressions.Regex RegexParseCsEscape();

    public static System.Text.StringBuilder CsEscapeDecode(this System.ReadOnlySpan<char> source)
    {
      var sb = new System.Text.StringBuilder();

      var evm = RegexParseCsEscape().EnumerateMatches(source);

      var lastEnd = 0;

      foreach (var vm in evm)
      {
        var index = vm.Index;
        var length = vm.Length;

        sb.Append(source[lastEnd..index]); // Append any in-between characters.

        sb.Append(new System.Text.Rune(int.Parse(source.Slice(index + 2, length - 2), System.Globalization.NumberStyles.HexNumber)).ToString()); // Append the rune string.

        lastEnd = index + length;
      }

      return sb;
    }

    /// <summary>
    /// <para>HTML encoding refers to HTML entities.</para>
    /// <para>HTML encodes a <see cref="System.Char"/>, e.g. "&#12388;".</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="asHexadecimal"></param>
    /// <returns></returns>
    public static string CsEscapeEncode(this char source, CsEscapeOption format)
      => new System.Text.Rune(source).CsEscapeEncode(format);

    /// <summary>
    /// <para>HTML encoding refers to HTML entities.</para>
    /// <para>HTML encodes a <see cref="System.Text.Rune"/>, e.g. "&#12388;".</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="asHexadecimal"></param>
    /// <returns></returns>
    public static string CsEscapeEncode(this System.Text.Rune source, CsEscapeOption format)
      => (source.Value > ushort.MaxValue ? CsEscapeOption.UTF32 : format) switch
      {
        CsEscapeOption.UTF16 => $@"\u{source.Value:X4}",
        CsEscapeOption.UTF32 => $@"\U{source.Value:X8}",
        CsEscapeOption.Variable => $@"\x{source.Value:X2}",
        _ => throw new NotImplementedException(),
      };

    public static System.Text.StringBuilder CsEscapeEncode(this System.ReadOnlySpan<char> source, CsEscapeOption format)
    {
      var sb = new System.Text.StringBuilder();
      foreach (var rune in source.EnumerateRunes())
        sb.Append(rune.CsEscapeEncode(format));
      return sb;
    }
  }
}
