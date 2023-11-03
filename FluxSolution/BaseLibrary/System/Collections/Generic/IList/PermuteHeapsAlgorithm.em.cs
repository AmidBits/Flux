using System.Linq;

namespace Flux
{
  public static partial class Permutation
  {
    /// <summary>
    /// <para>Heap's algorithm generates all possible permutations of n objects by sharing the same array space, i.e. each permutation is lost when enumerating the next permutation. This is not a pure method.</para>
    /// <see href="https://en.wikipedia.org/wiki/Heap%27s_algorithm"/>
    /// </summary>
    /// <remarks>CAUTION! All permutations are created in the same single underlying storage array, which means that each permutation overwrites the previous permutation.</remarks>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<TResult> PermuteHeapsAlgorithm<TSource, TResult>(this System.Collections.Generic.IList<TSource>[] source, System.Func<System.Collections.Generic.IList<TSource>[], TResult> resultSelector)
    {
      var stackState = new int[source.Length];

      System.Array.Fill(stackState, default);

      yield return resultSelector(source);

      for (var stackIndex = 0; stackIndex < stackState.Length;)
      {
        if (stackState[stackIndex] < stackIndex)
        {
          if ((stackIndex & 1) == 0)
            source.AsSpan().Swap(0, stackIndex);
          else
            source.AsSpan().Swap(stackState[stackIndex], stackIndex);

          yield return resultSelector(source);

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
    /// <para>Heap's algorithm generates all possible permutations of n objects by allocating new storage for each permutation.</para>
    /// <see href="https://en.wikipedia.org/wiki/Heap%27s_algorithm"/>
    /// </summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<TSource>[]> PermuteHeapsAlgorithm<TSource>(this System.Collections.Generic.IList<TSource>[] source)
      => PermuteHeapsAlgorithm(source, a => a.ToArray());
  }
}
