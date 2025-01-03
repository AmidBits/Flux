namespace Flux.Resources.ProjectGutenberg
{
  /// <summary>Get the lines of Webster's Unabridged Dictionary (from gutenberg.com</summary>
  /// <example>var words = Flux.Resources.GetGutenbergWebstersUnabridgedDictionary().Select(idr => idr.GetString(0)).Where(s => System.Text.RegularExpressions.Regex.IsMatch(s, @"^[A-Z]+$")).Select(s => s.ToLower()).ToArray();</example>
  /// <see cref="http://www.gutenberg.org/ebooks/29765"/>
  // Download URL: http://www.gutenberg.org/ebooks/29765.txt.utf-8
  public sealed class WebstersUnabridgedDictionary
    : ITabularDataAcquirable
  {
    public static readonly System.Uri Local = new(@"file://\Resources\ProjectGutenberg\pg29765.txt");
    public static readonly System.Uri Origin = new(@"http://www.gutenberg.org/ebooks/29765.txt.utf-8");

    public System.Uri Uri { get; private set; } = Local;

    /// <summary>Returns project Gutenberg's Websters unabridged dictionary data. No field names.</summary>
    public static System.Collections.Generic.IEnumerable<string[]> GetData(System.Uri uri)
    {
      var m_reSplitWords = new System.Text.RegularExpressions.Regex(@"\s*;\s*", System.Text.RegularExpressions.RegexOptions.Compiled);
      var m_reWord = new System.Text.RegularExpressions.Regex(@"^[A-Z \-';]+$", System.Text.RegularExpressions.RegexOptions.Compiled);

      var word = new System.Text.StringBuilder();
      var definition = new System.Text.StringBuilder();

      using var stream = uri.GetStream();
      using var reader = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8);

      foreach (var line in reader.ReadLines(s => s.Length > 0, s => s))
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
            yield return ReturnStrings();

            word.Clear();
            definition.Clear();
          }

          word.Append(match.Value.Trim());
          if (line.Length > match.Value.Length) definition.Append(line[match.Value.Length..]);
        }
        else if (word.Length > 0)
        {
          if (line.Length > 0)
          {
            if (line.StartsWith("End of Project Gutenberg"))
            {
              yield return ReturnStrings();

              yield break;
            }

            if (definition.Length > 0 && !definition.ToString().EndsWith(System.Environment.NewLine, System.StringComparison.Ordinal)) definition.Append(' ');
            definition.Append(line.Trim());
          }
          else definition.Append(System.Environment.NewLine);
        }
      }

      string[] ReturnStrings()
      {
        return new string[] { string.Join(System.Environment.NewLine, m_reSplitWords.Split(word.ToString())), definition.ToString() };
      }
    }

    #region Implemented interfaces

    public string[] FieldNames => new string[] { "Title", "Text" };
    public System.Type[] FieldTypes => FieldNames.Select(s => typeof(string)).ToArray();

    public System.Collections.Generic.IEnumerable<object[]> GetFieldValues() => GetData(Uri);

    #endregion // Implemented interfaces
  }
}
