using System.Linq;

namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>
    /// <para>Creates a new sequence of all possible permutations, in order, of the elements in <paramref name="source"/>. Uses the specified <paramref name="comparer"/>.</para>
    /// <see href="https://en.wikipedia.org/wiki/Permutation"/>
    /// <see href="https://stackoverflow.com/a/4319074"/>
    /// </summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<TSource[]> PermuteAlgorithmL<TSource>(this TSource[] source, System.Collections.Generic.IComparer<TSource>? comparer = null)
    {
      comparer ??= System.Collections.Generic.Comparer<TSource>.Default;

      var initialOrder = new int[source.Length]; // Figure out where we are in the sequence of all permutations
      for (var i = 0; i < source.Length; i++)
        initialOrder[i] = i;

      System.Array.Sort(initialOrder, (x, y) => comparer.Compare(source[x], source[y]));

      var transform = new System.ValueTuple<int, int>[source.Length];
      for (var i = 0; i < source.Length; i++)
        transform[i] = (initialOrder[i], i);

      for (var i = 1; i < source.Length; i++) // Handle duplicates
        if (comparer.Compare(source[transform[i - 1].Item2], source[transform[i].Item2]) == 0)
          transform[i].Item1 = transform[i - 1].Item1;

      while (true)
      {
        yield return ApplyTransform(source, transform);

        //Ref: E. W. Dijkstra, A Discipline of Programming, Prentice-Hall, 1997
        var decreasingPart = source.Length - 2;
        while (decreasingPart >= 0 && transform[decreasingPart].Item1 >= transform[decreasingPart + 1].Item1) //Find the largest partition from the back that is in decreasing (non-increasing) order.
          decreasingPart--;

        if (decreasingPart < 0) // The whole sequence is in decreasing order, finished.
          yield break;

        var greater = source.Length - 1;
        while (greater > decreasingPart && transform[decreasingPart].Item1 >= transform[greater].Item1) // Find the smallest element in the decreasing partition that is greater than (or equal to) the item in front of the decreasing partition.
          greater--;

        (transform[greater], transform[decreasingPart]) = (transform[decreasingPart], transform[greater]); // Swap.

        System.Array.Reverse(transform, decreasingPart + 1, source.Length - decreasingPart - 1); // Reverse the decreasing partition.
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
