namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Heap's algorithm generates all possible permutations of n objects.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Heap%27s_algorithm"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IReadOnlyList<T>> PermuteHeapsAlgorithm<T>(this System.Collections.Generic.IEnumerable<T> source)
    {
      return Permute(source.ToArray());

      static System.Collections.Generic.IEnumerable<System.Collections.Generic.IReadOnlyList<T>> Permute(T[] array)
      {
        var stackState = new int[array.Length];

        System.Array.Fill(stackState, default);

        yield return (System.Collections.Generic.IReadOnlyList<T>)array;

        for (var stackIndex = 0; stackIndex < stackState.Length;)
        {
          if (stackState[stackIndex] < stackIndex)
          {
            if ((stackIndex & 1) == 0)
              array.AsSpan().Swap(0, stackIndex);
            else
              array.AsSpan().Swap(stackState[stackIndex], stackIndex);

            yield return (System.Collections.Generic.IReadOnlyList<T>)array;

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
  }
}
