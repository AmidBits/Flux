namespace Flux
{
  public ref partial struct SpanBuilder<T>
  {
    /// <summary>Reports the index of the first occurence that satisfy the predicate.</summary>
    public int IndexOf(System.Func<T, int, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      for (var index = 0; index < m_bufferPosition; index++)
        if (predicate(m_buffer[index], index))
          return index;

      return -1;
    }

    /// <summary>Reports the first index of the specified target within the source, or -1 if not found. Uses the specified comparer.</summary>
    public int IndexOf(T value, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      for (var index = 0; index < m_bufferPosition; index++)
        if (equalityComparer.Equals(m_buffer[index], value))
          return index;

      return -1;
    }
    /// <summary>Reports the first index of the specified target within the source, or -1 if not found. Uses the default comparer.</summary>
    public int IndexOf(T value)
      => IndexOf(value, System.Collections.Generic.EqualityComparer<T>.Default);

    /// <summary>Returns the first index of the specified target within the source, or -1 if not found. Uses the specified comparer.</summary>
    public int IndexOf(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      var targetLength = target.Length;

      var maxLength = m_bufferPosition - targetLength;

      for (var index = 0; index < maxLength; index++)
        if (EqualsAt(index, target, 0, targetLength, equalityComparer))
          return index;

      return -1;
    }
    /// <summary>Reports the first index of the specified target within the source, or -1 if not found. Uses the default comparer.</summary>
    public int IndexOf(System.ReadOnlySpan<T> value)
      => IndexOf(value, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
