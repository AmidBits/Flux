namespace Flux.Permutations
{
  /// <summary>
  /// <para>Heap's algorithm generates all possible permutations of n objects.</para>
  /// <para>The algorithm minimizes movement: it generates each permutation from the previous one by interchanging a single pair of elements; the other n−2 elements are not disturbed.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Heap%27s_algorithm"/></para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/Permutation"/></para>
  /// </summary>
  public static partial class HeapsAlgorithm
  {
    /// <summary>
    /// <para>Creates a new sequence of all possible permutations of n objects.</para>
    /// <para>NOTE! This implementation share the same array space between each permutation, i.e. the previous permutation is lost to the next permutation.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Heap%27s_algorithm"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Permutation"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <remarks>CAUTION! All permutations are created in the same single underlying storage array, which means that each permutation overwrites the previous permutation.</remarks>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<T[]> PermutationsHeapsAlgorithm<T>(this T[] source)
    {
      var stackState = new int[source.Length];

      System.Array.Fill(stackState, default);

      yield return source;

      var stackIndex = 0;

      while (stackIndex < stackState.Length)
      {
        if (stackState[stackIndex] < stackIndex)
        {
          if (int.IsEvenInteger(stackIndex))
            source.Swap(0, stackIndex);
          else
            source.Swap(stackState[stackIndex], stackIndex);

          yield return source;

          stackState[stackIndex]++;
          stackIndex = 1;
        }
        else
        {
          stackState[stackIndex] = 0;
          stackIndex++;
        }
      }
    }
  }
}
