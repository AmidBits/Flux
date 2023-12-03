namespace Flux
{
  public static partial class Fx
  {
    public static System.Collections.Generic.IEnumerable<Text.TextElement> EnumerateTextElements(this System.IO.Stream source, System.Text.Encoding encoding)
      => new Text.TextElementEnumerator(source, encoding);
    public static System.Collections.Generic.IEnumerable<Text.TextElement> EnumerateTextElements(this System.IO.TextReader source)
      => new Text.TextElementEnumerator(source);
  }

  namespace Text
  {
    /// <summary>Creates an enumerator of <see cref="Text.TextElement"/> from a stream of <see cref="System.Char"/>.</summary>
    /// <remarks>Can be used for larger text segments by utilizing a (text) stream.</remarks>
    public sealed class TextElementEnumerator
    : Disposable, System.Collections.Generic.IEnumerable<TextElement>
    {
      const int DefaultBufferSize = 4096;
      const int DefaultMinLength = 16;

      internal readonly System.IO.TextReader m_textReader;
      internal readonly int m_bufferSize; // The size of the total buffer.
      internal readonly int m_minLength; // The minimum length of buffer content.

      public TextElementEnumerator(System.IO.TextReader textReader, int bufferSize = DefaultBufferSize, int minLength = DefaultMinLength)
      {
        m_textReader = textReader ?? throw new System.ArgumentNullException(nameof(textReader));
        m_bufferSize = bufferSize >= 128 ? bufferSize : throw new System.ArgumentOutOfRangeException(nameof(bufferSize));
        m_minLength = minLength >= 8 ? minLength : throw new System.ArgumentOutOfRangeException(nameof(minLength));
      }
      public TextElementEnumerator(System.IO.Stream stream, System.Text.Encoding encoding, int bufferSize = DefaultBufferSize, int minLength = DefaultMinLength)
        : this(new System.IO.StreamReader(stream, encoding), bufferSize, minLength) { }

      public System.Collections.Generic.IEnumerator<TextElement> GetEnumerator() => new TextElementIterator(this);
      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

      protected override void DisposeManaged() => m_textReader.Dispose();

      private sealed class TextElementIterator
        : System.Collections.Generic.IEnumerator<TextElement>
      {
        private readonly TextElementEnumerator m_enumerator;

        private readonly char[] m_buffer;
        private int m_bufferIndex;
        private int m_bufferCount;

        private TextElement m_current;

        private readonly System.Globalization.StringInfo m_stringInfo;

        private int m_textElementIndex;
        private int m_textElementCount;

        public TextElementIterator(TextElementEnumerator enumerator)
        {
          m_enumerator = enumerator;

          m_buffer = new char[enumerator.m_bufferSize];
          m_bufferIndex = 0;
          m_bufferCount = 0;

          m_stringInfo = new System.Globalization.StringInfo();

          m_textElementCount = 0;
          m_textElementIndex = 0;

          m_current = default!;
        }

        public TextElement Current
          => m_current;
        object System.Collections.IEnumerator.Current
          => m_current!;

        public bool MoveNext()
        {
          if (m_textElementCount - m_textElementIndex <= m_enumerator.m_minLength) // If available text elements is less or equal to low buffer, then 'top off' the buffer and reset the content of stringInfo.
          {
            m_bufferCount -= m_bufferIndex; // Adjust to any remaining char count.

            if (m_bufferCount > 0) // If there are any chars in the buffer..
              System.Array.Copy(m_buffer, m_bufferIndex, m_buffer, 0, m_bufferCount); // ..copy them to the beginning of the buffer.

            m_bufferIndex = 0; // Reset the buffer index.
            m_bufferCount += m_enumerator.m_textReader.Read(m_buffer, m_bufferCount, m_buffer.Length - m_bufferCount); // Read any remaining chars into the buffer, and add to the actual buffer count.

            m_stringInfo.String = new string(m_buffer, m_bufferIndex, m_bufferCount - m_bufferIndex); // Reset the stringInfo with all chars in the char buffer.

            m_textElementIndex = 0; // Reset the text element index.
            m_textElementCount = m_stringInfo.LengthInTextElements; // Count up the number of text elements.
          }

          if (m_textElementIndex < m_textElementCount) // If text elements are available.
          {
            var textElement = m_stringInfo.SubstringByTextElements(m_textElementIndex++, 1); // Get the next text element, and advance the text element index.

            m_bufferIndex += textElement.Length; // Adjust the buffer index by the number of characters in the text element.

            m_current = new TextElement(textElement); // Set current to the text element.

            return true;
          }

          return false;
        }

        public void Reset()
          => throw new System.NotImplementedException();

        public void Dispose()
        { }
      }
    }
  }
}
