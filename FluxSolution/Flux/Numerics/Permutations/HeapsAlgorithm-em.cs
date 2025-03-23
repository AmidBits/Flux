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
    public static System.Collections.Generic.IEnumerable<T[]> PermuteHeapsAlgorithm<T>(this T[] source)
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
