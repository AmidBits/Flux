namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Generates all possible permutations of the elements in the sequence. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Permutation"/>
    /// <see cref="https://stackoverflow.com/a/4319074"/>
    public static System.Collections.Generic.IEnumerable<TSource[]> PermuteAlgorithmL<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, System.Collections.Generic.IComparer<TSource>? comparer = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      comparer ??= System.Collections.Generic.Comparer<TSource>.Default;

      return Permute(source.ToList());

      System.Collections.Generic.IEnumerable<TSource[]> Permute(System.Collections.Generic.IList<TSource> list)
      {
        var length = list.Count;

        var initialOrder = new int[length]; // Figure out where we are in the sequence of all permutations
        for (var i = 0; i < length; i++)
          initialOrder[i] = i;

        System.Array.Sort(initialOrder, (x, y) => comparer.Compare(list[x], list[y]));

        var transform = new System.ValueTuple<int, int>[length];
        for (var i = 0; i < length; i++)
          transform[i] = (initialOrder[i], i);

        for (var i = 1; i < length; i++) // Handle duplicates
          if (comparer.Compare(list[transform[i - 1].Item2], list[transform[i].Item2]) == 0)
            transform[i].Item1 = transform[i - 1].Item1;

        while (true)
        {
          yield return ApplyTransform(list, transform);

          //Ref: E. W. Dijkstra, A Discipline of Programming, Prentice-Hall, 1997
          var decreasingPart = length - 2;
          while (decreasingPart >= 0 && transform[decreasingPart].Item1 >= transform[decreasingPart + 1].Item1) //Find the largest partition from the back that is in decreasing (non-increasing) order.
            decreasingPart--;

          if (decreasingPart < 0) // The whole sequence is in decreasing order, finished.
            yield break;

          var greater = length - 1;
          while (greater > decreasingPart && transform[decreasingPart].Item1 >= transform[greater].Item1) // Find the smallest element in the decreasing partition that is greater than (or equal to) the item in front of the decreasing partition.
            greater--;

          (transform[greater], transform[decreasingPart]) = (transform[decreasingPart], transform[greater]); // Swap.

          System.Array.Reverse(transform, decreasingPart + 1, length - decreasingPart - 1); // Reverse the decreasing partition.
        }

        static TSource[] ApplyTransform(System.Collections.Generic.IList<TSource> items, System.ValueTuple<int, int>[] transform)
        {
          var array = new TSource[transform.Length];
          for (var i = 0; i < transform.Length; i++)
            array[i] = items[transform[i].Item2];
          return array;
        }
      }
    }
  }
}
