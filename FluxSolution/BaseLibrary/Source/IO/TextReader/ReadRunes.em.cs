using System;

namespace Flux
{
  public static partial class Xtensions
  {
    public static System.Collections.Generic.IEnumerable<System.Text.Rune> ReadRunes(this System.IO.StreamReader source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      using var sri = new StreamRuneIterator(source);

      while (sri.MoveNext())
        yield return sri.Current;
    }

    private class StreamRuneIterator
      : System.Collections.Generic.IEnumerator<System.Text.Rune>
    {
      private System.Text.Rune m_current;
      private readonly System.IO.StreamReader m_source;

      private char[] charArray;
      private int charIndex;
      private int charCount;

      public StreamRuneIterator(System.IO.StreamReader source, int bufferSize = 8192)
      {
        m_source = source;
        m_current = default!;

        charArray = new char[bufferSize];
        charIndex = 0;
        charCount = 0;
      }

      public System.Text.Rune Current
        => m_current;
      object System.Collections.IEnumerator.Current
        => m_current!;

      public void Dispose()
        => m_source.Dispose();

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
          charCount += m_source.Read(charArray, charCount, charArray.Length - charCount);
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
        => throw new System.InvalidOperationException();
    }
  }
}
