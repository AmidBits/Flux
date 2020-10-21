using System.Net.Http.Headers;

namespace Flux.Resources.ProjectGutenberg
{
  /// <summary>Get the lines of Webster's Unabridged Dictionary (from gutenberg.com</summary>
  /// <example>var words = Flux.Resources.GetGutenbergWebstersUnabridgedDictionary().Select(idr => idr.GetString(0)).Where(s => System.Text.RegularExpressions.Regex.IsMatch(s, @"^[A-Z]+$")).Select(s => s.ToLower()).ToArray();</example>
  /// <see cref="http://www.gutenberg.org/ebooks/29765"/>
  // Download URL: http://www.gutenberg.org/ebooks/29765.txt.utf-8
  public class TenThousandWonderfulThings
    : Conversions
  {
    public static System.Uri LocalUri
      => new System.Uri(@"file://\Resources\ProjectGutenberg\pg45849.txt");
    public static System.Uri SourceUri
      => new System.Uri(@"http://www.gutenberg.org/ebooks/45849.txt.utf-8");

    public override System.Collections.Generic.IList<string> FieldNames
      => new string[] { @"Title", @"Text" };
    public override System.Collections.Generic.IList<System.Type> FieldTypes
      => new System.Type[] { typeof(string), typeof(string) };

    public override System.Collections.Generic.IEnumerable<string[]> GetStrings(System.Uri uri)
    {
      using var e = uri.ReadLines(System.Text.Encoding.UTF8).GetEnumerator();

      var thing = new System.Text.StringBuilder();

      while (e.MoveNext())
        if (e.Current.Equals(@"TEN THOUSAND WONDERFUL THINGS.", System.StringComparison.Ordinal)) // This is where we start enumerating lines from the text.
          break;

      while (e.MoveNext())
      {
        if (e.Current.Equals(@"  PRINTED BY WILLIAM CLOWES AND SONS, LIMITED, LONDON AND DECCLES.", System.StringComparison.Ordinal)) // This is where we stop enumerating lines from the text.
          break;

        if (thing.EndsWith("\r\n\r\n\r\n") && System.Text.RegularExpressions.Regex.IsMatch(e.Current, @"^[\!\-\:\;\'\""\,\.\? A-Z0-9]+$"))
        {
          var text = thing.ToString().Trim();
          var index = text.IndexOf("\r\n", System.StringComparison.Ordinal);

          yield return new string[] { text.Substring(0, index), text.Substring(index).Trim() };

          thing.Clear();
        }

        thing.AppendLine(e.Current);
      }
    }
    public override System.Collections.Generic.IEnumerable<object[]> GetObjects(System.Uri uri)
    {
      foreach (var strings in GetStrings(uri))
      {
        yield return new object[] { strings[0], System.Text.RegularExpressions.Regex.Replace(strings[1], @"(?<!\r\n)(\r\n)(?!\r\n)", " ").Trim() };
      }
    }
  }
}
