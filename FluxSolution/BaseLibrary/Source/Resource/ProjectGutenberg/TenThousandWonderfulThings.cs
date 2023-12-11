namespace Flux.Resources.ProjectGutenberg
{
  public sealed class TenThousandWonderfulThings
    : ITabularDataAcquirable
  {
    public static readonly System.Uri Local = new(@"file://\Resources\ProjectGutenberg\pg45849.txt");
    public static readonly System.Uri Origin = new(@"http://www.gutenberg.org/ebooks/45849.txt.utf-8");

    public System.Uri Uri { get; private set; } = Local;

    /// <summary>Returns project Gutenberg's Ten thousand wonderful things data. No field names.</summary>
    public static System.Collections.Generic.IEnumerable<string[]> GetData(System.Uri uri)
    {
      var reTitle = new System.Text.RegularExpressions.Regex(@"^[\!\-\:\;\'\""\,\.\? A-Z0-9]+$", System.Text.RegularExpressions.RegexOptions.Compiled);

      using var stream = uri.GetStream();
      using var reader = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8);

      using var e = reader.ReadLines(s => true, s => s).GetEnumerator();

      var entry = new System.Text.StringBuilder();

      while (e.MoveNext())
        if (e.Current.Equals(@"TEN THOUSAND WONDERFUL THINGS.", System.StringComparison.Ordinal)) // This is where we start enumerating lines from the text.
          break;

      while (e.MoveNext())
      {
        if (e.Current.Equals(@"  PRINTED BY WILLIAM CLOWES AND SONS, LIMITED, LONDON AND DECCLES.", System.StringComparison.Ordinal)) // This is where we stop enumerating lines from the text.
          break;

        if (entry.EndsWith("\r\n\r\n\r\n") && reTitle.IsMatch(e.Current))
        {
          var text = entry.ToString().Trim();
          var index = text.IndexOf("\r\n\r\n", System.StringComparison.Ordinal);

          yield return new string[] { text[..index], text[index..].Trim() };

          entry.Clear();
        }

        entry.AppendLine(e.Current);
      }
    }

    #region Implemented interfaces

    public string[] FieldNames => new string[] { "Title", "Text" };
    public System.Type[] FieldTypes => FieldNames.Select(s => typeof(string)).ToArray();

    public System.Collections.Generic.IEnumerable<object[]> GetFieldValues() => GetData(Uri);

    #endregion // Implemented interfaces
  }
}
