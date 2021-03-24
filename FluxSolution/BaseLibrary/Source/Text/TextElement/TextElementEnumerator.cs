namespace Flux.Text
{
  public class TextElementEnumerator
    : Disposable, System.Collections.Generic.IEnumerable<string>
  {
    internal readonly System.IO.TextReader m_textReader;
    internal readonly int m_bufferSize;

    public TextElementEnumerator(System.IO.Stream stream, System.Text.Encoding encoding, int bufferSize = 8192)
    {
      m_textReader = new System.IO.StreamReader(stream ?? throw new System.ArgumentNullException(nameof(stream)), encoding);
      m_bufferSize = bufferSize;
    }
    public TextElementEnumerator(System.IO.TextReader textReader, int bufferSize = 8192)
    {
      m_textReader = textReader ?? throw new System.ArgumentNullException(nameof(textReader));
      m_bufferSize = bufferSize;
    }

    public System.Collections.Generic.IEnumerator<string> GetEnumerator()
      => new TextElementIterator(this);
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();

    protected override void DisposeManaged()
      => m_textReader.Dispose();

    private class TextElementIterator
      : System.Collections.Generic.IEnumerator<string>
    {
      private readonly TextElementEnumerator m_enumerator;

      private readonly char[] m_array;
      private int m_index;
      private int m_count;

      private string m_current;

      public TextElementIterator(TextElementEnumerator enumerator)
      {
        m_enumerator = enumerator;

        m_array = new char[enumerator.m_bufferSize];
        m_index = 0;
        m_count = 0;

        m_current = default!;
      }

      public string Current
        => m_current;
      object System.Collections.IEnumerator.Current
        => m_current!;

      private readonly System.Globalization.StringInfo m_stringInfo = new System.Globalization.StringInfo();

      public bool MoveNext()
      {
        var difference = m_count - m_index;

        if (difference <= 4)
        {
          if (difference > 0)
            System.Array.Copy(m_array, m_index, m_array, 0, difference);

          m_index = 0;
          m_count = difference;
        }

        if (m_index == 0 && m_count < m_array.Length)
        {
          m_count += m_enumerator.m_textReader.Read(m_array, m_count, m_array.Length - m_count);
        }

        m_stringInfo.String = new string(m_array, m_index, m_count - m_index);

        if (m_stringInfo.String.Length > 0)
        {
          var grapheme = m_stringInfo.SubstringByTextElements(0, 1);

          if (!(grapheme is null) && grapheme.Length > 0)
          {
            m_index += grapheme.Length;

            m_current = grapheme;

            return true;
          }
        }
        //if (System.Text.Rune.DecodeFromUtf16(m_charArray.AsSpan(m_charIndex, m_charCount - m_charIndex), out var rune, out var count) is var or && or == System.Buffers.OperationStatus.Done)
        //{
        //  m_charIndex += count;

        //  m_current = rune;

        //  return true;
        //}

        return false;
      }

      public void Reset()
        => throw new System.NotImplementedException();
      //{
      //  m_enumerator.m_streamReader.BaseStream.Position = 0;

      //  m_charIndex = 0;
      //  m_charCount = 0;

      //  m_current = default!;
      //}

      public void Dispose() { }
    }
  }
}
