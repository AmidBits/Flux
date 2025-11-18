namespace Flux
{
  public static partial class ReadOnlySpanExtensions
  {
    public static SpanCharToTextElementEnumerator EnumerateTextElements(this System.ReadOnlySpan<char> source)
      => new(source);
  }

  public ref struct SpanCharToTextElementEnumerator
  {
    private readonly System.ReadOnlySpan<char> m_span;

    private System.Range m_current;

    internal SpanCharToTextElementEnumerator(System.ReadOnlySpan<char> span)
    {
      m_span = span;

      Reset();
    }

    internal SpanCharToTextElementEnumerator(System.Span<char> span) : this((System.ReadOnlySpan<char>)span) { }

    public readonly System.Range Current => m_current;

    public readonly SpanCharToTextElementEnumerator GetEnumerator() => this;

    public bool MoveNext()
    {
      while (m_current.Start.Value >= 0)
      {
        var offset = m_current.Start.Value + m_current.End.Value;

        var length = System.Globalization.StringInfo.GetNextTextElementLength(m_span[offset..]);

        if (length == 0)
        {
          m_current = System.Range.FromOffsetAndLength(-1, 0);

          break;
        }

        m_current = System.Range.FromOffsetAndLength(offset, length);

        return true;
      }

      return false;
    }

    public void Reset()
      => m_current = System.Range.FromOffsetAndLength(0, 0);
  }

  //public ref struct SpanCharToTextElementEnumerator
  //{
  //  private readonly System.ReadOnlySpan<char> m_span;

  //  private Text.TextElement m_current;

  //  private System.ReadOnlySpan<char> m_startOfNextTextElement;

  //  internal SpanCharToTextElementEnumerator(System.ReadOnlySpan<char> span)
  //  {
  //    m_span = span;

  //    Reset();
  //  }

  //  internal SpanCharToTextElementEnumerator(System.Span<char> span) : this((System.ReadOnlySpan<char>)span) { }

  //  public readonly Text.TextElement Current => m_current;

  //  public readonly SpanCharToTextElementEnumerator GetEnumerator() => this;

  //  public bool MoveNext()
  //  {
  //    while (m_startOfNextTextElement.Length > 0)
  //    {
  //      var length = System.Globalization.StringInfo.GetNextTextElementLength(m_startOfNextTextElement);

  //      m_current = new Text.TextElement(m_startOfNextTextElement[..length].ToString());

  //      m_startOfNextTextElement = m_startOfNextTextElement[length..];

  //      return true;
  //    }

  //    return false;
  //  }

  //  public void Reset()
  //  {
  //    m_current = new Text.TextElement(string.Empty);

  //    m_startOfNextTextElement = m_span;
  //  }
  //}
}
