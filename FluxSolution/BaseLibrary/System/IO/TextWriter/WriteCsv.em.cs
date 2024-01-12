namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Writes a single CSV line using the provided field values.</para>
    /// </summary>
    /// <param name="writer">The <see cref="System.IO.TextWriter"/>.</param>
    /// <param name="delimiter">The delimiter that separates the values.</param>
    /// <param name="enforceDoubleQuotes">Whether to always add double quotes (true) or only add then when needed (false).</param>
    /// <param name="data">The CSV data.</param>
    public static void WriteCsvLine(this System.IO.TextWriter writer, string delimiter = ",", bool enforceDoubleQuotes = false, params string[] data)
      => writer.WriteLine(string.Join(delimiter, data.Select(f => enforceDoubleQuotes || f.Contains(delimiter) || f.Contains(System.Environment.NewLine) ? $"\"{f.Replace("\"", "\"\"")}\"" : f)));

    /// <summary>
    /// <para>Writes an entire CSV file (table).</para>
    /// </summary>
    /// <param name="writer">The <see cref="System.IO.TextWriter"/>.</param>
    /// <param name="delimiter">The delimiter that separates the values.</param>
    /// <param name="enforceDoubleQuotes">Whether to always add double quotes (true) or only add then when needed (false).</param>
    /// <param name="data">The CSV data.</param>
    public static void WriteCsvFile(this System.IO.TextWriter writer, string delimiter = ",", bool enforceDoubleQuotes = false, params string[][] data)
    {
      for (var l = 0; l < data.Length; l++)
        WriteCsvLine(writer, delimiter, enforceDoubleQuotes, data[l]);
    }
  }
}
