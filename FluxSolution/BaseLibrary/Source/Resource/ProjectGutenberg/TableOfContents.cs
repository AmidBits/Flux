namespace Flux.Resources.ProjectGutenberg
{
  /// <summary>Get all the book title/author and number available in the Gutenberg project (from gutenberg.com</summary>
  public sealed class TableOfContents
    : ITabularDataAcquirable
  {
    public static string LocalFile
      => @"file://\Resources\ProjectGutenberg\GUTINDEX.ALL";
    public static System.Uri SourceUri
      => new(@"http://www.gutenberg.org/dirs/GUTINDEX.ALL");

    public System.Uri Uri { get; private set; }

    public TableOfContents(System.Uri uri)
      => Uri = uri;

    public string[] FieldNames
      => new string[] { @"Ebook", @"Number" };
    public Type[] FieldTypes
      => FieldNames.Select(s => typeof(string)).ToArray();

    public System.Collections.Generic.IEnumerable<object[]> GetFieldValues()
      => GetStrings();

    /// <summary>Returns project Gutenberg's table of contents data. No field names.</summary>
    public System.Collections.Generic.IEnumerable<string[]> GetStrings()
    {
      var reMatch = new System.Text.RegularExpressions.Regex(@"^[\p{L}\p{N}\p{Zs}\p{P}]+\s{2,}\d+$", System.Text.RegularExpressions.RegexOptions.Compiled);
      var reSplit = new System.Text.RegularExpressions.Regex(@"(?<=^.+)\s{2,}(?=\d+$)", System.Text.RegularExpressions.RegexOptions.Compiled);

      using var sr = new System.IO.StreamReader(Uri.GetStream(), System.Text.Encoding.UTF8);

      foreach (var line in sr.ReadLines(false))
        if (line.Length == 78 && reMatch.IsMatch(line))
          yield return reSplit.Split(line);
    }
  }
}
