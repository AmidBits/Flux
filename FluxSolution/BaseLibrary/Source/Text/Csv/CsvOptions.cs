namespace Flux.Text
{
  public class CsvOptions
  {
    public System.Text.Encoding Encoding { get; private set; }

    public System.Collections.Generic.List<char> EscapeCharacters { get; private set; }

    //public System.Type[] FieldTypes { get; private set; }

    public char FieldSeparator { get; private set; }
    //public string LineSeparator { get; private set; } = System.Environment.NewLine;

    //public bool ByteArrayAsBase64 { get; private set; } = false;
    //public bool DateTimeAsO { get; private set; } = false;
    //public bool DBNullAsUnicodeNull { get; private set; } = false;
    //public bool NullAsUnicodeNull { get; private set; } = false;

    public CsvOptions(char fieldSeparator, System.Text.Encoding encoding)
    {
      Encoding = encoding;
      EscapeCharacters = new System.Collections.Generic.List<char>() { fieldSeparator, '"', '\r', '\n' };
      FieldSeparator = fieldSeparator;
    }
    public CsvOptions()
      : this(',', System.Text.Encoding.UTF8)
    {
    }
  }

  //public class Csv
  //{
  //  public const string Zero = "\u0000";

  //  public const string DefaultColumnQuoteLeft = "\"";
  //  public const string DefaultColumnQuoteRight = "\"";
  //  public const string DefaultColumnSeparator = ",";
  //  public const string DefaultRowSeparator = "\u000d\u000a";
  //  public const string DefaultColumnPadding = " ";

  //  private readonly string m_columnQuoteLeft;
  //  private readonly string m_columnQuoteRight;
  //  private readonly string m_columnSeparator;
  //  private readonly string m_rowSeparator;
  //  private readonly int[]? m_fixedColumnLengths;
  //  private readonly string[]? m_fixedColumnPadDirections;
  //  private readonly string m_fixedColumnPadding;

  //  public Csv()
  //  {
  //    m_columnQuoteLeft = DefaultColumnQuoteLeft;
  //    m_columnQuoteRight = DefaultColumnQuoteRight;
  //    m_columnSeparator = DefaultColumnSeparator;
  //    m_rowSeparator = DefaultRowSeparator;
  //    m_fixedColumnLengths = null;
  //    m_fixedColumnPadDirections = null;
  //    m_fixedColumnPadding = DefaultColumnPadding;
  //  }
  //  public Csv(string columnQuoteLeft, string columnQuoteRight, string columnSeparator, string rowSeparator, string fixedColumnLengths, string fixedColumnPadding, string fixedColumnPadDirections)
  //  {
  //    columnQuoteLeft = ConvertEscapedUnicodeCharacters(columnQuoteLeft, DefaultColumnQuoteLeft);
  //    m_columnQuoteLeft = columnQuoteLeft.Equals(Zero) ? string.Empty : columnQuoteLeft;

  //    columnQuoteRight = ConvertEscapedUnicodeCharacters(columnQuoteRight, DefaultColumnQuoteRight);
  //    m_columnQuoteRight = columnQuoteRight.Equals(Zero) ? string.Empty : columnQuoteRight;

  //    columnSeparator = ConvertEscapedUnicodeCharacters(columnSeparator, DefaultColumnSeparator);
  //    m_columnSeparator = columnSeparator.Equals(Zero) ? string.Empty : columnSeparator;

  //    rowSeparator = ConvertEscapedUnicodeCharacters(rowSeparator, DefaultRowSeparator);
  //    m_rowSeparator = rowSeparator.Equals(Zero) ? string.Empty : rowSeparator;

  //    m_fixedColumnLengths = fixedColumnLengths?.Split(',').Select(s => int.TryParse(s.Trim(), out var l) ? l : 0).ToArray();

  //    fixedColumnPadding = ConvertEscapedUnicodeCharacters(fixedColumnPadding, DefaultColumnPadding);
  //    m_fixedColumnPadding = string.IsNullOrEmpty(fixedColumnPadding) ? DefaultColumnPadding : fixedColumnPadding;

  //    m_fixedColumnPadDirections = fixedColumnPadDirections?.Split(',').Select(s => s.Trim()).ToArray();
  //  }

  //  public System.Text.StringBuilder Format(System.Collections.Generic.IEnumerable<object[]> data)
  //  {
  //    if ((m_fixedColumnLengths is null || m_fixedColumnLengths.Length == 0) && (m_fixedColumnPadDirections is null || m_fixedColumnPadDirections.Length == 0))
  //    {
  //      var sb = new System.Text.StringBuilder();

  //      foreach (var values in data)
  //      {
  //        sb.Append(m_columnQuoteLeft);

  //        for (var index = 0; index < values.Length; index++)
  //        {
  //          if (index > 0)
  //          {
  //            sb.Append(m_columnQuoteRight); // End of previous column.
  //            sb.Append(m_columnSeparator);
  //            sb.Append(m_columnQuoteLeft); // Start of next column.
  //          }

  //          sb.Append(values[index]);
  //        }

  //        sb.Append(m_columnQuoteRight);

  //        sb.Append(m_rowSeparator);
  //      }

  //      return sb;
  //    }
  //    else return FormatFixed(data);
  //  }
  //  public System.Text.StringBuilder FormatFixed(System.Collections.Generic.IEnumerable<object[]> data)
  //  {
  //    var sb = new System.Text.StringBuilder();

  //    foreach (var values in data)
  //    {
  //      sb.Append(m_columnQuoteLeft);

  //      for (var index = 0; index < values.Length; index++)
  //      {
  //        if (index > 0)
  //        {
  //          sb.Append(m_columnQuoteRight); // End of previous column.
  //          sb.Append(m_columnSeparator);
  //          sb.Append(m_columnQuoteLeft); // Start of next column.
  //        }

  //        var value = values[index].ToString();

  //        if (m_fixedColumnLengths is null || m_fixedColumnLengths.Length == 0) sb.Append(value);
  //        else if (m_fixedColumnPadDirections[index][0] == 'R' || m_fixedColumnPadDirections[index][0] == 'r') sb.Append(values[index].ToString().PadRight(m_fixedColumnLengths[index], m_fixedColumnPadding[0]).Substring(0, m_fixedColumnLengths[index]));
  //        else if (m_fixedColumnPadDirections[index][0] == 'L' || m_fixedColumnPadDirections[index][0] == 'l') sb.Append(values[index].ToString().PadLeft(m_fixedColumnLengths[index], m_fixedColumnPadding[0]).Substring(0, m_fixedColumnLengths[index]));
  //        else throw new System.InvalidOperationException();
  //      }

  //      sb.Append(m_columnQuoteRight);

  //      sb.Append(m_rowSeparator);
  //    }

  //    return sb;
  //  }

  //  /// <summary>Converts any existing \uXXXX or \Uxxxxxxxx expression into the equivalent string value.</summary>
  //  public static string ConvertEscapedUnicodeCharacters(string text, string nullValue)
  //    => text is null ? nullValue : System.Text.RegularExpressions.Regex.Replace(text, @"(\\u[0-9A-Fa-f]{4}|\\U[0-9A-Fa-f]{8})", me => char.ConvertFromUtf32(int.Parse(me.Value.Substring(2), System.Globalization.NumberStyles.HexNumber)));

  //  /// <summary>If double quotes exists in the string, an extra double quote is added to each existing double quote, and then the string is surrounded by double quotes.</summary>
  //  public static string Escape(string text)
  //    => text.Contains("\"") ? text.Replace("\"", "\"\"").Wrap('"', '"', true) : text;
  //  /// <summary>If surrounding double quotes exists in the string, they are removed and then for any two double quotes only one is kept.</summary>
  //  public static string Unescape(string text)
  //    => text.IsWrapped('"', '"') ? text.Unwrap('"', '"').Replace("\"\"", "\"") : text;

  //  /// <summary></summary>
  //  public static System.Collections.Generic.IEnumerable<string> ReadFields(System.Collections.Generic.IEnumerable<string> textElements, string fieldSeparator = @",", System.Collections.Generic.IEqualityComparer<char>? comparer = null)
  //  {
  //    if (comparer is null) comparer = System.Collections.Generic.EqualityComparer<char>.Default;

  //    var field = new System.Text.StringBuilder();

  //    foreach (var text in textElements)
  //    {
  //      field.Append(text);

  //      var endsWidth = true;
  //      var doubleQuoteCount = 0;

  //      for (int fieldIndex = field.Length - 1, fieldSeparatorIndex = fieldSeparator.Length - 1; fieldIndex >= 0; fieldIndex--, fieldSeparatorIndex--)
  //      {
  //        if (fieldSeparatorIndex >= 0 && !comparer.Equals(field[fieldIndex], fieldSeparator[fieldSeparatorIndex]))
  //        {
  //          endsWidth = false;
  //          break;
  //        }
  //        else if (comparer.Equals(field[fieldIndex], '"'))
  //        {
  //          doubleQuoteCount++;
  //        }
  //      }

  //      if (endsWidth && field.Length >= fieldSeparator.Length && doubleQuoteCount % 2 == 0)
  //      {
  //        yield return field.ToString(0, field.Length - fieldSeparator.Length).Unwrap('"', '"').Replace("\"\"", "\"");

  //        field.Clear();
  //      }
  //    }

  //    if (field.Length > 0)
  //    {
  //      yield return field.ToString().Unwrap('"', '"').Replace("\"\"", "\"");
  //    }
  //  }
  //  /// <summary></summary>
  //  public static System.Collections.Generic.IEnumerable<string> ReadLines(System.Collections.Generic.IEnumerable<string> textElements, string lineSeparator = "\r\n", System.Collections.Generic.IEqualityComparer<char>? comparer = null)
  //  {
  //    if (comparer is null) comparer = System.Collections.Generic.EqualityComparer<char>.Default;

  //    var line = new System.Text.StringBuilder();

  //    foreach (var text in textElements)
  //    {
  //      line.Append(text);

  //      var endsWidth = true;
  //      var doubleQuoteCount = 0;

  //      for (int lineIndex = line.Length - 1, lineSeparatorIndex = lineSeparator.Length - 1; lineIndex >= 0; lineIndex--, lineSeparatorIndex--)
  //      {
  //        if (lineSeparatorIndex >= 0 && !comparer.Equals(line[lineIndex], lineSeparator[lineSeparatorIndex]))
  //        {
  //          endsWidth = false;
  //          break;
  //        }
  //        else if (comparer.Equals(line[lineIndex], '"'))
  //        {
  //          doubleQuoteCount++;
  //        }
  //      }

  //      if (endsWidth && line.Length >= lineSeparator.Length && doubleQuoteCount % 2 == 0)
  //      {
  //        yield return line.ToString(0, line.Length - lineSeparator.Length);

  //        line.Clear();
  //      }
  //    }

  //    if (line.Length > 0)
  //    {
  //      yield return line.ToString();
  //    }
  //  }
  //}
}
