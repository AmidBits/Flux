namespace Flux
{
  public static partial class ReadOnlyMemoryExtensions
  {
    public static SplitEnumerable Split(this ReadOnlyMemory<char> memory, char separator, StringSplitOptions options = StringSplitOptions.None)
      => new(memory, separator, options);

    public readonly ref struct SplitEnumerable
    {
      private readonly ReadOnlyMemory<char> m_memory;
      private readonly char m_separator;
      private readonly StringSplitOptions m_options;

      public SplitEnumerable(ReadOnlyMemory<char> memory, char separator, StringSplitOptions options)
      {
        m_memory = memory;
        m_separator = separator;
        m_options = options;
      }

      public SplitEnumerator GetEnumerator() => new(m_memory, m_separator, m_options);
    }

    public ref struct SplitEnumerator
    {
      private ReadOnlyMemory<char> m_remaining;
      private readonly char m_separator;
      private readonly StringSplitOptions m_options;
      private bool m_emitTrailingEmpty;

      public SplitEnumerator(ReadOnlyMemory<char> memory, char separator, StringSplitOptions options)
      {
        m_remaining = memory;
        m_separator = separator;
        m_options = options;
        m_emitTrailingEmpty = false;

        Current = default;
      }

      public ReadOnlyMemory<char> Current { get; private set; }

      public bool MoveNext()
      {
        while (true)
        {
          if (m_emitTrailingEmpty)
          {
            m_emitTrailingEmpty = false;

            Current = ReadOnlyMemory<char>.Empty;

            return (m_options & StringSplitOptions.RemoveEmptyEntries) == 0;
          }

          if (m_remaining.IsEmpty)
            return false;

          var index = m_remaining.Span.IndexOf(m_separator);

          ReadOnlyMemory<char> current;

          if (index < 0)
          {
            current = m_remaining;
            m_remaining = default;
          }
          else
          {
            current = m_remaining[..index];

            m_emitTrailingEmpty = index == m_remaining.Length - 1;

            m_remaining = m_remaining[(index + 1)..];
          }

          if ((m_options & StringSplitOptions.TrimEntries) != 0)
            current = Trim(current);

          if ((m_options & StringSplitOptions.RemoveEmptyEntries) != 0 && current.IsEmpty)
            continue;

          Current = current;
          return true;
        }
      }

      private static ReadOnlyMemory<char> Trim(ReadOnlyMemory<char> memory)
      {
        ReadOnlySpan<char> span = memory.Span;

        int start = 0;
        int end = span.Length - 1;

        while (start < span.Length && char.IsWhiteSpace(span[start]))
          start++;

        while (end >= start && char.IsWhiteSpace(span[end]))
          end--;

        return memory.Slice(start, end - start + 1);
      }
    }
  }
}
