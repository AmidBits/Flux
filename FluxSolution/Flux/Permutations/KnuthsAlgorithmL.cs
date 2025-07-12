namespace Flux.Permutations
{
  /// <summary>
  /// <para>Knuth's "algorithm L" generates all possible permutations of n objects in lexiographical order.</para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/Permutation"/></para>
  /// <para><seealso href="https://www-cs-faculty.stanford.edu/~knuth/"/></para>
  /// </summary>
  public static class KnuthsAlgorithmL
  {
    #region Extension methods

    /// <summary>
    /// <para>Creates a new sequence of all possible permutations of n objects, in lexiographical order (if the <paramref name="source"/> data is sorted). The order is based on the initial indices of the data in <paramref name="source"/>, so the order of the data itself does not matter.</para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Permutation"/></para>
    /// <para><seealso href="https://www-cs-faculty.stanford.edu/~knuth/"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<T[]> PermutationsKnuthsAlgorithmL<T>(this T[] source)
    {
      var permutation = new T[source.Length];

      foreach (var indices in Permutations.KnuthsAlgorithmL.GetPermutationIndices(source.Length))
      {
        indices.Select(index => source[index]).CopyInto(permutation);

        yield return permutation;
      }
    }

    #endregion // Extension methods

    /// <summary>
    /// <para>Generates the next permutation lexicographically after a given permutation. It changes the given permutation in-place. This implementation is Knuth's "Algorithm L".</para>
    /// <para>The <paramref name="items"/> are picked up "as is" or "on the go" so to speak.</para>
    /// </summary>
    /// <param name="items"></param>
    /// <returns>The next permutation of <paramref name="items"/> in lexiographical order.</returns>
    /// <remarks>
    /// <para></para>
    /// </remarks>
    public static bool NextPermutation<T>(System.Span<T> items)
      where T : System.IComparable<T>
    {
      // Knuths
      // 1. Find the largest index j such that a[j] < a[j + 1]. If no such index exists, the permutation is the last permutation.
      // 2. Find the largest index l such that a[j] < a[l]. Since j + 1 is such an index, l is well defined and satisfies j < l.
      // 3. Swap a[j] with a[l].
      // 4. Reverse the sequence from a[j + 1] up to and including the final element a[n].

      int largestIndex1 = -1, largestIndex2 = -1, i, j;

      for (i = items.Length - 2; i >= 0; i--)
        if (items[i].CompareTo(items[i + 1]) < 0)
        {
          largestIndex1 = i;
          break;
        }

      if (largestIndex1 < 0)
        return false;

      for (j = items.Length - 1; j >= 0; j--)
        if (items[largestIndex1].CompareTo(items[j]) < 0)
        {
          largestIndex2 = j;
          break;
        }

      items.Swap(largestIndex1, largestIndex2);

      for (i = largestIndex1 + 1, j = items.Length - 1; i < j; i++, j--)
        items.Swap(i, j);

      return true;
    }

    public static void PermutationInitKnuthsAlgorithmL(this System.Span<int> source)
    {
      for(var index = source.Length - 1; index >= 0; index--)
        source[index] = index;
    }
    
    public static bool PermutationNextKnuthsAlgorithmL(this System.Span<int> source)
    {
      // Knuths
      // 1. Find the largest index j such that a[j] < a[j + 1]. If no such index exists, the permutation is the last permutation.
      // 2. Find the largest index l such that a[j] < a[l]. Since j + 1 is such an index, l is well defined and satisfies j < l.
      // 3. Swap a[j] with a[l].
      // 4. Reverse the sequence from a[j + 1] up to and including the final element a[n].

      int largestIndex1 = -1, largestIndex2 = -1, i, j;

      for (i = items.Length - 2; i >= 0; i--)
        if (source[i].CompareTo(items[i + 1]) < 0)
        {
          largestIndex1 = i;
          break;
        }

      if (largestIndex1 < 0)
        return false;

      for (j = source.Length - 1; j >= 0; j--)
        if (source[largestIndex1].CompareTo(source[j]) < 0)
        {
          largestIndex2 = j;
          break;
        }

      source.Swap(largestIndex1, largestIndex2);

      for (i = largestIndex1 + 1, j = source.Length - 1; i < j; i++, j--)
        source.Swap(i, j);

      return true;
    }    

    /// <summary>
    /// <para>Creates a new sequence of permutations on the <paramref name="initialIndices"/>. The array <paramref name="initialIndices"/> is re-used for each iteration.</para>
    /// </summary>
    /// <param name="initialIndices"></param>
    /// <returns></returns>
    /// <remarks>The array is mutated each iteration to reflect the next permutation.</remarks>
    public static System.Collections.Generic.IEnumerable<int[]> GetPermutationIndices(int[] initialIndices)
    {
      do
      {
        yield return initialIndices;
      }
      while (NextPermutation<int>(initialIndices));
    }

    /// <summary>
    /// <para>Creates a new sequence of permutations based on the <paramref name="numberOfIndices"/>, which is expanded into an ordinal array of indices [0, <paramref name="numberOfIndices"/>). This is the initial permutation.</para>
    /// </summary>
    /// <param name="numberOfIndices"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<int[]> GetPermutationIndices(int numberOfIndices)
      => GetPermutationIndices(System.Linq.Enumerable.Range(0, numberOfIndices).ToArray());
  }
}
