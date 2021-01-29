using System.Linq;

namespace Flux
{
  public static partial class CsvEm
  {
    /// <summary>Returns a new sequence of string arrays from the <see cref="System.IO.Stream"/>, utilizing a <see cref="Flux.Text.CsvReader"/> with the specified options (defaults if null).</summary>
    public static System.Collections.Generic.IEnumerable<string[]> ReadCsv(this System.IO.Stream source, Text.Csv.CsvOptions? options)
    {
      using var reader = new Text.Csv.CsvReader(source, options ?? new Text.Csv.CsvOptions());

      for (reader.ReadToNextToken(); reader.TokenType != Text.Csv.CsvTokenType.None; reader.ReadToNextToken())
        if (reader.TokenType == Text.Csv.CsvTokenType.EndLine)
          yield return reader.FieldValues.ToArray();
      //foreach (var array in reader.ReadArrays())
      //  yield return array;
    }
  }
}
