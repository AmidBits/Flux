namespace Flux.Combinatorics.Basics.Permutations
{
  /// <summary>
  /// <para>Knuth's "algorithm L" generates all possible permutations (without repeats) of n objects in lexiographical order.</para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/Permutation"/></para>
  /// <para><seealso href="https://www-cs-faculty.stanford.edu/~knuth/"/></para>
  /// </summary>
  public static class KnuthsAlgorithmL
  {
    //#region Extension methods

    ///// <summary>
    ///// <para>Creates a new sequence of all possible permutations of n objects, in lexiographical order (if the <paramref name="source"/> data is sorted). The order is based on the initial indices of the data in <paramref name="source"/>, so the order of the data itself does not matter.</para>
    ///// <para><seealso href="https://en.wikipedia.org/wiki/Permutation"/></para>
    ///// <para><seealso href="https://www-cs-faculty.stanford.edu/~knuth/"/></para>
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="source"></param>
    ///// <returns></returns>
    //public static System.Collections.Generic.IEnumerable<T[]> PermutationsKnuthsAlgorithmL<T>(this T[] source)
    //{
    //  var permutation = new T[source.Length];

    //  foreach (var indices in KnuthsAlgorithmL.GetPermutationIndices(source.Length))
    //  {
    //    indices.Select(index => source[index]).CopyInto(permutation);

    //    yield return permutation;
    //  }
    //}

    //#endregion // Extension methods

    //public static int Count(int setSize) => (int)System.Numerics.BigInteger.CreateChecked(setSize).PermutationsWithoutRepetition(setSize);

    //public static KnuthsAlgorithmL<T> Create<T>(T[] data)
    //  where T : System.IComparable<T>
    //  => new(data);

    //public static System.Collections.Generic.IEnumerable<T[]> GetPermutations<T>(T[] data)
    //  where T : System.IComparable<T>
    //{
    //  var ha = Create(data);

    //  while (ha.MoveNext())
    //    yield return ha.Current;
    //}

    /// <summary>
    /// <para>Generates the next permutation lexicographically after a given permutation. It changes the given permutation in-place. This implementation is Knuth's "Algorithm L".</para>
    /// <para>The algorithm works in-place, the <paramref name="previousPermutation"/> are permuted using IComparable or "as is".</para>
    /// </summary>
    /// <param name="previousPermutation"></param>
    /// <returns>The next permutation of <paramref name="previousPermutation"/> in lexiographical order. Uses IComparable in-place.</returns>
    /// <remarks>
    /// <para></para>
    /// </remarks>
    public static bool NextPermutation<T>(System.Span<T> previousPermutation)
      where T : System.IComparable<T>
    {
      // Knuths
      // 1. Find the largest index j such that a[j] < a[j + 1]. If no such index exists, the permutation is the last permutation.
      // 2. Find the largest index l such that a[j] < a[l]. Since j + 1 is such an index, l is well defined and satisfies j < l.
      // 3. Swap a[j] with a[l].
      // 4. Reverse the sequence from a[j + 1] up to and including the final element a[n].

      int largestIndex1 = -1, largestIndex2 = -1, i, j;

      for (i = previousPermutation.Length - 2; i >= 0; i--)
        if (previousPermutation[i].CompareTo(previousPermutation[i + 1]) < 0)
        {
          largestIndex1 = i;
          break;
        }

      if (largestIndex1 < 0)
        return false;

      for (j = previousPermutation.Length - 1; j >= 0; j--)
        if (previousPermutation[largestIndex1].CompareTo(previousPermutation[j]) < 0)
        {
          largestIndex2 = j;
          break;
        }

      previousPermutation.Swap(largestIndex1, largestIndex2);

      for (i = largestIndex1 + 1, j = previousPermutation.Length - 1; i < j; i++, j--)
        previousPermutation.Swap(i, j);

      return true;
    }

    ///// <summary>
    ///// <para>Creates a new sequence of permutations on the <paramref name="initialIndices"/>. The array <paramref name="initialIndices"/> is re-used for each iteration.</para>
    ///// </summary>
    ///// <param name="initialIndices"></param>
    ///// <returns></returns>
    ///// <remarks>The array is mutated each iteration to reflect the next permutation.</remarks>
    //public static System.Collections.Generic.IEnumerable<int[]> GetPermutationIndices(int[] initialIndices)
    //{
    //  do
    //  {
    //    yield return initialIndices;
    //  }
    //  while (NextPermutation<int>(initialIndices));
    //}

    ///// <summary>
    ///// <para>Creates a new sequence of permutations based on the <paramref name="numberOfIndices"/>, which is expanded into an ordinal array of indices [0, <paramref name="numberOfIndices"/>). This is the initial permutation.</para>
    ///// </summary>
    ///// <param name="numberOfIndices"></param>
    ///// <returns></returns>
    //public static System.Collections.Generic.IEnumerable<int[]> GetPermutationIndices(int numberOfIndices)
    //  => GetPermutationIndices(System.Linq.Enumerable.Range(0, numberOfIndices).ToArray());
  }

  ///// <summary>
  ///// <para>Knuth's "algorithm L" generates all possible permutations (without repeats) of n objects in lexiographical order.</para>
  ///// <para><seealso href="https://en.wikipedia.org/wiki/Permutation"/></para>
  ///// <para><seealso href="https://www-cs-faculty.stanford.edu/~knuth/"/></para>
  ///// </summary>
  //public sealed class KnuthsAlgorithmL<T>
  //  where T : System.IComparable<T>
  //{
  //  private readonly T[] m_original;

  //  private readonly T[] m_permutation; // The data from m_original while running.

  //  private int m_index; // The lexiographical index of the current permutation.

  //  public KnuthsAlgorithmL(T[] data)
  //  {
  //    m_original = new T[data.Length];
  //    data.CopyTo(m_original.AsSpan());

  //    m_permutation = new T[m_original.Length];

  //    Reset();
  //  }

  //  public T[] Current => m_permutation;

  //  public int Index => m_index;

  //  public bool MoveNext()
  //  {
  //    var change = m_index == -1 ? true : KnuthsAlgorithmL.NextPermutation<T>(m_permutation);

  //    if (change)
  //      m_index++;

  //    return change;
  //  }

  //  public void Reset()
  //  {
  //    m_original.CopyTo(m_permutation.AsSpan());

  //    m_index = -1;
  //  }
  //}
}
