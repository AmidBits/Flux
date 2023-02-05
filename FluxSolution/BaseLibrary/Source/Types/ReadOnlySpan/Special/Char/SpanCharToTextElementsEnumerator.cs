namespace Flux
{
  public ref struct SpanCharToTextElementEnumerator
  {
    private readonly System.ReadOnlySpan<char> m_span;

    private System.ReadOnlySpan<char> m_startOfNextTextElement;
    private int m_indexOfNextTextElement;

    private Text.TextElement m_current;

    internal SpanCharToTextElementEnumerator(System.ReadOnlySpan<char> span)
    {
      m_span = span;

      m_indexOfNextTextElement = 0;
      m_startOfNextTextElement = m_span;

      m_current = new Text.TextElement(System.ReadOnlySpan<char>.Empty, -1);
    }

    public Text.TextElement Current => m_current;

    public SpanCharToTextElementEnumerator GetEnumerator() => this;

    public bool MoveNext()
    {
      while (m_startOfNextTextElement.Length > 0)
      {
        var length = System.Globalization.StringInfo.GetNextTextElementLength(m_startOfNextTextElement);

        m_current = new Text.TextElement(m_startOfNextTextElement[..length], m_indexOfNextTextElement);

        m_indexOfNextTextElement += length;
        m_startOfNextTextElement = m_startOfNextTextElement[length..];

        return true;
      }

      return false;
    }

    public void Reset()
    {
      m_indexOfNextTextElement = 0;
      m_startOfNextTextElement = m_span;

      m_current = new Text.TextElement(System.ReadOnlySpan<char>.Empty, -1);
    }
  }
}
