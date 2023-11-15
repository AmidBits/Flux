namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    public static SpanRuneToCharEnumerator EnumerateChars(this System.ReadOnlySpan<System.Text.Rune> source)
      => new(source);
  }

  public ref struct SpanRuneToCharEnumerator
  {
    private readonly System.ReadOnlySpan<System.Text.Rune> m_span;
    private int m_spanIndex;

    private System.ReadOnlySpan<char> m_spanChar;
    private int m_spanCharIndex;

    private char m_current;

    internal SpanRuneToCharEnumerator(System.ReadOnlySpan<System.Text.Rune> span)
    {
      m_span = span;
      m_spanIndex = 0;

      m_spanChar = m_span.Length > 0 ? m_span[0].ToString() : System.ReadOnlySpan<char>.Empty;
      m_spanCharIndex = 0;

      m_current = default;
    }

    public char Current => m_current;

    public SpanRuneToCharEnumerator GetEnumerator() => this;

    public bool MoveNext()
    {
      if (m_spanCharIndex < m_spanChar.Length)
      {
        m_current = m_spanChar[m_spanCharIndex++];

        return true;
      }

      if (m_spanIndex < m_span.Length)
      {
        m_spanChar = m_span[m_spanIndex++].ToString();
        m_spanCharIndex = 0;

        m_current = m_spanChar[m_spanCharIndex++];

        return true;
      }

      return false;
    }

    public void Reset()
    {
      m_spanIndex = 0;

      m_spanChar = m_span.Length > 0 ? m_span[0].ToString() : System.ReadOnlySpan<char>.Empty;
      m_spanCharIndex = 0;
    }
  }
}
