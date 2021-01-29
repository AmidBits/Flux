namespace Flux
{
  public static partial class CsvEm
  {
    public static Text.Csv.CsvEscapeLevel GetCsvEscapeLevel(this System.ReadOnlySpan<char> source, char fieldSeparator)
    {
      var escapeLevel = Text.Csv.CsvEscapeLevel.None;

      for (var index = source.Length - 1; index >= 0; index--)
      {
        var character = source[index];

        if (character == '"')
          return Text.Csv.CsvEscapeLevel.ReplaceAndEnclose;
        else if (character == fieldSeparator || character == '\r' || character == '\n')
          escapeLevel = Text.Csv.CsvEscapeLevel.Enclose; // Continue checking for '"', no return.
      }

      return escapeLevel;
    }
  }
}
