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
          if (!m_inDoubleQuotes)
          {
            if (read == '"')
            {
              m_inDoubleQuotes = true; // Enter double quoted field.
            }
            else if (read == m_options.FieldSeparator) // Between fields?
            {
              yield return sb.ToString();

              sb.Clear();
            }
            else if (read == '\r') // End of line by CR?
            {
              if (peek == '\n')  // End of line contains LF?
              {
                m_streamReader.Read();  // Discard LF.
              }

              yield return sb.ToString();

              yield break;
            }
            else if (read == '\n') // End of line by LF?
            {
              yield return sb.ToString();

              yield break;
            }
            else
            {
              sb.Append(char.ConvertFromUtf32(read));
            }
          }
          else if (m_inDoubleQuotes)
          {
            if (read == '"')
            {
              if (peek != '"')
              {
                m_inDoubleQuotes = false; // Exit double quoted field.
              }
              else // Peek is a DoubleQuote, so this is an escape sequence.
              {
                m_streamReader.Read(); // Discard one of the double quotes.

                sb.Append(char.ConvertFromUtf32(read));
              }
            }
            else
            {
              sb.Append(char.ConvertFromUtf32(read));
            }
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
