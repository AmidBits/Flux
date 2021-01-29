using System.Linq;

namespace Flux.Text.Csv
{
  public class CsvWriter
    : Flux.Disposable
  {
    private readonly char[] m_escapeCharacters;

    private readonly CsvOptions m_options;

    private readonly System.IO.StreamWriter m_streamWriter;

    public CsvWriter(System.IO.Stream stream, CsvOptions options)
    {
      m_options = options ?? new CsvOptions();

      m_escapeCharacters = new char[] { '"', m_options.FieldSeparator, '\r', '\n' };

      m_streamWriter = new System.IO.StreamWriter(stream, m_options.Encoding);
    }

    public int FieldCount { get; private set; } = -1;
    public int FieldIndex { get; private set; } = -1;

    public int LineIndex { get; private set; } = -1;

    private readonly System.Text.StringBuilder m_fieldValue = new System.Text.StringBuilder();

    private bool m_inField;
    private bool m_inLine;

    public void Flush()
    {
      if (m_inField)
        WriteEndField();
      if (m_inLine)
        WriteEndLine();

      m_streamWriter.Flush();
    }

    public void WriteStartLine()
    {
      if (m_inLine) throw new System.InvalidOperationException(@"Invalid context (already in line).");

      m_inLine = true;

      LineIndex++;
    }
    public void WriteEndLine()
    {
      if (m_inField) throw new System.InvalidOperationException(@"Invalid context (in field).");
      if (!m_inLine) throw new System.InvalidOperationException(@"Invalid context (not in line).");

      m_streamWriter.Write(System.Environment.NewLine);

      if (LineIndex > 0 && FieldIndex != FieldCount - 1) throw new System.InvalidOperationException(@"Inconsistent field count.");

      FieldIndex = -1;

      m_inLine = false;
    }

    public void WriteStartField()
    {
      if (!m_inLine) throw new System.InvalidOperationException(@"Invalid context (not in line).");
      if (m_inField) throw new System.InvalidOperationException(@"Invalid context (already in field).");

      m_inField = true;

      FieldIndex++;

      if (LineIndex == 0)
        FieldCount = FieldIndex + 1;
    }
    public void WriteEndField()
    {
      if (!m_inField) throw new System.InvalidOperationException(@"Invalid field context (not in field).");

      if (FieldIndex > 0)
        m_streamWriter.Write(m_options.FieldSeparator);

      var value = m_fieldValue.ToString();

      m_fieldValue.Clear();

      if (m_options.AlwaysEnquote || value.IndexOfAny(m_escapeCharacters) > -1)
      {
        m_streamWriter.Write('"');
        m_streamWriter.Write(value.Contains("\"", System.StringComparison.Ordinal) ? value.Replace("\"", "\"\"", System.StringComparison.Ordinal) : value);
        m_streamWriter.Write('"');
      }
      else
        m_streamWriter.Write(value);

      m_inField = false;
    }

    public void WriteString(string value)
    {
      if (!m_inField) throw new System.InvalidOperationException(@"Invalid field context (not in field).");

      m_fieldValue.Append(value);
    }

    public void WriteFieldString(string value)
    {
      WriteStartField();
      WriteString(value);
      WriteEndField();
    }

    public void WriteArray(System.Collections.Generic.IEnumerable<string> values)
    {
      if (values is null) throw new System.ArgumentNullException(nameof(values));

      WriteStartLine();
      foreach (var value in values)
        WriteFieldString(value);
      WriteEndLine();
    }
    public void WriteArray(params string[] values)
      => WriteArray(values.AsEnumerable());

    protected override void DisposeManaged()
      => Flush();
  }
}
