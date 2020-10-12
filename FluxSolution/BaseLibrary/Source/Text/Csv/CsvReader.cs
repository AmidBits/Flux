using System.Linq;

namespace Flux.Text
{
  public class CsvReader
    : Flux.Disposable, System.Collections.Generic.IEnumerable<string[]>
  {
    private readonly CsvOptions m_options;

    private readonly System.IO.StreamReader m_streamReader;

    public CsvReader(System.IO.Stream stream, CsvOptions options)
    {
      m_options = options ?? new CsvOptions();

      m_streamReader = new System.IO.StreamReader(stream, m_options.Encoding);
    }

    private bool m_inDoubleQuotes;

    public string[] ReadArray()
    {
      var sb = new System.Text.StringBuilder(8192);

      return m_streamReader.EndOfStream ? System.Array.Empty<string>() : GetFields().ToArray();

      System.Collections.Generic.IEnumerable<string> GetFields()
      {
        for (int read = m_streamReader.Read(), peek = m_streamReader.Peek(); read != -1; read = m_streamReader.Read(), peek = m_streamReader.Peek())
        {
          if (!m_inDoubleQuotes) // We are not inside a double quoted field, and so various characters are important.
          {
            if (read == '"') // Read is a double quote.
            {
              m_inDoubleQuotes = true; // Enter double quoted field.
            }
            else if (read == m_options.FieldSeparator) // Read is a field separator.
            {
              yield return sb.ToString();

              sb.Clear();
            }
            else if (read == '\r' || read == '\n') // If read is either a CR or an LF, we have reached the end of line (EOL).
            {
              if (read == '\r' && peek == '\n') // If read was a CR and peek is an LF (standard for CSV files)...
                m_streamReader.Read();  // We remove the peek LF from the stream.

              yield return sb.ToString(); // Return the accumulated characters as a field value.

              yield break; // Since this was the EOL we can break to denote a set of fields.
            }
            else
              sb.Append(char.ConvertFromUtf32(read));
          }
          else // We are inside a double quoted field, so really the only 'important' character will be an 'exit field' double quote.
          {
            if (read == '"') // Read is a double quote.
            {
              if (peek != '"')
              {
                m_inDoubleQuotes = false; // Exit double quoted field.
              }
              else // Peek is a double quote, so this is, or should be, an escape sequence.
              {
                m_streamReader.Read(); // Remove the peek double quote from the stream.

                sb.Append(char.ConvertFromUtf32(read)); // Keep the other double quote (since it was escaped as one).
              }
            }
            else
              sb.Append(char.ConvertFromUtf32(read)); // Anything else goes, for a double quoted field.
          }
        }
      }
    }

    public System.Collections.Generic.IEnumerable<string[]> ReadArrays()
    {
      while (!m_streamReader.EndOfStream)
      {
        yield return ReadArray();
      }
    }

    protected override void DisposeManaged()
      => m_streamReader?.Dispose();

    // IEnumerable<string[]>
    public System.Collections.Generic.IEnumerator<string[]> GetEnumerator()
      => ReadArrays().GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
  }
}
