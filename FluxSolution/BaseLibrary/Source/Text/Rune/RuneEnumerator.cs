namespace Flux
{
  public static partial class Fx
  {
    public static System.Collections.Generic.IEnumerable<System.Text.Rune> EnumerateRunes(this System.IO.Stream source, System.Text.Encoding encoding)
      => new Text.RuneEnumerator(source, encoding);
    public static System.Collections.Generic.IEnumerable<System.Text.Rune> EnumerateRunes(this System.IO.TextReader source)
      => new Text.RuneEnumerator(source);
  }

  namespace Text
  {
    /// <summary>Creates an enumerator of <see cref="System.Text.Rune"/> from a stream of <see cref="System.Char"/>.</summary>
    public sealed class RuneEnumerator
      : Disposable, System.Collections.Generic.IEnumerable<System.Text.Rune>
    {
      internal const int DefaultBufferSize = 4096;
      internal const int DefaultMinLength = 16;

      internal readonly System.IO.TextReader m_textReader;
      internal readonly int m_bufferSize; // The size of the total buffer.
      internal readonly int m_minLength; // The minimum length of buffer content.

      public RuneEnumerator(System.IO.TextReader textReader, int bufferSize = DefaultBufferSize, int minLength = DefaultMinLength)
      {
        m_textReader = textReader ?? throw new System.ArgumentNullException(nameof(textReader));
        m_bufferSize = bufferSize >= 128 ? bufferSize : throw new System.ArgumentOutOfRangeException(nameof(bufferSize));
        m_minLength = minLength >= 8 ? minLength : throw new System.ArgumentOutOfRangeException(nameof(minLength));
      }
      public RuneEnumerator(System.IO.Stream stream, System.Text.Encoding encoding, int bufferSize = DefaultBufferSize, int minLength = DefaultMinLength)
        : this(new System.IO.StreamReader(stream, encoding), bufferSize, minLength) { }
      public RuneEnumerator(string text, int bufferSize = DefaultBufferSize, int minLength = DefaultMinLength)
        : this(new System.IO.StringReader(text), bufferSize, minLength) { }

      public System.Collections.Generic.IEnumerator<System.Text.Rune> GetEnumerator() => new RuneIterator(this);
      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

      protected override void DisposeManaged() => m_textReader.Dispose();

      private sealed class RuneIterator
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

        public System.Text.Rune Current => m_current;
        object System.Collections.IEnumerator.Current => m_current;

        public bool MoveNext()
        {
          if (m_bufferCount - m_bufferIndex <= m_enumerator.m_minLength) // If one or less available chars, then 'top off' the buffer.
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

            return true;
          }

          return false;
        }

        public void Reset() => throw new System.NotImplementedException();

        public void Dispose() { }
      }
    }
  }
}
