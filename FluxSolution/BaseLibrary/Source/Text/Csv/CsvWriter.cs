namespace Flux.Text
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

    private bool m_inField;
    private bool m_inLine;

    private readonly System.Text.StringBuilder m_fieldValue = new System.Text.StringBuilder();

    private void FlushFieldValue()
    {
      FlushFieldValue(m_fieldValue.ToString());

      m_fieldValue.Clear();
    }
    private void FlushFieldValue(string value)
    {
      if (FieldIndex > 0)
      {
        m_streamWriter.Write(m_options.FieldSeparator);
      }

      if (value.IndexOfAny(m_escapeCharacters) > -1)
      {
        m_streamWriter.Write('"');
        m_streamWriter.Write(value.Contains("\"", System.StringComparison.Ordinal) ? value.Replace("\"", "\"\"", System.StringComparison.Ordinal) : value);
        m_streamWriter.Write('"');
      }
      else
      {
        m_streamWriter.Write(value);
      }
    }

    public int LineNumber { get; private set; } = -1;

    public void WriteStartField()
    {
      if (m_inLine && !m_inField)
      {
        m_inField = true;

        FieldIndex++;

        if (LineNumber == 0) FieldCount = FieldIndex + 1;
      }
      else throw new System.InvalidOperationException(@"Invalid context.");
    }
    public void WriteChars(char[] buffer, int index, int count)
    {
      if (m_inField)
      {
        m_fieldValue.Append(buffer, index, count);
      }
      else throw new System.InvalidOperationException(@"Invalid field context.");
    }
    public void WriteFieldString(string value)
    {
      if (!m_inField && m_inLine)
      {
        FieldIndex++;

        if (LineNumber == 0) FieldCount = FieldIndex + 1;

        FlushFieldValue(value ?? string.Empty);
      }
      else throw new System.InvalidOperationException(@"Invalid context.");
    }
    public void WriteString(string value)
    {
      if (m_inField)
      {
        m_fieldValue.Append(value);
      }
      else throw new System.InvalidOperationException(@"Invalid field context.");
    }
    public void WriteEndField()
    {
      if (m_inField)
      {
        FlushFieldValue();

        m_inField = false;
      }
      else throw new System.InvalidOperationException(@"Invalid field context.");

    }

    public void WriteStartLine()
    {
      if (!m_inField && !m_inLine)
      {
        m_inLine = true;

        LineNumber++;
      }
      else throw new System.InvalidOperationException(@"Invalid context (not in field).");
    }
    public void WriteEndLine()
    {
      if (!m_inField && m_inLine)
      {
        m_streamWriter.WriteLine();

        //if (LineNumber== 0) FieldCount = FieldIndex + 1;
        //else if (FieldIndex != FieldCount - 1) throw new System.InvalidOperationException(@"Inconsistent field count.");

        FieldIndex = -1;

        m_inLine = false;
      }
      else throw new System.InvalidOperationException(@"Invalid context (not in field).");
    }

    public void WriteArray(string[] values)
    {
      WriteStartLine();
      foreach (var value in values ?? throw new System.ArgumentNullException(nameof(values)))
      {
        WriteFieldString(value);

        //WriteStartField();
        //WriteString(value);
        //WriteEndField();
      }
      WriteEndLine();
    }
    public void WriteArrays(System.Collections.Generic.IEnumerable<string[]> arrays)
    {
      foreach (var array in arrays ?? throw new System.ArgumentNullException(nameof(arrays)))
      {
        WriteArray(array);
      }
    }

    protected override void DisposeManaged()
    {
      m_streamWriter.Flush();
      m_streamWriter.Dispose();
    }
  }
}
