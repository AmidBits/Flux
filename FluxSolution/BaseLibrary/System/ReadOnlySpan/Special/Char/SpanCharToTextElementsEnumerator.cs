namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    public static SpanCharToTextElementEnumerator EnumerateTextElements(this System.ReadOnlySpan<char> source)
      => new(source);
  }

  public ref struct SpanCharToTextElementEnumerator
  {
    private readonly System.ReadOnlySpan<char> m_span;

    private System.ReadOnlySpan<char> m_startOfNextTextElement;

    private Text.TextElement m_current;

    internal SpanCharToTextElementEnumerator(System.ReadOnlySpan<char> span)
    {
      m_span = span;

      m_startOfNextTextElement = m_span;

      m_current = new Text.TextElement(System.ReadOnlySpan<char>.Empty);
    }

    public Text.TextElement Current => m_current;

    public SpanCharToTextElementEnumerator GetEnumerator() => this;

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
      m_startOfNextTextElement = m_span;

      m_current = new Text.TextElement(System.ReadOnlySpan<char>.Empty);
    }
  }
}
