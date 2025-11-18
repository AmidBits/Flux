namespace Flux
{
  public static partial class HtmlEntities
  {
    [System.Text.RegularExpressions.GeneratedRegexAttribute(@"(?<Prefix>&#)(?<Codepoint>[0-9]+|x[0-9A-Fa-f]+)(?<Suffix>;)", System.Text.RegularExpressions.RegexOptions.Compiled)]
    public static partial System.Text.RegularExpressions.Regex RegexHtmlEncodingMatch();

    /// <summary>
    /// <para>Decodes all HTML entities to characters.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="replacementSelector"></param>
    /// <returns></returns>
    public static System.Text.StringBuilder HtmlDecode(this System.ReadOnlySpan<char> source/*, System.Func<System.ReadOnlySpan<char>, string>? replacer=null*/)
    {
      var sb = new System.Text.StringBuilder();

      var evm = RegexHtmlEncodingMatch().EnumerateMatches(source);

      var lastEnd = 0;

      foreach (var vm in evm)
      {
        sb.Append(source[lastEnd..vm.Index]);

        var integer
          = int.TryParse(source.Slice(vm.Index + 2, vm.Length - 3), System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, out var dec)
          ? dec
          : int.TryParse(source.Slice(vm.Index + 3, vm.Length - 4), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out var hex)
          ? hex
          : throw new System.InvalidOperationException();

        sb.Append(new System.Text.Rune(integer).ToString());

        lastEnd = vm.Index + vm.Length;
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
    public static string HtmlEncode(this char source, bool asHexadecimal = false)
      => new System.Text.Rune(source).HtmlEncode(asHexadecimal);

    /// <summary>
    /// <para>HTML encoding refers to HTML entities.</para>
    /// <para>HTML encodes a <see cref="System.Text.Rune"/>, e.g. "&#12388;".</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="asHexadecimal"></param>
    /// <returns></returns>
    public static string HtmlEncode(this System.Text.Rune source, bool asHexadecimal = false)
      => asHexadecimal ? $"&#x{source.Value:X4};" : $"&#{source.Value};";

    /// <summary>
    /// <para>Encodes all runes to HTML entities.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="asHexadecimal"></param>
    /// <returns></returns>
    public static System.Text.StringBuilder HtmlEncode(this System.ReadOnlySpan<char> source, bool asHexadecimal = false)
    {
      var sb = new System.Text.StringBuilder();
      foreach (var rune in source)
        sb.Append(rune.HtmlEncode(asHexadecimal));
      return sb;
    }
  }
}
