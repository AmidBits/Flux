namespace Flux.Resources1.ProjectGutenberg
{
  public static class TenThousandWonderfulThings
  {
    public static System.Uri UriLocal
      => new System.Uri(@"file://\Resources\ProjectGutenberg\pg45849.txt");
    public static System.Uri UriSource
      => new System.Uri(@"http://www.gutenberg.org/ebooks/45849.txt.utf-8");

    public static System.Collections.Generic.IEnumerable<string[]> Get(System.Uri uri)
    {
      yield return new string[] { @"Title", @"Text" };

      var reTitle = new System.Text.RegularExpressions.Regex(@"^[\!\-\:\;\'\""\,\.\? A-Z0-9]+$", System.Text.RegularExpressions.RegexOptions.Compiled);

      using var e = uri.GetStream().ReadLines(System.Text.Encoding.UTF8, true).GetEnumerator();

      var entry = new System.Text.StringBuilder();

      while (e.MoveNext())
        if (e.Current.Equals(@"TEN THOUSAND WONDERFUL THINGS.", System.StringComparison.Ordinal)) // This is where we start enumerating lines from the text.
          break;

      while (e.MoveNext())
      {
        if (e.Current.Equals(@"  PRINTED BY WILLIAM CLOWES AND SONS, LIMITED, LONDON AND DECCLES.", System.StringComparison.Ordinal)) // This is where we stop enumerating lines from the text.
          break;

        if (entry.EndsWith("\r\n\r\n\r\n") && reTitle.IsMatch(e.Current))
        {
          var text = entry.ToString().Trim();
          var index = text.IndexOf("\r\n", System.StringComparison.Ordinal);

          yield return new string[] { text.Substring(0, index), text.Substring(index).Trim() };

          entry.Clear();
        }

        entry.AppendLine(e.Current);
      }
    }
  }
}
