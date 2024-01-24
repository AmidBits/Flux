namespace Flux.Resources.Scowl
{
  /// <summary>The records from 2Of12Full word list.</summary>
  /// <see href="https://github.com/en-wl/wordlist"/>
  // Download URL: https://github.com/en-wl/wordlist/raw/master/alt12dicts/2of12full.txt
  /// <seealso cref="http://wordlist.aspell.net/"/>
  /// <seealso cref="https://github.com/en-wl/wordlist/blob/master/"/>
  public sealed partial class TwoOfTwelveFull
    : ITabularDataAcquirable
  {
    [System.Text.RegularExpressions.GeneratedRegexAttribute(@"(?<=[\-0-9]+[:#&=]?)\s+", System.Text.RegularExpressions.RegexOptions.Compiled)]
    private static partial System.Text.RegularExpressions.Regex SplitRegex();

    public static readonly System.Uri Local = new(@"file://\Resources\Scowl\2of12full.txt");
    public static readonly System.Uri Origin = new(@"https://raw.githubusercontent.com/en-wl/wordlist/master/alt12dicts/2of12full.txt");

    public System.Uri Uri { get; private set; } = Local;

    /// <summary>Returns two of twelve full data. No field names.</summary>
    public static System.Collections.Generic.IEnumerable<string[]> GetData(System.Uri uri)
    {
      using var stream = uri.GetStream();
      using var reader = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8);

      foreach (var fields in reader.ReadLines(s => s.Length > 0, s => SplitRegex().Split(s.Trim())))
        yield return fields;
    }

    #region Implemented interfaces

    public string[] FieldNames => new string[] { "DictionaryCount", "NonVariant", "Variant", "NonAmerican", "SecondClass", "Word" };
    public System.Type[] FieldTypes => FieldNames.Select(s => typeof(string)).ToArray();

    public System.Collections.Generic.IEnumerable<object[]> GetFieldValues() => GetData(Uri);

    #endregion // Implemented interfaces
  }
}
