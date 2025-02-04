namespace Flux
{
  public static partial class Resource
  {
    #region Gutenberg SynonymsAndAntonymsSamuelFallows

    [System.Text.RegularExpressions.GeneratedRegexAttribute(@".(?=>key:|syn:|ant:)", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase)]
    private static partial System.Text.RegularExpressions.Regex RegexGutenbergSynonymsAndAntonymsBySamuelFallows();

    /// <summary>
    /// <para>A Complete Dictionary of Synonyms and Antonyms by Samuel Fallows (Acdsasf).</para>
    /// <para><remarks>Returns keywords, synonyms and antonyms.</remarks></para>
    /// <para><see href="http://www.gutenberg.org/ebooks/51155"/></para>
    /// <para>Remote: <see href="http://www.gutenberg.org/files/51155/51155-0.txt"/></para>
    /// <para>Local: <see href="file://\Resources\ProjectGutenberg\51155-0.txt"/></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<string[]> GetGutenbergSynonymsAndAntonymsBySamuelFallows(string file = @"file://\Resources\ProjectGutenberg\51155-0.txt")
    {
      yield return new string[] { "Keywords", "Synonyms", "Antonyms" };

      foreach (var item in EnumerateArrays().Select(a => new string[] { string.Join(@",", a[0]), string.Join(@",", a[1]), string.Join(@",", a[2]) }))
        yield return item;

      System.Collections.Generic.IEnumerable<string[][]> EnumerateArrays()
      {
        var lines = new System.Text.StringBuilder();

        using var stream = new System.Uri(file).GetStream();
        using var reader = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8);

        foreach (var line in reader.ReadLines(s => s.Length > 0, s => s))
        {
          if (line == @"=")
          {
            if (lines.IsCommonPrefix(0, @"KEY:"))
            {
              var dictionary = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>>();

              System.Array.ForEach(
                RegexGutenbergSynonymsAndAntonymsBySamuelFallows().Split(lines.RemoveAll(null, '.').ToLower().ToString()),
                s =>
                {
                  var array = s.Split(':');

                  var key = array[0];
                  var value = array[1].Split(',', System.StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(s => s.Trim()).ToList();

                  if (dictionary.TryGetValue(key, out var dictionaryList))
                    dictionaryList.AddRange(value);
                  else
                    dictionary.Add(key, value);
                }
              );

              var key = dictionary.TryGetValue("key", out var lKey) ? lKey : new();
              var syn = dictionary.TryGetValue("syn", out var lSyn) ? lSyn : new();
              var ant = dictionary.TryGetValue("ant", out var lAnt) ? lAnt : new();

              yield return new string[][] { key.ToArray(), syn.ToArray(), ant.ToArray() };
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

    #endregion // Gutenberg SynonymsAndAntonymsSamuelFallows
  }
}
