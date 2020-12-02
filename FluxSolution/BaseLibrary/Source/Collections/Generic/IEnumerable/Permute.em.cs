namespace Flux
{
  public static partial class IEnumerableEm
  {
    /// <summary>Generates all possible permutations of the elements in the sequence.</summary>
    /// <see cref="https://stackoverflow.com/a/4319074"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> Permute<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IComparer<T>? comparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var sourceLength = source.Count;

      var transform = new System.ValueTuple<int, int>[sourceLength];

      if (comparer is null) // No comparer. Start with an identity transform.
      {
        for (var i = 0; i < sourceLength; i++)
          transform[i] = new System.ValueTuple<int, int>(i, i);
      }
      else
      {
        var initialOrder = new int[sourceLength]; //Figure out where we are in the sequence of all permutations

        for (int i = 0; i < sourceLength; i++)
          initialOrder[i] = i;

        System.Array.Sort(initialOrder, (x, y) => comparer.Compare(source[x], source[y]));

        for (int i = 0; i < sourceLength; i++)
          transform[i] = new System.ValueTuple<int, int>(initialOrder[i], i);

        for (var i = 1; i < sourceLength; i++) // Handle duplicates
          if (comparer.Compare(source[transform[i - 1].Item2], source[transform[i].Item2]) == 0)
            transform[i].Item1 = transform[i - 1].Item1;
      }

      yield return ApplyTransform(source, transform);

      while (true)
      {
        //Ref: E. W. Dijkstra, A Discipline of Programming, Prentice-Hall, 1997
        var decreasingpart = sourceLength - 2;
        while (decreasingpart >= 0 && transform[decreasingpart].Item1 >= transform[decreasingpart + 1].Item1) //Find the largest partition from the back that is in decreasing (non-increasing) order.
          --decreasingpart;

        if (decreasingpart < 0) // The whole sequence is in decreasing order, finished.
          yield break;

        var greater = sourceLength - 1;
        while (greater > decreasingpart && transform[decreasingpart].Item1 >= transform[greater].Item1) // Find the smallest element in the decreasing partition that is greater than (or equal to) the item in front of the decreasing partition.
          greater--;

        var tmp = transform[decreasingpart];
        transform[decreasingpart] = transform[greater];
        transform[greater] = tmp;

        System.Array.Reverse(transform, decreasingpart + 1, sourceLength - decreasingpart - 1); // Reverse the decreasing partition.

        yield return ApplyTransform(source, transform);
      }

      static System.Collections.Generic.IEnumerable<T> ApplyTransform(System.Collections.Generic.IList<T> items, System.ValueTuple<int, int>[] transform)
      {
        for (int i = 0; i < transform.Length; i++)
          yield return items[transform[i].Item2];
      }
    }
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> Permute<T>(this System.Collections.Generic.IList<T> items)
      => Permute(items, System.Collections.Generic.Comparer<T>.Default);
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> Permute<T>(this System.Collections.Generic.IEnumerable<T> items, System.Collections.Generic.IComparer<T> comparer)
      => Permute(new System.Collections.Generic.List<T>(items), comparer);
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> Permute<T>(this System.Collections.Generic.IEnumerable<T> items)
      => Permute(new System.Collections.Generic.List<T>(items), System.Collections.Generic.Comparer<T>.Default);
  }
}
