using System;

namespace Flux
{
  public static partial class RuneEm
  {
    public static System.Collections.Generic.IEnumerable<System.Text.Rune> ReadRunes(this System.IO.Stream source, System.Text.Encoding encoding)
      => new Text.StreamReaderRuneEnumerator(source, encoding);
    public static System.Collections.Generic.IEnumerable<System.Text.Rune> ReadRunes(this System.IO.StreamReader source)
      => new Text.StreamReaderRuneEnumerator(source);
  }

  namespace Text
  {
    public class StreamReaderRuneEnumerator
    : Disposable, System.Collections.Generic.IEnumerable<System.Text.Rune>
    {
      internal readonly System.IO.StreamReader m_streamReader;
      internal readonly int m_bufferSize;

      public StreamReaderRuneEnumerator(System.IO.Stream stream, System.Text.Encoding encoding, int bufferSize = 8192)
      {
        m_streamReader = new System.IO.StreamReader(stream ?? throw new System.ArgumentNullException(nameof(stream)), encoding);
        m_bufferSize = bufferSize;
      }
      public StreamReaderRuneEnumerator(System.IO.StreamReader streamReader, int bufferSize = 8192)
      {
        m_streamReader = streamReader ?? throw new System.ArgumentNullException(nameof(streamReader));
        m_bufferSize = bufferSize;
      }

      public System.Collections.Generic.IEnumerator<System.Text.Rune> GetEnumerator()
        => new StreamReaderRuneIterator(this);
      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        => GetEnumerator();

      protected override void DisposeManaged()
        => m_streamReader.Dispose();

      private class StreamReaderRuneIterator
        : System.Collections.Generic.IEnumerator<System.Text.Rune>
      {
        private readonly StreamReaderRuneEnumerator m_enumerator;

        private readonly char[] charArray;
        private int charIndex;
        private int charCount;

        private System.Text.Rune m_current;

        public StreamReaderRuneIterator(StreamReaderRuneEnumerator enumerator)
        {
          m_enumerator = enumerator;

          charArray = new char[enumerator.m_bufferSize];
          charIndex = 0;
          charCount = 0;

          m_current = default!;
        }

        public System.Text.Rune Current
          => m_current;
        object System.Collections.IEnumerator.Current
          => m_current!;

        public bool MoveNext()
        {
          var difference = charCount - charIndex;

          if (difference <= 4)
          {
            if (difference > 0)
              System.Array.Copy(charArray, charIndex, charArray, 0, difference);

            charIndex = 0;
            charCount = difference;
          }

          if (charIndex == 0 && charCount < charArray.Length)
          {
            charCount += m_enumerator.m_streamReader.Read(charArray, charCount, charArray.Length - charCount);
          }

          if (System.Text.Rune.DecodeFromUtf16(charArray.AsSpan(charIndex, charCount - charIndex), out var rune, out var count) is var or && or == System.Buffers.OperationStatus.Done)
          {
            charIndex += count;

            m_current = rune;

            return true;
          }

          return false;
        }

        public void Reset()
        {
          m_enumerator.m_streamReader.BaseStream.Position = 0;

          charIndex = 0;
          charCount = 0;

          m_current = default!;
        }

        public void Dispose() { }
      }
    }
  }
}
