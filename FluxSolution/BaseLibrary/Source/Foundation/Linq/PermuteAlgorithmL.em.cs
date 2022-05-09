namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Generates all possible permutations of the elements in the sequence.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Permutation"/>
    /// <see cref="https://stackoverflow.com/a/4319074"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> PermuteAlgorithmL<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IComparer<T> comparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      return Permute(source.ToList());

      System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> Permute(System.Collections.Generic.IList<T> list)
      {
        var sourceCount = list.Count;

        var transform = new System.ValueTuple<int, int>[sourceCount];

        if (comparer is null) // No comparer. Start with an identity transform.
        {
          for (var i = 0; i < sourceCount; i++)
            transform[i] = new System.ValueTuple<int, int>(i, i);
        }
        else
        {
          var initialOrder = new int[sourceCount]; // Figure out where we are in the sequence of all permutations

          for (int i = 0; i < sourceCount; i++)
            initialOrder[i] = i;

          System.Array.Sort(initialOrder, (x, y) => comparer.Compare(list[x], list[y]));

          for (int i = 0; i < sourceCount; i++)
            transform[i] = new System.ValueTuple<int, int>(initialOrder[i], i);

          for (var i = 1; i < sourceCount; i++) // Handle duplicates
            if (comparer.Compare(list[transform[i - 1].Item2], list[transform[i].Item2]) == 0)
              transform[i].Item1 = transform[i - 1].Item1;
        }

        while (true)
        {
          yield return ApplyTransform(list, transform);

          //Ref: E. W. Dijkstra, A Discipline of Programming, Prentice-Hall, 1997
          var decreasingpart = sourceCount - 2;
          while (decreasingpart >= 0 && transform[decreasingpart].Item1 >= transform[decreasingpart + 1].Item1) //Find the largest partition from the back that is in decreasing (non-increasing) order.
            --decreasingpart;

          if (decreasingpart < 0) // The whole sequence is in decreasing order, finished.
            yield break;

          var greater = sourceCount - 1;
          while (greater > decreasingpart && transform[decreasingpart].Item1 >= transform[greater].Item1) // Find the smallest element in the decreasing partition that is greater than (or equal to) the item in front of the decreasing partition.
            greater--;

          (transform[greater], transform[decreasingpart]) = (transform[decreasingpart], transform[greater]); // Swap.

          System.Array.Reverse(transform, decreasingpart + 1, sourceCount - decreasingpart - 1); // Reverse the decreasing partition.
        }

        static System.Collections.Generic.IEnumerable<T> ApplyTransform(System.Collections.Generic.IList<T> items, System.ValueTuple<int, int>[] transform)
        {
          var transformLength = transform.Length;
          for (var i = 0; i < transformLength; i++)
            yield return items[transform[i].Item2];
        }
      }
    }
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> PermuteAlgorithmL<T>(this System.Collections.Generic.IEnumerable<T> source)
      => PermuteAlgorithmL(source, System.Collections.Generic.Comparer<T>.Default);
  }
}
