namespace Flux.Resources.ProjectGutenberg
{
  /// <summary>Get the lines of Webster's Unabridged Dictionary (from gutenberg.com</summary>
  /// <example>var words = Flux.Resources.GetGutenbergWebstersUnabridgedDictionary().Select(idr => idr.GetString(0)).Where(s => System.Text.RegularExpressions.Regex.IsMatch(s, @"^[A-Z]+$")).Select(s => s.ToLower()).ToArray();</example>
  /// <see cref="http://www.gutenberg.org/ebooks/29765"/>
  // Download URL: http://www.gutenberg.org/ebooks/29765.txt.utf-8
  public class WebstersUnabridgedDictionary
    : ResourceFactory
  {
    public static System.Uri LocalUri
      => new System.Uri(@"file://\Resources\ProjectGutenberg\pg29765.txt");
    public static System.Uri SourceUri
      => new System.Uri(@"http://www.gutenberg.org/ebooks/29765.txt.utf-8");

    public override System.Collections.Generic.IList<string> FieldNames
      => new string[] { @"Word", @"Definition" };
    public override System.Collections.Generic.IList<System.Type> FieldTypes
      => new System.Type[] { typeof(string), typeof(string) };

    private static readonly System.Text.RegularExpressions.Regex m_reSplitWords = new System.Text.RegularExpressions.Regex(@"\s*;\s*", System.Text.RegularExpressions.RegexOptions.Compiled);
    private static readonly System.Text.RegularExpressions.Regex m_reWord = new System.Text.RegularExpressions.Regex(@"^[A-Z \-';]+$", System.Text.RegularExpressions.RegexOptions.Compiled);

    public override System.Collections.Generic.IEnumerable<string[]> GetStrings(System.Uri uri)
    {
      yield return (string[])FieldNames;

      var word = new System.Text.StringBuilder();
      var definition = new System.Text.StringBuilder();

      foreach (var line in uri.GetStream().ReadLines(System.Text.Encoding.UTF8))
      {
        if (m_reWord.Match(line) is var match && match.Success)
        {
          if (word.Length > 0 && definition.Length == 0)
          {
            word.Append(' ');
            word.Append(match.Value.Trim());

            continue;
          }
          else if (word.Length > 0 && definition.Length > 0)
          {
            yield return new string[] { string.Join(System.Environment.NewLine, m_reSplitWords.Split(word.ToString())), definition.ToString() };

            word.Clear();
            definition.Clear();
          }

          word.Append(match.Value.Trim());
          if (line.Length > match.Value.Length) definition.Append(line.Substring(match.Value.Length));
        }
        else if (word.Length > 0)
        {
          if (line.Length > 0)
          {
            if (definition.Length > 0 && !definition.ToString().EndsWith(System.Environment.NewLine, System.StringComparison.Ordinal)) definition.Append(' ');
            definition.Append(line.Trim());
          }
          else definition.Append(System.Environment.NewLine);
        }
      }
    }
  }
}

//System.Console.WriteLine(nameof(Flux.Resources.ProjectGutenberg.WebstersUnabridgedDictionary));
//foreach (var strings in new Flux.Resources.ProjectGutenberg.WebstersUnabridgedDictionary().GetStrings(Flux.Resources.ProjectGutenberg.WebstersUnabridgedDictionary.SourceUri))
//  System.Console.WriteLine(string.Join('|', strings));
