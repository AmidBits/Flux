namespace Flux
{
  public static partial class CsvEm
  {
    public static string CsvEscape(this string source, char fieldSeparator)
      => source.AsReadOnlySpan().GetCsvEscapeLevel(fieldSeparator) switch
      {
        Text.Csv.CsvEscapeLevel.None => source,
        Text.Csv.CsvEscapeLevel.Enclose => source.Wrap('"', '"'),
        Text.Csv.CsvEscapeLevel.ReplaceAndEnclose => source.ToStringBuilder().Replace("\"", "\"\"").Wrap('"', '"').ToString(),
        _ => source,
      };
  }
}
