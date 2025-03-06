namespace Flux
{
  public static partial class Em
  {
    [System.Text.RegularExpressions.GeneratedRegex(@"((?<Prefix>\\u)(?<Number>[0-9A-Fa-f]{4})|(?<Prefix>\\U)(?<Number>[0-9A-Fa-f]{8})|(?<Prefix>\\x)(?<Number>[0-9A-Fa-f]{1,8}))", System.Text.RegularExpressions.RegexOptions.Compiled)]
    public static partial System.Text.RegularExpressions.Regex RegexParseCsEscape();

    public static SpanMaker<char> CsEscapeDecode(this System.ReadOnlySpan<char> source)
    {
      var sm = new SpanMaker<char>();

      var evm = RegexParseCsEscape().EnumerateMatches(source);

      var lastEnd = 0;

      foreach (var vm in evm)
      {
        var index = vm.Index;
        var length = vm.Length;

        sm.Append(source.Slice(lastEnd, index - lastEnd)); // Append any in-between characters.

        sm.Append(new System.Text.Rune(int.Parse(source.Slice(index + 2, length - 2), System.Globalization.NumberStyles.HexNumber)).ToString()); // Append the rune string.

        lastEnd = index + length;
      }

      return sm;
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

    public static SpanMaker<char> CsEscapeEncode(this System.ReadOnlySpan<char> source, CsEscapeOption format)
    {
      var sm = new SpanMaker<char>();
      foreach (var rune in source.EnumerateRunes())
        sm.Append(rune.CsEscapeEncode(format));
      return sm;
    }
  }
}
