namespace Flux.Text
{
  public class GraphemeEnumerator
    : Disposable, System.Collections.Generic.IEnumerable<GraphemeCluster>
  {
    internal readonly System.IO.TextReader m_textReader;
    internal readonly int m_bufferSize;

    public GraphemeEnumerator(System.IO.TextReader textReader, int bufferSize = 8192)
    {
      if (textReader is null) throw new System.ArgumentNullException(nameof(textReader));
      if (bufferSize < 128) throw new System.ArgumentOutOfRangeException(nameof(bufferSize));

      m_textReader = textReader;
      m_bufferSize = bufferSize;
    }
    public GraphemeEnumerator(System.IO.Stream stream, System.Text.Encoding encoding, int bufferSize = 8192)
      : this(new System.IO.StreamReader(stream, encoding), bufferSize)
    { }

    public System.Collections.Generic.IEnumerator<GraphemeCluster> GetEnumerator()
      => new GraphemeIterator(this);
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();

    protected override void DisposeManaged()
      => m_textReader.Dispose();

    private class GraphemeIterator
      : System.Collections.Generic.IEnumerator<GraphemeCluster>
    {
      private readonly GraphemeEnumerator m_enumerator;

      private readonly char[] m_buffer;
      private int m_bufferIndex;
      private int m_bufferCount;

      private readonly System.Globalization.StringInfo m_stringInfo;

      private int m_textElementIndex;
      private int m_textElementCount;

      private GraphemeCluster m_current;

      public GraphemeIterator(GraphemeEnumerator enumerator)
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

      public GraphemeCluster Current
        => m_current;
      object System.Collections.IEnumerator.Current
        => m_current!;

      private int m_overallIndex = 0;

      public bool MoveNext()
      {
        if (m_textElementCount - m_textElementIndex <= 8) // If one or less available text elements, then 'top off' the buffer and reset the content of stringInfo.
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

          m_current = new GraphemeCluster(textElement, m_overallIndex); // Set current to the text element.

          m_overallIndex += textElement.Length;

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