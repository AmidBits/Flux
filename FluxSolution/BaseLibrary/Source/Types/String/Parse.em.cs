namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Parses the string for a Unicode notation expression.</summary>
    public static int ParseUnicodeNotation(this string source)
      => Text.UnicodeNotation.Parse(source);
    /// <summary>Parses the string for a Unicode notation expression.</summary>
    public static int ParseUnicodeStringLiteral(this string source)
      => Text.UnicodeStringLiteral.Parse(source);

    /// <summary>Parses the string as a CSV 'line' and creates a list of sub-strings representing the comma separated values (or fields).</summary>
    /// <remarks>Uses a more </remarks>
    public static System.Collections.Generic.List<string> ParseCsvLine(this string source, char separator = ',')
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var list = new System.Collections.Generic.List<string>();

      var startIndex = -1;
      var quoteCounter = 0;

      for (var index = 0; index < source.Length; index++)
      {
        var read = source[index];

        if (startIndex == -1)
          startIndex = index;

        if (read == separator && (quoteCounter & 1) == 0)
        {
          list.Add(GetField(startIndex, index));

          startIndex = -1;
          quoteCounter = 0;
        }
        else if (read == '"')
          quoteCounter++;
      }

      list.Add(startIndex > -1 ? GetField(startIndex, source.Length) : string.Empty);

      return list;

      string GetField(int left, int right)
        => source[left] != '"' ? source.Substring(left, right - left) : source.Substring(left + 1, right - left - 2).Replace("\"\"", "\"", System.StringComparison.OrdinalIgnoreCase);
    }
  }
}
