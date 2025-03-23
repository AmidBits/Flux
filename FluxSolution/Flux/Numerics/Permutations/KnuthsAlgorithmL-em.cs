namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para>Yields a new sequence, in lexiographical order, of all permutations of the <paramref name="source"/> data. The order is based on the initial indices of the data in <paramref name="source"/>, not the data itself.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<T[]> PermuteAllKnuthsAlgorithmL<T>(this T[] source)
    {
      var permutation = new T[source.Length];

      foreach (var indices in Numerics.Permutations.KnuthsAlgorithmL.GetPermutationIndices(source.Length))
      {
        indices.Select(index => source[index]).CopyInto(permutation);

        yield return permutation;
      }
    }

    /// <summary>
    /// <para>Yields a new sequence, in lexiographical order, of permutations based on the inital order of the <paramref name="source"/> data. No initial sorting is applied, and the algorithm picks up from the passed order and yields a lexiographical "rest" of the permutations.</para>
    /// <para>If the data in <paramref name="source"/> is in ascending order, a full set of permutations are generated, otherwise (from a lexiographical point of view) only "the remaining" permutations are generated.</para>
    /// <para>The <paramref name="source"/> array is mutated each iteration with the next permutation. The order in the source is always in descending order when the algorithm is finished.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<T[]> PermuteKnuthsAlgorithmL<T>(this T[] source)
      where T : System.IComparable<T>
    {
      do
      {
        yield return source;
      }
      while (Numerics.Permutations.KnuthsAlgorithmL.NextPermutation<T>(source));

      System.Array.Reverse(source);
    }
  }
}
