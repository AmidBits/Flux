namespace Flux
{
  public ref struct ReadOnlySpanSplitIterator<T>
    where T : System.IEquatable<T>
  {
    private System.ReadOnlySpan<T> m_source;
    private T m_split;

    public System.ReadOnlySpan<T> Current { get; private set; }

    public ReadOnlySpanSplitIterator(System.ReadOnlySpan<T> source, T split)
    {
      m_source = source;
      m_split = split;

      Current = default;
    }

    public bool MoveNext()
    {
      if (m_source.Length > 0)
      {
        var split = m_split;

        var index = m_source.IndexOf((e, i) => e.Equals(split));

        if (index == -1)
        {
          Current = m_source;
          m_source = System.ReadOnlySpan<T>.Empty;
        }
        else
        {
          Current = m_source.Slice(0, index);
          m_source = m_source.Slice(index + 1);
        }

        return true;
      }

      return false;
    }

    public bool TryMoveNext(out System.ReadOnlySpan<T> result)
    {
      if (MoveNext())
      {
        result = Current;
        return true;
      }
      else
      {
        result = default;
        return false;
      }
    }
  }
}