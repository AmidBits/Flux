namespace Flux
{
  public static partial class XtensionsText
  {
    public static string CsvEscape(this string source, char fieldSeparator)
      => !string.IsNullOrEmpty(source) && source.Contains('"') ? source.Replace("\"", "\"\"").Wrap('"', '"') : source.ContainsAny(fieldSeparator, '\r', '\n') ? source.Wrap('"', '"') : source;

    public static string CsvUnescape(this string source)
      => source?.Unwrap('"', '"').Replace("\"\"", "\"") ?? string.Empty;
  }
}
