namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Heap's algorithm generates all possible permutations of n objects.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Heap%27s_algorithm"/>
    public static System.Collections.Generic.IEnumerable<TSource[]> PermuteHeapsAlgorithm<TSource>(this System.Collections.Generic.IEnumerable<TSource> source)
    {
      return Permute(source.ToArray());

      static System.Collections.Generic.IEnumerable<TSource[]> Permute(TSource[] array)
      {
        var stackState = new int[array.Length];

        System.Array.Fill(stackState, default);

        yield return array.ToArray();

        for (var stackIndex = 0; stackIndex < stackState.Length;)
        {
          if (stackState[stackIndex] < stackIndex)
          {
            if ((stackIndex & 1) == 0)
              array.AsSpan().Swap(0, stackIndex);
            else
              array.AsSpan().Swap(stackState[stackIndex], stackIndex);

            yield return array.ToArray();

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
