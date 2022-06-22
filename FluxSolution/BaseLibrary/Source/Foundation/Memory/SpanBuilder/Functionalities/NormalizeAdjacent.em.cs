namespace Flux
{
  public ref partial struct SpanBuilder<T>
  {
    /// <summary>Creates a new readonlyspan with the specified (or all if none specified) consecutive characters in the string normalized. Uses the specfied comparer.</summary>
    public System.ReadOnlySpan<T> NormalizeAdjacent(System.Collections.Generic.IEqualityComparer<T> equalityComparer, System.Collections.Generic.IList<T> values)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      var target = new T[m_bufferPosition];

      var index = 0;
      var previous = default(T);

      for (var indexOfSource = 0; indexOfSource < m_bufferPosition; indexOfSource++)
      {
        var current = m_buffer[indexOfSource];

        if (!equalityComparer.Equals(current, previous) || (values.Count > 0 && !values.Contains(current, equalityComparer)))
        {
          target[index++] = current;

          previous = current;
        }
      }

      return target[..index];
    }
    /// <summary>Creates a new readonlyspan with the specified (or all if none specified) consecutive characters in the string normalized. Uses the default comparer.</summary>
    public System.ReadOnlySpan<T> NormalizeAdjacent(System.Collections.Generic.IList<T> values)
      => NormalizeAdjacent(System.Collections.Generic.EqualityComparer<T>.Default, values);
  }
}
