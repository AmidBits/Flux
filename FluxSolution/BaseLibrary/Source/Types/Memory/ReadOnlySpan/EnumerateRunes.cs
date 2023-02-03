//namespace Flux
//{
//  public static partial class ExtensionMethodsReadOnlySpan
//  {
//    public static SpanRuneEnumerator EnumerateRunes(this System.ReadOnlySpan<System.Text.Rune> source) => new(source);
//  }

//  public readonly ref struct SpanRuneEnumerator
//  {
//    private readonly System.ReadOnlySpan<System.Text.Rune> m_source;

//    public SpanRuneEnumerator(System.ReadOnlySpan<System.Text.Rune> source) => m_source = source;

//    public System.ReadOnlySpan<System.Text.Rune> Source => m_source;

//    public SpanRuneIterator GetEnumerator() => new SpanRuneIterator(this);
//  }

//  public ref struct SpanRuneIterator
//  {
//    private readonly SpanRuneEnumerator m_enumerator;

//    private int m_index;

//    private System.Text.Rune m_current;

//    public SpanRuneIterator(SpanRuneEnumerator enumerator)
//    {
//      m_enumerator = enumerator;
//      m_index = -1;
//    }

//    public System.Text.Rune Current => m_current;

//    public bool MoveNext()
//    {
//      if (m_index < m_enumerator.Source.Length)
//      {
//        m_current = m_enumerator.Source[m_index];

//        m_index++;

//        return true;
//      }

//      return false;
//    }

//    public void Reset()
//      => m_index = -1;
//  }
//}
