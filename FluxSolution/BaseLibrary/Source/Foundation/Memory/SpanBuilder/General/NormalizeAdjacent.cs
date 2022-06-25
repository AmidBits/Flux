namespace Flux
{
  public ref partial struct SpanBuilder<T>
  {
    /// <summary>Creates a new readonlyspan with the specified (or all if none specified) consecutive characters in the string normalized. Uses the specfied comparer.</summary>
    public void NormalizeAdjacent(System.Collections.Generic.IList<T> values, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      var targetIndex = 0;
      var previous = default(T);

      for (var sourceIndex = 0; sourceIndex < m_bufferPosition; sourceIndex++)
      {
        var current = m_buffer[sourceIndex];

        if (!equalityComparer.Equals(current, previous) || (values.Count > 0 && !values.Contains(current, equalityComparer)))
        {
          m_buffer[targetIndex++] = current;

          previous = current;
        }
      }

      m_bufferPosition = targetIndex;

      Clear();
    }
    /// <summary>Creates a new readonlyspan with the specified (or all if none specified) consecutive characters in the string normalized. Uses the default comparer.</summary>
    public void NormalizeAdjacent(System.Collections.Generic.IList<T> values)
      => NormalizeAdjacent(values, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
