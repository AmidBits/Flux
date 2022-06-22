namespace Flux
{
  public ref partial struct SpanBuilder<T>
  {
    /// <summary>Reports the last index of any of the specified targets in the source. Or -1 if none were found. Uses the specified comparer.</summary>
    public int LastIndexOfAny(System.Collections.Generic.IEqualityComparer<T> equalityComparer, params T[] values)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      for (var sourceIndex = m_bufferPosition - 1; sourceIndex >= 0; sourceIndex--)
        for (var valueIndex = 0; valueIndex < values.Length; valueIndex++)
          if (equalityComparer.Equals(m_buffer[sourceIndex], values[valueIndex]))
            return sourceIndex;

      return -1;
    }
    /// <summary>Reports the last index of any of the specified targets in the source. Or -1 if none were found. Uses the default comparer</summary>
    public int LastIndexOfAny(params T[] values)
      => LastIndexOfAny(System.Collections.Generic.EqualityComparer<T>.Default, values);
  }
}
