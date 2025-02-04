
// This is all questionable and more to show the Enumerator class than useful. String.Split and RegEx.Split is more useful, IMO.

namespace Flux
{
  public static partial class Fx
  {
    public static SplitEnumerator<char> EnumerateSplits(this string source, string separator)
      => new(source.AsSpan(), separator.AsSpan());
    public static SplitEnumerator<char> EnumerateSplits(this string source, params char[] separators)
      => new(source.AsSpan(), separators);
  }

  /// <summary>
  /// <para></para>
  /// </summary>
  /// <remarks>
  /// <para>Must be a <see langword="ref"/> <see langword="struct"/> as it contains a <see cref="System.ReadOnlySpan{T}"/>.</para>
  /// </remarks>
  /// <typeparam name="T"></typeparam>
  public ref struct SplitEnumerator<T>
  {
    private readonly System.ReadOnlySpan<T> m_separator;
    private System.ReadOnlySpan<T> m_span;

    public SplitEnumerator(System.ReadOnlySpan<T> span, System.ReadOnlySpan<T> separator)
    {
      m_separator = separator;
      m_span = span;

      Current = default;
    }
    public SplitEnumerator(System.ReadOnlySpan<T> span, params T[] separators)
      : this(span, separators.AsSpan())
    {
    }

    public SplitEntry<T> Current { get; private set; }

    public readonly SplitEnumerator<T> GetEnumerator() // Compatibility with operator foreach.
      => this;

    public bool MoveNext()
    {
      if (m_span.Length == 0) // Span is empty.
        return false;

      if (m_span.IndexOf(m_separator, null) is var index && index > -1) // Separator found.
      {
        Current = new SplitEntry<T>(m_span[..index], m_span.Slice(index, m_separator.Length));
        m_span = m_span[(index + m_separator.Length)..];
      }
      else // Separator not found, so this is the last sub-span.
      {
        Current = new SplitEntry<T>(m_span, System.ReadOnlySpan<T>.Empty);
        m_span = System.ReadOnlySpan<T>.Empty;
      }

      return true;
    }
  }

  /// <summary>
  /// <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public readonly ref struct SplitEntry<T>
  {
    public SplitEntry(System.ReadOnlySpan<T> subSpan, System.ReadOnlySpan<T> separator)
    {
      SubSpan = subSpan;
      Separator = separator;
    }

    public System.ReadOnlySpan<T> SubSpan { get; }
    public System.ReadOnlySpan<T> Separator { get; }

    public void Deconstruct(out System.ReadOnlySpan<T> subSpan, out System.ReadOnlySpan<T> separator)
    {
      subSpan = SubSpan;
      separator = Separator;
    }

    public static implicit operator System.ReadOnlySpan<T>(SplitEntry<T> entry)
      => entry.SubSpan;
  }
}
