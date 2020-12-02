namespace Flux
{
  public static partial class CsvEm
  {
    public static string CsvEscape(this string source, char fieldSeparator)
      => !string.IsNullOrEmpty(source) && source.Contains('"', System.StringComparison.Ordinal) ? source.Replace("\"", "\"\"", System.StringComparison.Ordinal).Wrap('"', '"') : source.ContainsAny(System.Linq.Enumerable.Empty<char>().Append(fieldSeparator, '\r', '\n')) ? source.Wrap('"', '"') : source;

    public static string CsvUnescape(this string source)
      => source?.Unwrap('"', '"').Replace("\"\"", "\"", System.StringComparison.Ordinal) ?? string.Empty;
  }
}
