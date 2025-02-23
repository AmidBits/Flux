namespace Flux
{
  public static partial class Resources
  {
    #region Gutenberg TableOfContents

    [System.Text.RegularExpressions.GeneratedRegexAttribute(@"^[\p{L}\p{N}\p{Zs}\p{P}]+\s{2,}\d+$", System.Text.RegularExpressions.RegexOptions.Compiled)]
    private static partial System.Text.RegularExpressions.Regex RegexGutenbergTableOfContentsMatch();

    [System.Text.RegularExpressions.GeneratedRegexAttribute(@"(?<=^.+)\s{2,}(?=\d+$)", System.Text.RegularExpressions.RegexOptions.Compiled)]
    private static partial System.Text.RegularExpressions.Regex RegexGutenbergTableOfContentsSplit();

    /// <summary>
    /// <para>Get all the book title/author and number available in the Gutenberg project (from gutenberg.com</para>
    /// <para>Local: <see href="file://\Resources\ProjectGutenberg\GUTINDEX.ALL"/></para>
    /// <para>Remote: <see href="http://www.gutenberg.org/dirs/GUTINDEX.ALL"/></para>
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<string[]> GetGutenbergTableOfContents(string file = @"file://\Resources\ProjectGutenberg\GUTINDEX.ALL")
    {
      yield return new string[] { "Ebook", "Number" };

      using var stream = new System.Uri(file).GetStream();
      using var reader = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8);

      foreach (var fields in reader.ReadLines(s => s.Length == 78 && RegexGutenbergTableOfContentsMatch().IsMatch(s), RegexGutenbergTableOfContentsSplit().Split))
        yield return fields;
    }

    #endregion // Gutenberg TableOfContents
  }
}
