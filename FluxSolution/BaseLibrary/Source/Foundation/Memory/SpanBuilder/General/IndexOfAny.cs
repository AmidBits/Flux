namespace Flux
{
  public ref partial struct SpanBuilder<T>
  {
    /// <summary>Reports the first index of any of the specified characters within the source, or -1 if none were found. Uses the specified comparer.</summary>
    public int IndexOfAny(System.Collections.Generic.IList<T> values, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      for (var index = 0; index < m_bufferPosition; index++)
        if (values.Contains(m_buffer[index], equalityComparer))
          return index;

      return -1;
    }
    /// <summary>Reports the first index of any of the specified characters within the source, or -1 if none were found. Uses the default comparer.</summary>
    public int IndexOfAny(System.Collections.Generic.IList<T> values)
      => IndexOfAny(values, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
