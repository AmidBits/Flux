namespace Flux
{
  public static partial class XtensionReadOnlySpan
  {
    extension<T>(System.ReadOnlySpan<T> source)
    {
      /// <summary>
      /// <para>Searches a <paramref name="source"/> text for all indices of a <paramref name="target"/> substring. Returns an empty list if not found. Uses the specified <paramref name="equalityComparer"/> (or default if null).</para>
      /// <see href="https://en.wikipedia.org/wiki/Knuth%E2%80%93Morris%E2%80%93Pratt_algorithm"/>
      /// </summary>
      public System.Collections.Generic.List<int> KnuthMorrisPrattIndices(System.ReadOnlySpan<T> target, out System.Collections.Generic.Dictionary<int, int> table, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        table = KnuthMorrisPrattTable(target, equalityComparer);

        var indices = new System.Collections.Generic.List<int>();

        var sourceIndex = 0;
        var targetIndex = 0;

        var sourceLength = source.Length;
        var targetLength = target.Length;

        while (sourceIndex < sourceLength)
        {
          if (equalityComparer.Equals(target[targetIndex], source[sourceIndex]))
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
      }

      /// <summary>
      /// <para>Creates a map of the amount of safely skippable elements in <paramref name="source"/> (word). Uses the specified <paramref name="equalityComparer"/> (or default if null).</para>
      /// <see href="https://en.wikipedia.org/wiki/Knuth%E2%80%93Morris%E2%80%93Pratt_algorithm"/>
      /// </summary>
      public System.Collections.Generic.Dictionary<int, int> KnuthMorrisPrattTable(System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        var table = new System.Collections.Generic.Dictionary<int, int>()
        {
          [0] = -1
        };

        var positionIndex = 1; // Position index.
        var candidateIndex = 0; // Current candidate index.

        while (positionIndex < source.Length)
        {
          if (equalityComparer.Equals(source[positionIndex], source[candidateIndex]))
            table[positionIndex] = table[candidateIndex];
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

      /// <summary>
      /// <para>The Prefix function for this span is an array of length n, where p[i] is the length of the longest proper prefix of the sub-span <paramref name="source"/>[0...i] which is also a suffix of this sub-span.</para>
      /// <para>A proper prefix of a span is a prefix that is not equal to the span itself.</para>
      /// <para>I.e., z[i] is the length of the longest common prefix between source and the suffix of source starting at i.</para>
      /// <para>Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Knuth%E2%80%93Morris%E2%80%93Pratt_algorithm"/></para>
      /// <para><see href="https://cp-algorithms.com/string/prefix-function.html"/></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      public int[] PrefixFunction(System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        var pi = new int[source.Length];

        for (int i = 1; i < source.Length; i++)
        {
          var j = pi[i - 1];

          while (j > 0 && !equalityComparer.Equals(source[i], source[j]))
            j = pi[j - 1];

          if (equalityComparer.Equals(source[i], source[j]))
            j++;

          pi[i] = j;
        }

        return pi;
      }

      /// <summary>
      /// <para>The Z-function for this span is an array of length n where the i-th element is equal to the greatest number of <typeparamref name="T"/>'s starting from the position i that coincide with the first <typeparamref name="T"/>'s of <paramref name="source"/>.</para>
      /// <para>I.e. z[i] is the length of the longest sub-span that is, at the same time, a prefix of <paramref name="source"/> and a prefix of the suffix of <paramref name="source"/> starting at i.</para>
      /// <para>Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// <para><see href="https://cp-algorithms.com/string/z-function.html"/></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      public int[] Zfunction(System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        var z = new int[source.Length];

        var l = 0;
        var r = 0;

        for (var i = 1; i < source.Length; i++)
        {
          if (i < r)
            z[i] = int.Min(r - i, z[i - l]);

          while (i + z[i] < source.Length && equalityComparer.Equals(source[z[i]], source[i + z[i]]))
            z[i]++;

          if (i + z[i] > r)
          {
            l = i;
            r = i + z[i];
          }
        }

        return z;
      }
    }
  }
}
