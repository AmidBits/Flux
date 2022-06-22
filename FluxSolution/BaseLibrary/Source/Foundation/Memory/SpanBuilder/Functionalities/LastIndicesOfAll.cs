namespace Flux
{
  public ref partial struct SpanBuilder<T>
  {
    /// <summary>Reports all last indices of the specified targets within the source (-1 if not found). Uses the specified comparer.</summary>
    public int[] LastIndicesOfAll(System.Collections.Generic.IEqualityComparer<T> equalityComparer, params T[] values)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      var lastIndices = new int[values.Length];

      System.Array.Fill(lastIndices, -1);

      for (var sourceIndex = m_bufferPosition - 1; sourceIndex >= 0; sourceIndex--)
      {
        var sourceChar = m_buffer[sourceIndex];

        for (var valueIndex = values.Length - 1; valueIndex >= 0; valueIndex--)
        {
          if (lastIndices[valueIndex] == -1 && equalityComparer.Equals(sourceChar, values[valueIndex]))
          {
            lastIndices[valueIndex] = sourceIndex;

            if (!System.Array.Exists(lastIndices, i => i == -1))
              return lastIndices;
          }
        }
      }

      return lastIndices;
    }
    /// <summary>Reports all last indices of the specified targets within the source (-1 if not found). Uses the default comparer.</summary>
    public int[] LastIndicesOfAll(params T[] values)
      => LastIndicesOfAll(System.Collections.Generic.EqualityComparer<T>.Default, values);
  }
}
