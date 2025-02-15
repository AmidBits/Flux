namespace Flux
{
  public ref struct SpanCharToTextElementEnumerator
  {
    private readonly System.ReadOnlySpan<char> m_span;

    private Text.TextElement m_current;

    private System.ReadOnlySpan<char> m_startOfNextTextElement;

    internal SpanCharToTextElementEnumerator(System.ReadOnlySpan<char> span)
    {
      m_span = span;

      Reset();
    }

    internal SpanCharToTextElementEnumerator(System.Span<char> span) : this((System.ReadOnlySpan<char>)span) { }

    public readonly Text.TextElement Current => m_current;

    public readonly SpanCharToTextElementEnumerator GetEnumerator() => this;

    public bool MoveNext()
    {
      while (m_startOfNextTextElement.Length > 0)
      {
        var length = System.Globalization.StringInfo.GetNextTextElementLength(m_startOfNextTextElement);

        m_current = new Text.TextElement(m_startOfNextTextElement[..length]);

        m_startOfNextTextElement = m_startOfNextTextElement[length..];

        return true;
      }

      return false;
    }

    public void Reset()
    {
      m_current = new Text.TextElement([]);

      m_startOfNextTextElement = m_span;
    }
  }
}
