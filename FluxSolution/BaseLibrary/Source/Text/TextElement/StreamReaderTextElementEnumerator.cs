namespace Flux
{
  public static partial class Xtensions
  {
    public static System.Collections.Generic.IEnumerable<string> ReadTextElements(this System.IO.Stream source, System.Text.Encoding encoding)
      => new Text.StreamReaderTextElementEnumerator(source, encoding);
    public static System.Collections.Generic.IEnumerable<string> ReadTextElements(this System.IO.StreamReader source)
      => new Text.StreamReaderTextElementEnumerator(source);
  }

  namespace Text
  {
    public class StreamReaderTextElementEnumerator
    : Disposable, System.Collections.Generic.IEnumerable<string>
    {
      internal readonly System.IO.StreamReader m_streamReader;
      internal readonly int m_bufferSize;

      public StreamReaderTextElementEnumerator(System.IO.Stream stream, System.Text.Encoding encoding, int bufferSize = 8192)
      {
        m_streamReader = new System.IO.StreamReader(stream ?? throw new System.ArgumentNullException(nameof(stream)), encoding);
        m_bufferSize = bufferSize;
      }
      public StreamReaderTextElementEnumerator(System.IO.StreamReader streamReader, int bufferSize = 8192)
      {
        m_streamReader = streamReader ?? throw new System.ArgumentNullException(nameof(streamReader));
        m_bufferSize = bufferSize;
      }

      public System.Collections.Generic.IEnumerator<string> GetEnumerator()
        => new StreamReaderTextElementIterator(this);
      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        => GetEnumerator();

      protected override void DisposeManaged()
        => m_streamReader.Dispose();

      private class StreamReaderTextElementIterator
        : System.Collections.Generic.IEnumerator<string>
      {
        private readonly StreamReaderTextElementEnumerator m_enumerator;

        private readonly char[] m_charArray;
        private int m_charIndex;
        private int m_charCount;

        private string m_current;

        public StreamReaderTextElementIterator(StreamReaderTextElementEnumerator enumerator)
        {
          m_enumerator = enumerator;

          m_charArray = new char[enumerator.m_bufferSize];
          m_charIndex = 0;
          m_charCount = 0;

          m_current = default!;
        }

        public string Current
          => m_current;
        object System.Collections.IEnumerator.Current
          => m_current!;

        System.Globalization.StringInfo m_stringInfo = new System.Globalization.StringInfo();

        public bool MoveNext()
        {
          var difference = m_charCount - m_charIndex;

          if (difference <= 4)
          {
            if (difference > 0)
              System.Array.Copy(m_charArray, m_charIndex, m_charArray, 0, difference);

            m_charIndex = 0;
            m_charCount = difference;
          }

          if (m_charIndex == 0 && m_charCount < m_charArray.Length)
          {
            m_charCount += m_enumerator.m_streamReader.Read(m_charArray, m_charCount, m_charArray.Length - m_charCount);
          }

          m_stringInfo.String = new string(m_charArray, m_charIndex, m_charCount - m_charIndex);

          if (m_stringInfo.String.Length > 0)
          {
            var grapheme = m_stringInfo.SubstringByTextElements(0, 1);

            if (!(grapheme is null) && grapheme.Length > 0)
            {
              m_charIndex += grapheme.Length;

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
        {
          m_enumerator.m_streamReader.BaseStream.Position = 0;

          m_charIndex = 0;
          m_charCount = 0;

          m_current = default!;
        }

        public void Dispose() { }
      }
    }
  }
}
