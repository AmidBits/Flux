namespace Flux.Resources.ProjectGutenberg
{
  /// <summary>Get all the book title/author and number available in the Gutenberg project (from gutenberg.com</summary>
  public sealed partial class TableOfContents
    : ITabularDataAcquirable
  {
    public static readonly System.Uri Local = new(@"file://\Resources\ProjectGutenberg\GUTINDEX.ALL");
    public static readonly System.Uri Origin = new(@"http://www.gutenberg.org/dirs/GUTINDEX.ALL");

    public System.Uri Uri { get; private set; } = Local;

    [System.Text.RegularExpressions.GeneratedRegexAttribute(@"^[\p{L}\p{N}\p{Zs}\p{P}]+\s{2,}\d+$", System.Text.RegularExpressions.RegexOptions.Compiled)]
    private static partial System.Text.RegularExpressions.Regex MatchRegex();

    [System.Text.RegularExpressions.GeneratedRegexAttribute(@"(?<=^.+)\s{2,}(?=\d+$)", System.Text.RegularExpressions.RegexOptions.Compiled)]
    private static partial System.Text.RegularExpressions.Regex SplitRegex();

    /// <summary>Returns project Gutenberg's table of contents data. No field names.</summary>
    public static System.Collections.Generic.IEnumerable<string[]> GetData(System.Uri uri)
    {
      using var stream = uri.GetStream();
      using var reader = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8);

      foreach (var fields in reader.ReadLines(s => s.Length == 78 && MatchRegex().IsMatch(s), SplitRegex().Split))
        yield return fields;
    }

    #region Implemented interfaces

    public string[] FieldNames => new string[] { "Ebook", "Number" };
    public Type[] FieldTypes => FieldNames.Select(s => typeof(string)).ToArray();

    public System.Collections.Generic.IEnumerable<object[]> GetFieldValues() => GetData(Uri);

    #endregion // Implemented interfaces
  }
}
