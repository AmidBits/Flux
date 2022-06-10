namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Heap's algorithm generates all possible permutations of n objects.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Heap%27s_algorithm"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IReadOnlyList<T>> PermuteHeapsAlgorithm<T>(this System.Collections.Generic.IEnumerable<T> source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      return Permute(source.ToList());

      static System.Collections.Generic.IEnumerable<System.Collections.Generic.IReadOnlyList<T>> Permute(System.Collections.Generic.IList<T> list)
      {
        var stackState = new int[list.Count];

        System.Array.Fill(stackState, default);

        yield return (System.Collections.Generic.IReadOnlyList<T>)list;

        for (var stackIndex = 0; stackIndex < stackState.Length;)
        {
          if (stackState[stackIndex] < stackIndex)
          {
            if ((stackIndex & 1) == 0)
              list.AsSpan().Swap(0, stackIndex);
            else
              list.AsSpan().Swap(stackState[stackIndex], stackIndex);

            yield return (System.Collections.Generic.IReadOnlyList<T>)list;

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
