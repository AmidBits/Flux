namespace Flux
{
  public ref partial struct SpanBuilder<T>
  {
    /// <summary>Searches a text (source) for the index of a substring (target). Returns -1 if not found. Uses the specified equality comparer.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Boyer%E2%80%93Moore%E2%80%93Horspool_algorithm"/>
    public int FindIndexBMH(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      var skippable = CreateTable(AsReadOnlySpan(), target);

      var skip = 0;

      var sourceLength = m_bufferPosition;
      var targetLength = target.Length;

      while (sourceLength - skip >= targetLength)
      {
        if (Same(AsReadOnlySpan()[skip..], target, targetLength, equalityComparer))
          return skip;

        skip += skippable[m_buffer[skip + targetLength - 1]];
      }

      return -1;

      /// <summary>Creates a map of the amount of safely skippable elements.</summary>
      static System.Collections.Generic.Dictionary<T, int> CreateTable(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      {
        var table = new System.Collections.Generic.Dictionary<T, int>(); // The alphabet.

        var sourceLength = source.Length;
        var targetLength = target.Length;

        for (var index = System.Math.Max(targetLength, sourceLength) - 1; index >= 0; index--)
        {
          if (index < targetLength && target[index] is var wc && !table.ContainsKey(wc)) // Add to alphabet from word (needle) characters, if it is not already in the table.
            table.Add(wc, targetLength);
          if (index < sourceLength && source[index] is var tc && !table.ContainsKey(tc)) // Add to alphabet from text (haystack) characters, if it is not already in the table.
            table.Add(tc, targetLength);
        }

        for (var i = 0; i < targetLength; i++)
          table[target[i]] = targetLength - 1 - i;

        return table;
      }

      static bool Same(System.ReadOnlySpan<T> word1, System.ReadOnlySpan<T> word2, int length, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      {
        for (var i = length - 1; i >= 0; i--)
          if (!equalityComparer.Equals(word1[i], word2[i]))
            return false;

        return true;
      }
    }
    /// <summary>Searches a text for the index of a substring. Returns -1 if not found. Uses the default equality comparer.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Boyer%E2%80%93Moore%E2%80%93Horspool_algorithm"/>
    public int BoyerMooreHorspoolSearch(System.ReadOnlySpan<T> target)
      => FindIndexBMH(target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
