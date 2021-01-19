using System.Linq;

namespace Flux.Resources1.ProjectGutenberg
{
  public static class TableOfContents
  {
    public static System.Uri UriLocal
      => new System.Uri(@"file://\Resources\ProjectGutenberg\GUTINDEX.ALL");
    public static System.Uri UriSource
      => new System.Uri(@"http://www.gutenberg.org/dirs/GUTINDEX.ALL");

    /// <summary>Get all the book title/author and number available in the Gutenberg project (from gutenberg.com</summary>
    public static System.Collections.Generic.IEnumerable<string[]> Get(System.Uri uri)
    {
      yield return new string[] { @"Ebook", @"Number" };

      var reMatch = new System.Text.RegularExpressions.Regex(@"^[\p{L}\p{N}\p{Zs}\p{P}]+\s{2,}\d+$", System.Text.RegularExpressions.RegexOptions.Compiled);
      var reSplit = new System.Text.RegularExpressions.Regex(@"(?<=^.+)\s{2,}(?=\d+$)", System.Text.RegularExpressions.RegexOptions.Compiled);

      foreach (var item in uri.GetStream().ReadLines(System.Text.Encoding.UTF8).Where(s => s.Length == 78 && reMatch.IsMatch(s)).Select(s => reSplit.Split(s)))
        yield return item;
    }
  }
}
