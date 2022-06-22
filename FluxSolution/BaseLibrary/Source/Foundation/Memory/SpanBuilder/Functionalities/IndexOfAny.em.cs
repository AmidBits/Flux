namespace Flux
{
  public ref partial struct SpanBuilder<T>
  {
    /// <summary>Reports the first index of any of the specified characters within the source, or -1 if none were found. Uses the specified comparer.</summary>
    public int IndexOfAny(System.Collections.Generic.IEqualityComparer<T> equalityComparer, params T[] values)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      for (var index = 0; index < m_bufferPosition; index++)
      {
        var character = m_buffer[index];

        if (System.Array.Exists(values, c => equalityComparer.Equals(c, character)))
          return index;
      }

      return -1;
    }
    /// <summary>Reports the first index of any of the specified characters within the source, or -1 if none were found. Uses the default comparer.</summary>
    public int IndexOfAny(params T[] values)
      => IndexOfAny(System.Collections.Generic.EqualityComparer<T>.Default, values);
  }
}
