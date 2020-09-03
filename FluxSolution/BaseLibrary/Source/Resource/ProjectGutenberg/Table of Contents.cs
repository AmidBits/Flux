using System.Linq;

namespace Flux.Resources.ProjectGutenberg
{
  /// <summary>Get all the book title/author and number available in the Gutenberg project (from gutenberg.com</summary>
  public static partial class TableOfContents
  {
    public static System.Collections.Generic.IList<string> FieldNames
      => new string[] { @"Ebook", @"Number" };

    public static System.Uri LocalUri
      => new System.Uri(@"file://\Resources\ProjectGutenberg\GUTINDEX.ALL");
    public static System.Uri SourceUri
      => new System.Uri(@"http://www.gutenberg.org/dirs/GUTINDEX.ALL");

    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<string>> GetData(System.Uri uri)
      => uri.ReadLines(System.Text.Encoding.UTF8).Where(s => s.Length == 78 && System.Text.RegularExpressions.Regex.IsMatch(s, @"^[\p{L}\p{N}\p{Zs}\p{P}]+\s{2,}\d+$")).Select(s => System.Text.RegularExpressions.Regex.Split(s, @"(?<=^.+)\s{2,}(?=\d+$)"));

    public static Flux.Data.EnumerableDataReader<System.Collections.Generic.IList<string>> GetDataReader(System.Uri uri)
      => new Flux.Data.EnumerableDataReader<System.Collections.Generic.IList<string>>(GetData(uri), dr => (System.Collections.Generic.IList<object>)dr, FieldNames);
  }
}
