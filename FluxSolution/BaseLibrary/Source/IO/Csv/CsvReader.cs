namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns a new sequence of string arrays from the <see cref="System.IO.Stream"/>, utilizing a <see cref="Text.CsvReader"/> with the specified options (defaults if null).</summary>
    public static System.Collections.Generic.IEnumerable<string[]> ReadCsv(this System.IO.Stream source, CsvOptions? options)
    {
      using var reader = new CsvReader(source, options ?? new CsvOptions());

      foreach (var idr in reader)
        yield return idr.GetStrings(string.Empty).ToArray();
    }
  }

  public sealed class CsvReader
    : Data.TabularDataReader
  {
    private readonly System.Text.StringBuilder m_csvValue = new(8192);

    private readonly CsvOptions m_options;

    private readonly System.IO.TextReader m_textReader;

    private CsvTokenType m_tokenType = CsvTokenType.None;

    /// <summary>Creates a new CSV reader on the stream with the specified options.</summary>
    public CsvReader(System.IO.TextReader textReader, CsvOptions options)
    {
      m_options = options ?? new CsvOptions();

      m_textReader = textReader;
    }
    /// <summary>Creates a new CSV reader on the stream with the specified options.</summary>
    public CsvReader(System.IO.Stream stream, CsvOptions options)
      : this(new System.IO.StreamReader(stream, options.Encoding), options)
    { }
    /// <summary>Creates a new CSV reader on the stream with the specified options.</summary>
    public CsvReader(System.IO.Stream stream, CsvOptions options, System.Collections.Generic.IList<string> fieldNames)
      : this(stream, options)
    {
      m_fieldNames.AddRange(fieldNames);
      m_fieldTypes.AddRange(System.Linq.Enumerable.Repeat(typeof(string), FieldNames.Count));
    }

    /// <summary>Returns the current type of token of the reader.</summary>
    public CsvTokenType TokenType
      => m_tokenType;

    /// <summary>Reads a field value ending with either a comma or a carriage return.</summary>
    public string ReadFieldValue()
    {
      m_csvValue.Clear();

      var isQuotedField = false;

      for (var peek = m_textReader.Peek(); peek != -1; peek = m_textReader.Peek())
      {
        if (isQuotedField) // We are inside double quoted field, so really the only 'important' character will be an 'exit field' double quote.
        {
          if (peek == '"') // Read is a double quote.
          {
            m_textReader.Read(); // Discard the current peek.

            if (m_textReader.Peek() != '"') // Was it a single double quote?
              isQuotedField = false; // Yes, so let's exit double quoted field.
            else // No, since the 'second' peek is a double quote, this is, or should be, an escape sequence.
              m_csvValue.Append(char.ConvertFromUtf32(m_textReader.Read())); // Withdraw and store the 'second' double quote (since it was escaped as one).
          }
          else // For a double quoted field, anything else goes here.
            m_csvValue.Append(char.ConvertFromUtf32(m_textReader.Read()));
        }
        else // We are outside of any double quotes, and so various characters are important.
        {
          if (peek == '"') // Peek is a double quote.
          {
            m_textReader.Read();
            isQuotedField = true; // Enter double quoted field.
          }
          else if (peek == m_options.FieldSeparator) // Read is a field separator.
          {
            m_textReader.Read();
            break;
          }
          else if (peek == '\r')
          {
            m_textReader.Read();
            break;
          }
          else if (peek == '\n')
          {
            m_csvValue.Append(char.ConvertFromUtf32(m_textReader.Read())); // Save this single line feed for the token evaluation.
            break;
          }
          else // For a field not double quoted, anything else goes here.
            m_csvValue.Append(char.ConvertFromUtf32(m_textReader.Read()));
        }
      }

      return m_csvValue.ToString();
    }

    public void ClearEndOfLine()
    {
      if (m_textReader.Peek() == 13)
        m_textReader.Read();
      if (m_textReader.Peek() == 10)
        m_textReader.Read();
    }

    public bool IsEndOfLine
      => m_textReader.Peek() is var peek && (peek == 13 || peek == 10);
    public bool IsEndOfStream
      => m_textReader.Peek() == -1;

    public CsvTokenType ReadToNextToken()
    {
      switch (TokenType)
      {
        case CsvTokenType.None:
          m_tokenType = IsEndOfStream ? CsvTokenType.EndStream : CsvTokenType.StartStream;
          break;
        case CsvTokenType.StartLine:
          m_fieldValues.Clear();

          m_tokenType = IsEndOfStream ? CsvTokenType.None : CsvTokenType.StartField;
          break;
        case CsvTokenType.StartField:
          m_fieldValues.Add(ReadFieldValue());
          m_tokenType = CsvTokenType.EndField;
          break;
        case CsvTokenType.StartStream:
          m_tokenType = IsEndOfStream ? CsvTokenType.EndStream : CsvTokenType.StartLine;
          break;
        case CsvTokenType.EndField:
          var lastField = (string)m_fieldValues[^1];

          //if (lastField == "\n")
          //{
          //  m_fieldValues.RemoveAt(m_fieldValues.Count - 1);
          //  m_tokenType = CsvTokenType.EndLine;
          //  break;
          //}
          //else
          if (lastField.EndsWith('\n'))
          {
            m_fieldValues[^1] = lastField[..^1];
            m_tokenType = CsvTokenType.EndLine;
            break;
          }

          m_tokenType = IsEndOfStream ? CsvTokenType.EndLine : CsvTokenType.StartField;
          break;
        case CsvTokenType.EndLine:
          m_tokenType = IsEndOfStream ? CsvTokenType.EndStream : CsvTokenType.StartLine; // Either end of stream or start of a new line.
          break;
        case CsvTokenType.EndStream:
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(TokenType));
      }

      return m_tokenType;
    }

    public CsvTokenType ReadToNextToken2()
    {
      switch (TokenType)
      {
        case CsvTokenType.None:
          if (m_textReader.Peek() != -1)
          {
            m_tokenType = CsvTokenType.StartLine;
          }
          break;
        case CsvTokenType.StartLine:
          m_fieldValues.Clear();

          if (ReadFieldValue() is var startLineValue && startLineValue == "\n") // A single line feed is an end of line.
          {
            m_tokenType = CsvTokenType.EndLine;
          }
          else // Otherwise a 'start' of another field.
          {
            m_fieldValues.Add(startLineValue);

            m_tokenType = CsvTokenType.StartField;
          }
          break;
        case CsvTokenType.StartField:
          m_tokenType = CsvTokenType.EndField; // The content of the field has already been acquired, so it's the end of a field.
          break;
        case CsvTokenType.EndField:
          if (ReadFieldValue() is var endFieldValue && endFieldValue.EndsWith('\n')) // A single line feed is an end of line.
          {
            m_fieldValues.Add(endFieldValue[..^1]);

            m_tokenType = CsvTokenType.EndLine;
          }
          else // Otherwise a 'start' of another field.
          {
            m_fieldValues.Add(endFieldValue);

            m_tokenType = CsvTokenType.StartField;
          }
          break;
        case CsvTokenType.EndLine:
          m_tokenType = m_textReader.Peek() == -1 ? CsvTokenType.None : CsvTokenType.StartLine;
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(TokenType));
      }

      return m_tokenType;
    }

    public bool ReadToToken(CsvTokenType tokenType)
    {
      while (ReadToNextToken() != CsvTokenType.EndStream)
        if (TokenType == tokenType)
          return true;

      return false;
    }

    // Statics
    public static string UnescapeCsv(string source)
      => source?.Unwrap('"', '"').Replace("\"\"", "\"", System.StringComparison.Ordinal) ?? string.Empty;
    //=> source is not null && source.Length >= 2 && source[0] == '"' && source[^1] == '"' ? source[1..^1].Replace("\"\"", "\"", System.StringComparison.Ordinal) : string.Empty;

    // DataReader
    public override bool Read()
    {
      m_fieldValues.Clear();

      if (FieldNames.Count == 0)
      {
        if (!(IsClosed = !ReadToToken(CsvTokenType.EndLine)))
        {
          m_fieldNames.AddRange(FieldValues.Cast<string>());
          m_fieldTypes.AddRange(System.Linq.Enumerable.Repeat(typeof(string), FieldNames.Count));
        }
        else throw new System.Exception(@"Could not load field names.");
      }
      else // Otherwise load FieldValues.
        IsClosed = !ReadToToken(CsvTokenType.EndLine);

      return !IsClosed;
    }

    // Disposable
    protected override void DisposeManaged()
    {
      m_textReader?.Dispose();

      base.DisposeManaged();
    }

    public override string ToString()
      => $"{GetType().Name} {{ {m_options} }}";
  }
}
