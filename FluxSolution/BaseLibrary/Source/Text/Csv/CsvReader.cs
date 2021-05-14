using System.Linq;

namespace Flux.Text.Csv
{
  public enum CsvTokenType
  {
    None,
    StartLine,
    StartField,
    EndField,
    EndLine
  }

  public class CsvReader
    : Data.TabularDataReader
  //: Flux.Disposable, System.Collections.Generic.IEnumerable<string[]>
  {
    private readonly CsvOptions m_options;

    private readonly System.IO.StreamReader m_streamReader;

    /// <summary>Creates a new CSV reader on the stream with the specified options.</summary>
    public CsvReader(System.IO.Stream stream, CsvOptions options)
    {
      m_options = options ?? new CsvOptions();

      m_streamReader = new System.IO.StreamReader(stream, m_options.Encoding);
    }
    /// <summary>Creates a new CSV reader on the stream with the specified options.</summary>
    public CsvReader(System.IO.Stream stream, CsvOptions options, System.Collections.Generic.IList<string> fieldNames)
      : this(stream, options)
    {
      FieldNames.AddRange(fieldNames);
      FieldTypes.AddRange(System.Linq.Enumerable.Repeat(typeof(string), FieldNames.Count));
    }

    private CsvTokenType m_tokenType = CsvTokenType.None;
    /// <summary>Returns the current type of token of the reader.</summary>
    public CsvTokenType TokenType
      => m_tokenType;

    private readonly System.Text.StringBuilder m_csvValue = new System.Text.StringBuilder(8192);

    /// <summary>Reads a field value ending with either a comma or a carriage return.</summary>
    public string ReadFieldValue()
    {
      m_csvValue.Clear();

      var isQuotedField = false;

      for (var peek = m_streamReader.Peek(); peek != -1; peek = m_streamReader.Peek())
      {
        if (!isQuotedField) // We are not inside a double quoted field, and so various characters are important.
        {
          if (peek == '"') // Read is a double quote.
          {
            m_streamReader.Read();

            isQuotedField = true; // Enter double quoted field.
          }
          else if (peek == m_options.FieldSeparator) // Read is a field separator.
          {
            m_streamReader.Read();

            break;
          }
          else if (peek == '\r')
          {
            m_streamReader.Read();

            break;
          }
          else if (peek == '\n')
          {
            m_csvValue.Append(char.ConvertFromUtf32(m_streamReader.Read()));

            break;
          }
          else
            m_csvValue.Append(char.ConvertFromUtf32(m_streamReader.Read()));
        }
        else // We are inside a double quoted field, so really the only 'important' character will be an 'exit field' double quote.
        {
          if (peek == '"') // Read is a double quote.
          {
            m_streamReader.Read();

            if (m_streamReader.Peek() != '"') // Single double quote?
            {
              isQuotedField = false; // Exit double quoted field.
            }
            else // Peek is a double quote, so this is, or should be, an escape sequence.
            {
              m_csvValue.Append(char.ConvertFromUtf32(m_streamReader.Read())); // Keep the other double quote (since it was escaped as one).
            }
          }
          else
            m_csvValue.Append(char.ConvertFromUtf32(m_streamReader.Read())); // Anything else goes, for a double quoted field.
        }
      }

      return m_csvValue.ToString();
    }

    public CsvTokenType ReadToNextToken()
    {
      switch (TokenType)
      {
        case CsvTokenType.None:
          if (m_streamReader.Peek() != -1)
          {
            m_tokenType = CsvTokenType.StartLine;
          }
          break;
        case CsvTokenType.StartLine:
          FieldValues.Clear();

          if (ReadFieldValue() is var startLineValue && startLineValue == "\n")
          {
            m_tokenType = CsvTokenType.EndLine;
          }
          else
          {
            FieldValues.Add(startLineValue);

            m_tokenType = CsvTokenType.StartField;
          }
          break;
        case CsvTokenType.StartField:
          m_tokenType = CsvTokenType.EndField;
          break;
        case CsvTokenType.EndField:
          if (ReadFieldValue() is var endFieldValue && endFieldValue == "\n")
          {
            m_tokenType = CsvTokenType.EndLine;
          }
          else
          {
            FieldValues.Add(endFieldValue);

            m_tokenType = CsvTokenType.StartField;
          }
          break;
        case CsvTokenType.EndLine:
          m_tokenType = m_streamReader.Peek() == -1 ? CsvTokenType.None : CsvTokenType.StartLine;
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(TokenType));
      }

      return m_tokenType;
    }

    public bool ReadToToken(CsvTokenType tokenType)
    {
      while (ReadToNextToken() != CsvTokenType.None)
        if (TokenType == tokenType)
          return true;

      return false;
    }

    // Statics
    public static string UnescapeCsv(string source)
      => source?.Unwrap('"', '"').Replace("\"\"", "\"", System.StringComparison.Ordinal) ?? string.Empty;

    // DataReader
    public override bool Read()
    {
      FieldValues.Clear();

      if (FieldNames.Count == 0)
      {
        if (!(IsClosed = !ReadToToken(CsvTokenType.EndLine)))
        {
          FieldNames.AddRange(FieldValues.Cast<string>());
          FieldTypes.AddRange(System.Linq.Enumerable.Repeat(typeof(string), FieldNames.Count));
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
      m_streamReader?.Dispose();

      base.DisposeManaged();
    }

    //// IEnumerable<string[]>
    //public System.Collections.Generic.IEnumerator<string[]> GetEnumerator()
    //{
    //	return ReadArrays().Cast<string[]>().GetEnumerator();

    //	System.Collections.Generic.IEnumerable<string[]> ReadArrays()
    //	{
    //		while (ReadToToken(CsvTokenType.EndLine))
    //			yield return CsvValues.ToArray();
    //	}
    //}
    //System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    //	=> GetEnumerator();
  }
}
