using System.Linq;

namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>
    /// <para>Heap's algorithm generates all possible permutations of n objects by sharing the same array space, i.e. each permutation is lost when enumerating the next permutation.</para>
    /// <see href="https://en.wikipedia.org/wiki/Heap%27s_algorithm"/>
    /// </summary>
    /// <remarks>CAUTION! All permutations are created in the same single underlying storage array, which means that each permutation overwrites the previous permutation.</remarks>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<TResult> PermuteHeapsAlgorithm<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource[], TResult> resultSelector)
    {
      return Permute(source.ToArray());

      System.Collections.Generic.IEnumerable<TResult> Permute(TSource[] array)
      {
        var stackState = new int[array.Length];

        System.Array.Fill(stackState, default);

        yield return resultSelector(array);

        for (var stackIndex = 0; stackIndex < stackState.Length;)
        {
          if (stackState[stackIndex] < stackIndex)
          {
            if ((stackIndex & 1) == 0)
              array.AsSpan().Swap(0, stackIndex);
            else
              array.AsSpan().Swap(stackState[stackIndex], stackIndex);

            yield return resultSelector(array);

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
    }
    /// <summary>
    /// <para>Heap's algorithm generates all possible permutations of n objects by allocating arrays for each permutation.</para>
    /// <see href="https://en.wikipedia.org/wiki/Heap%27s_algorithm"/>
    /// </summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<TSource[]> PermuteHeapsAlgorithm<TSource>(this System.Collections.Generic.IEnumerable<TSource> source)
      => PermuteHeapsAlgorithm(source, a => a.ToArray());
  }
}
