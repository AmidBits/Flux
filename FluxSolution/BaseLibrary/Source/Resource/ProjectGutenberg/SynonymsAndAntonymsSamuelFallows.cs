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

        foreach (var line in reader.ReadLines(s => s.Length > 0, s => s))
        {
          if (line == @"=")
          {
            if (lines.StartsWith(@"KEY:"))
            {
              var dictionary = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>>();

              System.Array.ForEach(
                System.Text.RegularExpressions.Regex.Split(lines.RemoveAll(null, '.').ToLowerCase().ToString(), @".(?=>key:|syn:|ant:)", System.Text.RegularExpressions.RegexOptions.IgnoreCase),
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

    #region Implemented interfaces

    public string[] FieldNames => new string[] { "Keywords", "Synonyms", "Antonyms" };
    public Type[] FieldTypes => FieldNames.Select(s => typeof(string)).ToArray();

    public System.Collections.Generic.IEnumerable<object[]> GetFieldValues() => GetData(Uri);

    #endregion // Implemented interfaces
  }
}
