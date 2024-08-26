namespace Flux
{
  /// <summary>
  /// <para></para>
  /// </summary>
  public static class PermutationKnuthsAlgorithmL
  {
    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="initialIndices"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<int[]> GeneratePermutationIndices(int[] initialIndices)
    {
      do
      {
        yield return initialIndices;
      }
      while (NextPermutation(initialIndices));
    }

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="numberOfIndices"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<int[]> GeneratePermutationIndices(int numberOfIndices)
      => GeneratePermutationIndices(System.Linq.Enumerable.Range(0, numberOfIndices).ToArray());

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<T[]> GetPermutationsKnuthsAlgorithmL<T>(this System.Collections.Generic.IList<T> source)
    {
      var sourceLength = source.Count;

      foreach (var pi in GeneratePermutationIndices(sourceLength))
      {
        var ps = new T[sourceLength];

        for (var i = 0; i < sourceLength; i++)
          ps[i] = source[pi[i]];

        yield return ps;
      }
    }

    /// <summary>
    /// <para>Compute the next permutation of <paramref name="indices"/> using Knuth's "Algorithm L".</para>
    /// </summary>
    /// <param name="indices"></param>
    /// <returns></returns>
    /// <remarks>
    /// <para>This algorithm result in lexiographical order.</para>
    /// </remarks>
    public static bool NextPermutation(System.Span<int> indices)
    {
      // Knuths
      // 1. Find the largest index j such that a[j] < a[j + 1]. If no such index exists, the permutation is the last permutation.
      // 2. Find the largest index l such that a[j] < a[l]. Since j + 1 is such an index, l is well defined and satisfies j < l.
      // 3. Swap a[j] with a[l].
      // 4. Reverse the sequence from a[j + 1] up to and including the final element a[n].

      int largestIndex1 = -1, largestIndex2 = -1, i, j;

      for (i = indices.Length - 2; i >= 0; i--)
        if (indices[i] < indices[i + 1])
        {
          largestIndex1 = i;
          break;
        }

      if (largestIndex1 < 0) return false;

      for (j = indices.Length - 1; j >= 0; j--)
        if (indices[largestIndex1] < indices[j])
        {
          largestIndex2 = j;
          break;
        }

      indices.Swap(largestIndex1, largestIndex2);

      for (i = largestIndex1 + 1, j = indices.Length - 1; i < j; i++, j--)
        indices.Swap(i, j);

      return true;
    }
  }
}
