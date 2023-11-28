namespace Flux.Resources.ProjectGutenberg
{
  /// <summary>A Complete Dictionary of Synonyms and Antonyms by Samuel Fallows (Acdsasf).</summary>
  /// <remarks>Returns keywords, synonyms and antonyms.</summary>
  /// <see cref="http://www.gutenberg.org/ebooks/51155"/>
  public sealed class SynonymsAndAntonymsSamuelFallows
    : ITabularDataAcquirable
  {
    public static readonly System.Uri Local = new(@"file://\Resources\ProjectGutenberg\51155-0.txt");
    public static readonly System.Uri Origin = new(@"http://www.gutenberg.org/files/51155/51155-0.txt");

    public System.Uri Uri { get; private set; } = Local;

    /// <summary>Returns project Gutenberg's Synonyms and antonyms by Samuel Fallows data. No field names.</summary>
    public static System.Collections.Generic.IEnumerable<string[]> GetData(System.Uri uri)
    {
      var reSection = new System.Text.RegularExpressions.Regex(@"(?<=(KEY:|SYN:|ANT:))\s", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase);

      foreach (var item in EnumerateArrays().Select(a => new string[] { string.Join(@",", a[0]), string.Join(@",", a[1]), string.Join(@",", a[2]) }))
        yield return item;

      System.Collections.Generic.IEnumerable<string[][]> EnumerateArrays()
      {
        var lines = new System.Text.StringBuilder();

        using var stream = uri.GetStream();
        using var reader = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8);

        foreach (var line in reader.ReadLines(false))
        {
          if (line == @"=" || line.Length == 0)
          {
            if (lines.StartsWith(@"KEY:"))
            {
              var list = reSection.Replace(lines.ToString(), @",").ToLower(System.Globalization.CultureInfo.CurrentCulture).Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim(' ', '.')).Where(s => s.Length > 0).ToList();

              var iKey = list.IndexOf(@"key:");
              var iSyn = list.IndexOf(@"syn:");
              var iAnt = list.IndexOf(@"ant:");

              var listKey = iKey > -1 ? list.GetRange(iKey + 1, (iSyn > -1 ? iSyn : iAnt > -1 ? iAnt : list.Count) - 1) : new();
              var listSyn = iSyn > -1 ? list.GetRange(iSyn + 1, (iAnt > -1 ? iAnt - iSyn : list.Count - iSyn) - 1) : new();
              var listAnt = iAnt > -1 ? list.GetRange(iAnt + 1, (list.Count - iAnt) - 1) : new();

              if (listKey.Count > 0)
                yield return new string[][] { listKey.ToArray(), listSyn.ToArray(), listAnt.ToArray() };
            }

            lines.Clear();
          }
          else
          {
            if (lines.Length > 0)
              lines.Append(',');

            lines.Append(line);
          }
        }
      }
    }

    #region Implemented interfaces

    public string[] FieldNames => new string[] { "Keywords", "Synonyms", "Antonyms" };
    public Type[] FieldTypes => FieldNames.Select(s => typeof(string)).ToArray();

    public System.Collections.Generic.IEnumerable<object[]> GetFieldValues() => GetData(Uri);

    #endregion // Implemented interfaces
  }
}
