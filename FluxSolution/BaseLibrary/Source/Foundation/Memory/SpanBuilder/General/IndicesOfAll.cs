namespace Flux
{
  public ref partial struct SpanBuilder<T>
  {
    /// <summary>Reports all first indices of the specified targets within the source (-1 if not found). Uses the specified comparer.</summary>
    public int[] IndicesOfAll(System.Collections.Generic.IList<T> values, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      var indices = new int[values.Count];

      System.Array.Fill(indices, -1);

      for (var sourceIndex = 0; sourceIndex < m_bufferPosition; sourceIndex++)
      {
        var sourceItem = m_buffer[sourceIndex];

        for (var valueIndex = 0; valueIndex < values.Count; valueIndex++)
        {
          if (indices[valueIndex] == -1 && equalityComparer.Equals(sourceItem, values[valueIndex]))
          {
            indices[valueIndex] = sourceIndex;

            if (!System.Array.Exists(indices, i => i == -1))
              return indices;
          }
        }
      }

      return indices;
    }
    /// <summary>Reports all first indices of the specified targets within the source (-1 if not found). Uses the specified comparer.</summary>
    public int[] IndicesOfAll(System.Collections.Generic.IList<T> values)
      => IndicesOfAll(values, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
