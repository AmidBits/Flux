using Flux.Model;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Flux
{
  // https://stackoverflow.com/a/4319074
  public static partial class Xtensions
  {
    /// <summary>
    /// Generates permutations.
    /// </summary>
    /// <typeparam name="T">Type of items to permute.</typeparam>
    /// <param name="items">Array of items. Will not be modified.</param>
    /// <param name="comparer">Optional comparer to use.
    /// If a <paramref name="comparer"/> is supplied, 
    /// permutations will be ordered according to the 
    /// <paramref name="comparer"/>
    /// </param>
    /// <returns>Permutations of input items.</returns>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> Permute<T>(System.Collections.Generic.IList<T> items, System.Collections.Generic.IComparer<T> comparer)
    {
      var length = items.Count;

      var transform = new IntPair[length];

      if (comparer == null)
      {
        //No comparer. Start with an identity transform.
        for (var i = 0; i < length; i++)
        {
          System.ValueTuple.Create(i, i);
          transform[i] = new IntPair(i, i);
        }
      }
      else
      {
        //Figure out where we are in the sequence of all permutations
        var initialorder = new int[length];

        for (int i = 0; i < length; i++)
          initialorder[i] = i;

        System.Array.Sort(initialorder, delegate (int x, int y)
        {
          return comparer.Compare(items[x], items[y]);
        });

        for (int i = 0; i < length; i++)
          transform[i] = new IntPair(initialorder[i], i);

        //Handle duplicates
        for (int i = 1; i < length; i++)
          if (comparer.Compare(items[transform[i - 1].Second], items[transform[i].Second]) == 0)
            transform[i].First = transform[i - 1].First;
      }

      yield return ApplyTransform(items, transform);

      while (true)
      {
        //Ref: E. W. Dijkstra, A Discipline of Programming, Prentice-Hall, 1997
        //Find the largest partition from the back that is in decreasing (non-icreasing) order
        int decreasingpart = length - 2;
        for (; decreasingpart >= 0 && transform[decreasingpart].First >= transform[decreasingpart + 1].First; --decreasingpart) ;
        //The whole sequence is in decreasing order, finished
        if (decreasingpart < 0) 
          yield break;
        //Find the smallest element in the decreasing partition that is 
        //greater than (or equal to) the item in front of the decreasing partition
        int greater = length - 1;
        while ( greater > decreasingpart && transform[decreasingpart].First >= transform[greater].First) 
          greater--;
        //Swap the two
        Swap(ref transform[decreasingpart], ref transform[greater]);
        //Reverse the decreasing partition
        System.Array.Reverse(transform, decreasingpart + 1, length - decreasingpart - 1);

        yield return ApplyTransform(items, transform);
      }
    }

    #region Overloads

    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> Permute<T>(System.Collections.Generic.IList<T> items)
      => Permute(items, System.Collections.Generic.Comparer<T>.Default);

    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> Permute<T>(this System.Collections.Generic.IEnumerable<T> items, System.Collections.Generic.IComparer<T> comparer)
    {
      System.Collections.Generic.List<T> list = new System.Collections.Generic.List<T>(items);

      return Permute(list.ToList(), comparer);
    }

    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> Permute<T>(this System.Collections.Generic.IEnumerable<T> items)
      => Permute(items.ToList(), System.Collections.Generic.Comparer<T>.Default);

    #endregion Overloads

    #region Utility

    public static System.Collections.Generic.IEnumerable<T> ApplyTransform<T>(System.Collections.Generic.IList<T> items, IntPair[] transform)
    {
      for (int i = 0; i < transform.Length; i++)
      {
        yield return items[transform[i].Second];
      }
    }

    public static void Swap<T>(ref T x, ref T y)
    {
      T tmp = x;
      x = y;
      y = tmp;
    }

    public struct IntPair
    {
      public IntPair(int first, int second)
      {
        this.First = first;
        this.Second = second;
      }
      public int First;
      public int Second;
    }

    #endregion
    //private static bool NextCombination(System.Collections.Generic.IList<int> num, int n, int k)
    //{
    //  var isFinished = false;
    //  var isChanged = false;

    //  if (k <= 0) 
    //    return false;

    //  for (var i = k - 1; !isFinished && !isChanged; i--)
    //  {
    //    if (num[i] < n - 1 - (k - 1) + i)
    //    {
    //      num[i]++;

    //      if (i < k - 1)
    //        for (var j = i + 1; j < k; j++)
    //          num[j] = num[j - 1] + 1;
    //      isChanged = true;
    //    }

    //    isFinished = i == 0;
    //  }

    //  return isChanged;
    //}



    //public static System.Collections.Generic.IEnumerable<string> Permutate(this string source)
    //{
    //  return
    //      source
    //          .AsEnumerable() // <-- not necessary, string is already IEnumerable<char>
    //          .Permutate()
    //          .Select(a => new string(a));
    //}

    //public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<T>> Permute<T>(this System.Collections.Generic.IEnumerable<T> source)
    //{
    //  return PermuteImpl(source, System.Linq.Enumerable.Empty<T>());

    //  System.Collections.Generic.IEnumerable<System.Collections.Generic.List<T>> PermuteImpl(System.Collections.Generic.IEnumerable<T> reminder, System.Collections.Generic.IEnumerable<T> prefix)
    //  {
    //    if (reminder.Any())
    //    {
    //        foreach (var t in reminder.Select((r, i) => (r, i)))
    //        {
    //          var nextReminder = reminder.Take(t.i).Concat(reminder.Skip(t.i + 1)).ToList();
    //          var nextPrefix = prefix.Append(t.r);

    //          foreach (var permutation in PermuteImpl(nextReminder, nextPrefix))
    //          {
    //            yield return permutation;
    //          }
    //        }

    //    }
    //    else
    //    {
    //      yield return  prefix.ToList();
    //    }
    //  }
    //}
    //public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> Permute<T>(this System.Collections.Generic.IEnumerable<T> values) 
    //  => values.SelectMany(x => Permute(new[] { new[] { x } }, values, values.Count() - 1));
    //public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> Permute<T>(this System.Collections.Generic.IEnumerable<T> values, int permutations) 
    //  => values.SelectMany(x => Permute(new[] { new[] { x } }, values, permutations - 1));
    //private static System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> Permute<T>(System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> current, System.Collections.Generic.IEnumerable<T> values, int count) 
    //  => (count == 1) ? Permute(current, values) : Permute(Permute(current, values), values, --count);
    //private static System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> Permute<T>(System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> current, System.Collections.Generic.IEnumerable<T> values) 
    //  => current.SelectMany(x => values.Select(y => x.Concat(new[] { y })));

    ///// <summary>
    ///// 
    ///// </summary>
    //public static System.Collections.Generic.IEnumerable<TSource> Prepend<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, TSource item)
    //{
    //  if (source == null) throw new System.ArgumentNullException(nameof(source));

    //  yield return item;

    //  foreach (var element in source)
    //    yield return element;
    //}

    //public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<TSource>> Permutate<TSource>(this System.Collections.Generic.IEnumerable<TSource> source)
    //{
    //  if (source == null) throw new System.ArgumentNullException(nameof(source));

    //  var list = source.ToList();

    //  if (list.Count > 1)
    //    return from s in list
    //           from p in Permutate(list.Take(list.IndexOf(s)).Concat(list.Skip(list.IndexOf(s) + 1)))
    //           select p.Prepend(s);

    //  return new[] { list };
    //}

    //public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<TSource>> Combinate<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, int k)
    //{
    //  if (source == null) throw new System.ArgumentNullException(nameof(source));

    //  var list = source.ToList();

    //  if (k <= 0 || k > list.Count) throw new System.ArgumentOutOfRangeException(nameof(k));

    //  if (k == 0) yield return System.Linq.Enumerable.Empty<TSource>();

    //  foreach (var l in list)
    //    foreach (var c in Combinate(list.Skip(list.Count - k - 2), k - 1))
    //      yield return c.Prepend(l);
    //}
  }
}
