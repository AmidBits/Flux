namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para>Heap's algorithm generates all possible permutations of n objects by sharing the same array space, i.e. each permutation is lost when enumerating the next permutation. This is not a pure method.</para>
    /// <see href="https://en.wikipedia.org/wiki/Heap%27s_algorithm"/>
    /// </summary>
    /// <remarks>CAUTION! All permutations are created in the same single underlying storage array, which means that each permutation overwrites the previous permutation in that same underlying storage array.</remarks>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<TSource[]> PermuteHeapsAlgorithm<TSource>(this TSource[] source)
    {
      var stackState = new int[source.Length];

      System.Array.Fill(stackState, default);

      yield return source;

      for (var stackIndex = 0; stackIndex < stackState.Length;)
      {
        if (stackState[stackIndex] < stackIndex)
        {
          if ((stackIndex & 1) == 0)
            source.Swap(0, stackIndex);
          else
            source.Swap(stackState[stackIndex], stackIndex);

          yield return source;

          stackState[stackIndex]++;
          stackIndex = 0;
        }
        else
        {
          stackState[stackIndex] = 0;
          stackIndex++;
        }
      }
    }

    /// <summary>
    /// <para>Creates a new instance of <see cref="HeapsAlgorithm{TSource}"/> which enables the generation of all possible permutations of n objects.</para>
    /// <see href="https://en.wikipedia.org/wiki/Heap%27s_algorithm"/>
    /// </summary>
    /// <remarks>CAUTION! All permutations are created in the same single underlying storage array, which means that each permutation overwrites the previous permutation in that same underlying storage array.</remarks>
    /// <exception cref="System.ArgumentNullException"/>
    public static void PermuteHeapsAlgorithm<TSource>(this TSource[] source, out Numerics.Permutations.HeapsAlgorithm<TSource> result) => result = new(source);
  }
}
