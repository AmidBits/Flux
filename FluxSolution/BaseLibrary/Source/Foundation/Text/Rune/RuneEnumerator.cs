namespace Flux.Text
{
  public class RuneEnumerator
    : Disposable, System.Collections.Generic.IEnumerable<System.Text.Rune>
  {
    internal readonly System.IO.TextReader m_textReader;
    internal readonly int m_bufferSize;

    public RuneEnumerator(System.IO.TextReader textReader, int bufferSize = 8192)
    {
      if (textReader is null) throw new System.ArgumentNullException(nameof(textReader));
      if (bufferSize < 8) throw new System.ArgumentOutOfRangeException(nameof(bufferSize));

      m_textReader = textReader;
      m_bufferSize = bufferSize;
    }
    public RuneEnumerator(System.IO.Stream stream, System.Text.Encoding encoding, int bufferSize = 8192)
     : this(new System.IO.StreamReader(stream, encoding), bufferSize)
    { }

    public System.Collections.Generic.IEnumerator<System.Text.Rune> GetEnumerator()
      => new RuneIterator(this);
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();

    protected override void DisposeManaged()
      => m_textReader.Dispose();

    private class RuneIterator
      : System.Collections.Generic.IEnumerator<System.Text.Rune>
    {
      private readonly RuneEnumerator m_enumerator;

      private readonly char[] m_buffer;
      private int m_bufferIndex;
      private int m_bufferCount;

      private System.Text.Rune m_current;

      public RuneIterator(RuneEnumerator enumerator)
      {
        m_enumerator = enumerator;

        m_buffer = new char[enumerator.m_bufferSize];
        m_bufferIndex = 0;
        m_bufferCount = 0;

        m_current = default!;
      }

      private int m_overallPosition = 0;
      public int OverallPosition
        => m_overallPosition;

      public System.Text.Rune Current
        => m_current;
      object System.Collections.IEnumerator.Current
        => m_current!;

      public bool MoveNext()
      {
        if (m_bufferCount - m_bufferIndex <= 1) // If one or less available chars, then 'top off' the buffer.
        {
          m_bufferCount -= m_bufferIndex; // Adjust to any remaining char count.

          if (m_bufferCount > 0) // If there are any chars in the buffer..
            System.Array.Copy(m_buffer, m_bufferIndex, m_buffer, 0, m_bufferCount); // ..copy them to the beginning of the buffer.

          m_bufferIndex = 0; // Reset the buffer index.
          m_bufferCount += m_enumerator.m_textReader.Read(m_buffer, m_bufferCount, m_buffer.Length - m_bufferCount); // Read any remaining chars into the buffer, and add to the actual buffer count.
        }

        // The read only span could be handled better, need to investigate.
        if (System.Text.Rune.DecodeFromUtf16(new System.ReadOnlySpan<char>(m_buffer, m_bufferIndex, m_bufferCount - m_bufferIndex), out var rune, out var charCount) is var os && os == System.Buffers.OperationStatus.Done) // Read the next rune.
        {
          m_bufferIndex += charCount; // Adjust the buffer index by the number of chars in the rune.

          m_current = rune; // Set current to the rune.

          m_overallPosition += charCount;

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
