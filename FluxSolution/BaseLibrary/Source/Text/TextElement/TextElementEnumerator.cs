namespace Flux.Text
{
  public static partial class ExtensionMethods
  {
    public static System.Collections.Generic.IEnumerable<Text.TextElementCluster> EnumerateTextElements(this System.IO.Stream source, System.Text.Encoding encoding)
      => new Flux.Text.TextElementEnumerator(source, encoding);
    public static System.Collections.Generic.IEnumerable<Text.TextElementCluster> EnumerateTextElements(this System.IO.TextReader source)
      => new Flux.Text.TextElementEnumerator(source);
  }

  public ref struct RefStruct
  {
    public IEnumerator<int> GetEnumerator()
    {
      return Foo();

      IEnumerator<int> Foo()
      {
        yield return 1;
      }
    }
  }

  public ref struct CharSpanEnumerator
  {
    private readonly System.ReadOnlySpan<char> m_characters;

    public CharSpanEnumerator(System.ReadOnlySpan<char> characters) => m_characters = characters;

    public CharSpanIterator GetEnumerator() => new CharSpanIterator();

    public ref struct CharSpanIterator
    {
      private CharSpanEnumerator m_enumerator;
      private int m_index;

      public CharSpanIterator(CharSpanEnumerator enumerator)
      {
        m_enumerator = enumerator;
        m_index = -1;
      }

      public bool MoveNext()
      {
        if (m_index < m_enumerator.m_characters.Length)
        {
          m_index++;

          return true;
        }

        return false;
      }

      public void Reset()
      {
        m_index = -1;
      }
    }
  }

  public sealed class TextElementEnumerator
    : Disposable, System.Collections.Generic.IEnumerable<TextElementCluster>
  {
    internal readonly System.IO.TextReader m_textReader;
    internal readonly int m_bufferSize;

    public TextElementEnumerator(System.IO.TextReader textReader, int bufferSize = 8192)
    {
      if (textReader is null) throw new System.ArgumentNullException(nameof(textReader));
      if (bufferSize < 128) throw new System.ArgumentOutOfRangeException(nameof(bufferSize));

      m_textReader = textReader;
      m_bufferSize = bufferSize;
    }
    public TextElementEnumerator(System.IO.Stream stream, System.Text.Encoding encoding, int bufferSize = 8192)
      : this(new System.IO.StreamReader(stream, encoding), bufferSize)
    { }

    public System.Collections.Generic.IEnumerator<TextElementCluster> GetEnumerator()
      => new TextElementIterator(this);
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();

    protected override void DisposeManaged()
      => m_textReader.Dispose();

    private sealed class TextElementIterator
      : System.Collections.Generic.IEnumerator<TextElementCluster>
    {
      private readonly TextElementEnumerator m_enumerator;

      private readonly char[] m_buffer;
      private int m_bufferIndex;
      private int m_bufferCount;

      private TextElementCluster m_current;

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

      private int m_overallIndex = 0;

      public TextElementCluster Current
        => m_current;
      object System.Collections.IEnumerator.Current
        => m_current!;

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

          m_current = new TextElementCluster(textElement, m_overallIndex); // Set current to the text element.

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
