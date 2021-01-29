namespace Flux
{
  public static partial class CsvEm
  {
    public static string CsvUnescape(this string source)
      => source?.Unwrap('"', '"').Replace("\"\"", "\"", System.StringComparison.Ordinal) ?? string.Empty;
  }
}
