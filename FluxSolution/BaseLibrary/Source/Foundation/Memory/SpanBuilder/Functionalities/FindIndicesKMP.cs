namespace Flux
{
  public ref partial struct SpanBuilder<T>
  {
    /// <summary>Searches a text for all indices of a substring. Returns an empty list if not found. Uses the specified equality comparer.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Knuth%E2%80%93Morris%E2%80%93Pratt_algorithm"/>
    public System.Collections.Generic.List<int> FindIndicesKMP(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      var table = CreateTable(target, equalityComparer);

      var indices = new System.Collections.Generic.List<int>();

      var sourceIndex = 0;
      var targetIndex = 0;

      var sourceLength = m_bufferPosition;
      var targetLength = target.Length;

      while (sourceIndex < sourceLength)
      {
        if (equalityComparer.Equals(target[targetIndex], m_buffer[sourceIndex]))
        {
          sourceIndex++;
          targetIndex++;

          if (targetIndex == targetLength)
          {
            indices.Add(sourceIndex - targetIndex);

            targetIndex = table[targetIndex];
          }
        }
        else
        {
          targetIndex = table[targetIndex];

          if (targetIndex < 0)
          {
            sourceIndex++;
            targetIndex++;
          }
        }
      }

      return indices;

      /// <summary>Creates a map of the amount of safely skippable elements in target (word).</summary>
      static System.Collections.Generic.Dictionary<int, int> CreateTable(System.ReadOnlySpan<T> source, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      {
        var table = new System.Collections.Generic.Dictionary<int, int>
        {
          [0] = -1
        };

        var positionIndex = 1; // Position index.
        var candidateIndex = 0; // Current candidate index.

        while (positionIndex < source.Length)
        {
          if (equalityComparer.Equals(source[positionIndex], source[candidateIndex]))
          {
            table[positionIndex] = table[candidateIndex];
          }
          else
          {
            table[positionIndex] = candidateIndex;

            while (candidateIndex >= 0 && !equalityComparer.Equals(source[positionIndex], source[candidateIndex]))
              candidateIndex = table[candidateIndex];
          }

          positionIndex++;
          candidateIndex++;
        }

        table[positionIndex] = candidateIndex;

        return table;
      }
    }
    /// <summary>Searches a text for all indices of a substring. Returns an empty list if not found. Uses the default equality comparer.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Knuth%E2%80%93Morris%E2%80%93Pratt_algorithm"/>
    public System.Collections.Generic.List<int> KnuthMorrisPrattSearch(System.ReadOnlySpan<T> target)
      => FindIndicesKMP(target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
