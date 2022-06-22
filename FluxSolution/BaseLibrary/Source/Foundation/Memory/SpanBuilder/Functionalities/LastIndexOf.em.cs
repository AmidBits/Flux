namespace Flux
{
  public ref partial struct SpanBuilder<T>
  {
    /// <summary>Reports the index of the last occurence that satisfies the predicate.</summary>
    public int LastIndexOf(System.Func<T, int, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      for (var index = m_bufferPosition - 1; index >= 0; index--)
        if (predicate(m_buffer[index], index))
          return index;

      return -1;
    }

    /// <summary>Returns the last index of the occurence of the target within the source. Or -1 if not found. Uses the specified comparer.</summary>
    public int LastIndexOf(T value, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      for (var index = m_bufferPosition - 1; index >= 0; index--)
        if (equalityComparer.Equals(m_buffer[index], value))
          return index;

      return -1;
    }
    /// <summary>Returns the last index of the occurence of the target within the source. Or -1 if not found. Uses the default comparer.</summary>
    public int LastIndexOf(T value)
      => LastIndexOf(value, System.Collections.Generic.EqualityComparer<T>.Default);

    /// <summary>Reports the last index of the occurence of the target within the source. Or -1 if not found. Uses the specified comparer.</summary>
    public int LastIndexOf(System.ReadOnlySpan<T> value, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      for (var index = m_bufferPosition - value.Length; index >= 0; index--)
        if (EqualsAt(index, value, 0, value.Length, equalityComparer))
          return index;

      return -1;
    }
    /// <summary>Reports the last index of the occurence of the target within the source. Or -1 if not found. Uses the default comparer</summary>
    public int LastIndexOf(System.ReadOnlySpan<T> value)
      => LastIndexOf(value, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
