//namespace Flux
//{
//  public static partial class ExtensionMethodsReadOnlySpan
//  {
//    public static SpanEnumerator<T> Enumerate<T>(this System.ReadOnlySpan<T> source) => new(source);
//  }

//  public readonly ref struct SpanEnumerator<T>

//  {
//    private readonly System.ReadOnlySpan<T> m_source;

//    public SpanEnumerator(System.ReadOnlySpan<T> source) => m_source = source;

//    public System.ReadOnlySpan<T> Source => m_source;

//    public SpanIterator<T> GetEnumerator() => new SpanIterator<T>(this);
//  }

//  public ref struct SpanIterator<T>
//  {
//    private readonly SpanEnumerator<T> m_enumerator;

//    private int m_index;

//    private T m_current;

//    public SpanIterator(SpanEnumerator<T> enumerator)
//    {
//      m_enumerator = enumerator;
//      m_index = -1;
//      m_current = default!;
//    }

//    public T Current => m_current;

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
