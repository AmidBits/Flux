namespace Flux
{
  public static partial class IListExtensions
  {
    extension<T>(System.Collections.Generic.IList<T> source)
    {
      #region Swap (also for List<>, etc.)

      /// <summary>
      /// <para>In-place swap of two elements by the specified indices.</para>
      /// </summary>
      /// <exception cref="System.ArgumentNullException"/>
      public void Swap(int indexA, int indexB)
      {
        if (indexA != indexB) // No need to do anything, if the indices are the same.
        {
          System.ArgumentNullException.ThrowIfNull(source);

          (source[indexB], source[indexA]) = (source[indexA], source[indexB]);
        }
      }

      #endregion
    }

    extension<T>(System.Collections.Generic.IList<T> source)
      where T : System.Numerics.INumber<T>
    {
      #region GetPairsEqualToSum

      /// <summary>
      /// <para>Creates a new sequence of all pair indices for which values when added equals the specified sum.</para>
      /// </summary>
      public System.Collections.Generic.List<(T, T)> GetPairsEqualToSum(T sum)
      {
        var pairs = new System.Collections.Generic.List<(T, T)>();

        for (var i = 0; i < source.Count; i++)
        {
          var si = source[i];

          for (var j = i + 1; j < source.Count; j++)
          {
            var ti = source[i];

            if (si + ti == sum)
              pairs.Add((si, ti));
          }
        }

        return pairs;
      }

      #endregion

      #region Median

      /// <summary>
      /// <para>Compute the <paramref name="median"/> of all elements in <paramref name="source"/>. This version buffers the sequence in <paramref name="sortedList"/>, to avoid multiple passes, available as an output parameter.</para>
      /// <para><see href="http://en.wikipedia.org/wiki/Median"/></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <typeparam name="TResult"></typeparam>
      /// <param name="source"></param>
      /// <param name="median"></param>
      /// <param name="sortedList"></param>
      public void Median<TResult>(out TResult median)
        where TResult : System.Numerics.IFloatingPoint<TResult>
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var (quotient, remainder) = int.DivRem(source.Count, 2);

        median = (remainder == 0)
          ? TResult.CreateChecked(source[quotient - 1] + source[quotient]) / TResult.CreateChecked(2) // Even count = ((previous + current) / 2).
          : TResult.CreateChecked(source[quotient]); // Odd count = current value only.
      }

      #endregion
    }

    ///// <summary>Creates a new sequence, a set of all subsets (as lists) of the source set, including the empty set and the source itself.</summary>
    ///// <exception cref="System.ArgumentNullException"/>
    ///// <see href="https://en.wikipedia.org/wiki/Power_set"/>
    //public static System.Collections.Generic.IEnumerable<T[]> PowerSet<T>(this System.Collections.Generic.IList<T> source)
    //{
    //  var powerCount = (int)System.Numerics.BigInteger.Pow(2, source.Count);

    //  var subsetList = new System.Collections.Generic.List<T>(source.Count);

    //  for (var o = 0; o < powerCount; o++)
    //  {
    //    subsetList.Clear();

    //    for (var i = 0; i < powerCount; i++)
    //      if ((o & (1L << i)) > 0)
    //        subsetList.Add(source[i]);

    //    yield return subsetList.ToArray();
    //  }
    //}

    //public static void Shuffle<T>(this System.Collections.Generic.IList<T> source, System.Random? rng = null)
    //{
    //  System.ArgumentNullException.ThrowIfNull(source);

    //  rng ??= System.Random.Shared;

    //  for (var i = source.Count - 1; i > 0; i--)
    //    source.Swap(i, rng.Next(i + 1));
    //}

    //public static int InsertSorted<T>(this System.Collections.Generic.IList<T> source, T item)
    //{
    //  var insertIndex = FindInsertIndex(source, item, 0, int.Max(0, source.Count - 1), null);
    //  source.Insert(insertIndex, item);
    //  return insertIndex;
    //}


    //public static int SortAt<T>(this System.Collections.Generic.IList<T> source, int index)
    //{
    //  var item = source[index];
    //  source.RemoveAt(index);

    //  var insertIndex = FindInsertIndex(source, item, 0, int.Max(0, source.Count - 1), null);
    //  source.Insert(insertIndex, item);
    //  return insertIndex;
    //}

    //public static int FindInsertIndex<T>(this System.Collections.Generic.IList<T> source, T item, int begin, int end, System.Collections.Generic.IComparer<T>? comparer = null)
    //{
    //  if (begin == end)
    //    return begin;

    //  comparer ??= System.Collections.Generic.Comparer<T>.Default;

    //  var middle = begin + (end - begin) / 2;

    //  var cmp = comparer.Compare(item, source[middle]);

    //  return cmp switch
    //  {
    //    < 0 => FindInsertIndex(source, item, begin, middle, comparer),
    //    > 0 => FindInsertIndex(source, item, middle, end, comparer),
    //    _ => middle
    //  };
    //}
  }
}
