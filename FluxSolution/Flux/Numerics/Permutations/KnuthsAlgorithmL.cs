namespace Flux.Numerics.Permutations
{
  /// <summary>
  /// <para>A static class that implements permutations using Knuth's "algorithm L".</para>
  /// </summary>
  /// <remarks>
  /// <para>This algorithm yields permutations in lexiographical order.</para>
  /// </remarks>
  public static class KnuthsAlgorithmL
  {
    /// <summary>
    /// <para>Compute the next permutation of <paramref name="indices"/>, based on it's current state, using Knuth's "Algorithm L".</para>
    /// <para>The <paramref name="indices"/> is picked up "on the go" so the initial state does not have to be the ordinal indices.</para>
    /// </summary>
    /// <param name="indices"></param>
    /// <returns></returns>
    /// <remarks>
    /// <para>This result is the next permutation in lexiographical order.</para>
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

    /// <summary>
    /// <para>Creates a new sequence of permutations based on the <paramref name="source"/> data.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <remarks>The array is mutated each iteration to reflect the next permutation.</remarks>
    public static System.Collections.Generic.IEnumerable<T[]> PermuteAlgorithmL<T>(this System.Collections.Generic.IList<T> source)
    {
      var sourceCount = source.Count;

      var permutation = new T[sourceCount];

      foreach (var indices in YieldPermutationIndices(sourceCount))
      {
        for (var i = sourceCount - 1; i >= 0; i--)
          permutation[i] = source[indices[i]];

        yield return permutation;
      }
    }

    /// <summary>
    /// <para>Creates a new sequence of permutations on the <paramref name="initialIndices"/>. The array <paramref name="initialIndices"/> is re-used for each iteration.</para>
    /// </summary>
    /// <param name="initialIndices"></param>
    /// <returns></returns>
    /// <remarks>The array is mutated each iteration to reflect the next permutation.</remarks>
    public static System.Collections.Generic.IEnumerable<int[]> YieldPermutationIndices(int[] initialIndices)
    {
      do
      {
        yield return initialIndices;
      }
      while (NextPermutation(initialIndices));
    }

    /// <summary>
    /// <para>Creates a new sequence of permutations based on the <paramref name="numberOfIndices"/>, which is expanded into an ordinal array of indices [0, <paramref name="numberOfIndices"/>). This is the initial permutation.</para>
    /// </summary>
    /// <param name="numberOfIndices"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<int[]> YieldPermutationIndices(int numberOfIndices)
      => YieldPermutationIndices(System.Linq.Enumerable.Range(0, numberOfIndices).ToArray());
  }
}
