namespace Flux
{
  public static partial class XtensionTextWriter
  {
    extension(System.IO.TextWriter source)
    {
      /// <summary>
      /// <para>Writes a jagged array as CSV (tabular) <paramref name="data"/> to the <paramref name="writer"/>.</para>
      /// </summary>
      /// <param name="writer">The <see cref="System.IO.TextWriter"/>.</param>
      /// <param name="data">The CSV data.</param>
      /// <param name="delimiter">The delimiter that separates the values.</param>
      /// <param name="encloseInQuotes">Whether to always add double quotes (true) or only add then when needed (false).</param>
      /// <param name="trimWhiteSpace">Indicates whether leading and trailing white space should be trimmed from field values.</param>
      public void WriteCsv(string[][] data, string delimiter = ",", bool encloseInQuotes = true, bool trimWhiteSpace = false)
      {
        for (var l = 0; l < data.Length; l++)
          source.WriteLine(string.Join(delimiter, data[l].Select(s => trimWhiteSpace ? s.Trim() : s).Select(s => encloseInQuotes || s.Contains(delimiter) || s.Contains(System.Environment.NewLine) ? $"\"{s.Replace("\"", "\"\"")}\"" : s)));
      }
    }
  }
}
