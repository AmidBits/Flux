namespace Flux
{
  public static partial class Resource
  {
    #region Gutenberg WebstersUnabridgedDictionary

    [System.Text.RegularExpressions.GeneratedRegexAttribute(@"^[A-Z \-';]+$", System.Text.RegularExpressions.RegexOptions.Compiled)]
    private static partial System.Text.RegularExpressions.Regex RegexGutenbergWebstersUnabridgedDictionaryWord();

    [System.Text.RegularExpressions.GeneratedRegexAttribute(@"\s*;\s*", System.Text.RegularExpressions.RegexOptions.Compiled)]
    private static partial System.Text.RegularExpressions.Regex RegexGutenbergWebstersUnabridgedDictionaryWords();

    /// <summary>
    /// <para>Get the lines of Webster's Unabridged Dictionary (from gutenberg.com)</para>
    /// <para><remarks>Returns keywords, synonyms and antonyms.</remarks></para>
    /// <para><see href="http://www.gutenberg.org/ebooks/29765"/></para>
    /// <para>Remote: <see href="http://www.gutenberg.org/ebooks/29765.txt.utf-8"/></para>
    /// <para>Local: <see href="file://\Resources\ProjectGutenberg\pg29765.txt"/></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<string[]> GetGutenbergWebstersUnabridgedDictionary(string file = @"file://\Resources\ProjectGutenberg\pg29765.txt")
    {
      yield return new string[] { "Title", "Text" };

      var word = new System.Text.StringBuilder();
      var definition = new System.Text.StringBuilder();

      using var stream = new System.Uri(file).GetStream();
      using var reader = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8);

      foreach (var line in reader.ReadLines(s => s.Length > 0, s => s))
      {
        if (RegexGutenbergWebstersUnabridgedDictionaryWord().Match(line) is var match && match.Success)
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
        return new string[] { string.Join(System.Environment.NewLine, RegexGutenbergWebstersUnabridgedDictionaryWords().Split(word.ToString())), definition.ToString() };
      }
    }

    #endregion // Gutenberg WebstersUnabridgedDictionary
  }
}
