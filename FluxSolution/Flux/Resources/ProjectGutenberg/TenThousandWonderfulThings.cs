namespace Flux
{
  public static partial class Resources
  {
    #region Gutenberg TenthousandWonderfulThings

    [System.Text.RegularExpressions.GeneratedRegexAttribute("(?<!\\s+)\r\n(?!\\s+)", System.Text.RegularExpressions.RegexOptions.Compiled)]
    private static partial System.Text.RegularExpressions.Regex RegexGutenbergTenThousandWonderfulThings();

    public static System.Collections.Generic.IEnumerable<string[]> GetGutenbergTenThousandWonderfulThings(string file = @"file://\Resources\ProjectGutenberg\pg45849.txt")
    {
      yield return new string[] { "Title", "Text" };

      //var reTitle = new System.Text.RegularExpressions.Regex(@"^[\!\-\:\;\'\""\,\.\? A-Z0-9]+$", System.Text.RegularExpressions.RegexOptions.Compiled);

      using var stream = new System.Uri(file).GetStream();
      using var reader = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8);

      using var e = reader.ReadLines(s => true, s => s).GetEnumerator();

      while (e.MoveNext())
        if (e.Current.Equals(@"TEN THOUSAND WONDERFUL THINGS.", System.StringComparison.Ordinal)) // This is where we start enumerating lines from the text.
          break;

      var entry = new System.Text.StringBuilder();

      while (e.MoveNext())
      {
        if (e.Current.Equals(@"  PRINTED BY WILLIAM CLOWES AND SONS, LIMITED, LONDON AND DECCLES.", System.StringComparison.Ordinal)) // This is where we stop enumerating lines from the text.
          break;

        if (entry.IsCommonSuffix(0, "\r\n\r\n\r\n") && e.Current.EndsWith('.'))//&& reTitle.IsMatch(e.Current))
        {
          var text = entry.ToString().Trim();

          text = RegexGutenbergTenThousandWonderfulThings().Replace(text, " ");
          //text = System.Text.RegularExpressions.Regex.Replace(text, "(?<!\\s+)\r\n(?!\\s+)", " ");

          var index = text.IndexOf("\r\n\r\n", System.StringComparison.Ordinal);

          yield return new string[] { text[..index].Trim(), text[index..].Trim() };

          entry.Clear();
        }

        entry.AppendLine(e.Current);
      }
    }

    public static System.Data.DataTable CreateGutenbergTenThousandWonderfulThings()
      => GetGutenbergTenThousandWonderfulThings().ToDataTable(true, true, "TenThousandWonderfulThings");

    #endregion // Gutenberg TenthousandWonderfulThings
  }
}
