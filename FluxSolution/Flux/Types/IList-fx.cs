namespace Flux
{
  public static partial class XtensionIList
  {
    /// <summary>Creates a new sequence of all pair indices for which values when added equals the specified sum.</summary>
    public static System.Collections.Generic.IEnumerable<(TSelf, TSelf)> GetPairsEqualToSum<TSelf>(this System.Collections.Generic.IList<TSelf> source, TSelf sum)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      for (var i = 0; i < source.Count; i++)
      {
        var si = source[i];

        for (var j = i + 1; j < source.Count; j++)
        {
          var ti = source[i];

          if (si + ti == sum)
            yield return (si, ti);
        }
      }
    }

    /// <summary>
    /// <para>Creates a new sequence of <paramref name="count"/> elements taken from <paramref name="source"/>. Uses the specified <paramref name="rng"/> (shared if null).</para>
    /// <remarks>This function may return duplicates from the <see cref="System.Collections.Generic.IList{T}"/>. Use <see cref="GetRandomElements{T}(IEnumerable{T}, double, System.Random?, int)"/> for unique/distinct elements.</remarks>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="count"></param>
    /// <param name="rng"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<T> GetRandomCount<T>(this System.Collections.Generic.IList<T> source, int count, System.Random? rng = null)
    {
      while (--count >= 0)
        yield return source.Random(rng);
    }

    /// <summary>
    /// <para>Compute the <paramref name="median"/> of all elements in <paramref name="source"/>. This version buffers the sequence in <paramref name="sortedList"/>, to avoid multiple passes, available as an output parameter.</para>
    /// <para><see href="http://en.wikipedia.org/wiki/Median"/></para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source"></param>
    /// <param name="median"></param>
    /// <param name="sortedList"></param>
    public static void Median<TSelf, TResult>(this System.Collections.Generic.IList<TSelf> source, out TResult median)
      where TSelf : System.Numerics.INumber<TSelf>
      where TResult : System.Numerics.IFloatingPoint<TResult>
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var (quotient, remainder) = int.DivRem(source.Count, 2);

      median = (remainder == 0)
        ? TResult.CreateChecked(source[quotient - 1] + source[quotient]) / TResult.CreateChecked(2) // Even count = ((previous + current) / 2).
        : TResult.CreateChecked(source[quotient]); // Odd count = current value only.
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

    /// <summary>In-place swap of two elements by the specified indices.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    /// <exception cref="System.ArgumentOutOfRangeException"/>
    public static void Swap<T>(this System.Collections.Generic.IList<T> source, int indexA, int indexB)
    {
      if (indexA != indexB) // No need to do anything, if the indices are the same.
      {
        System.ArgumentNullException.ThrowIfNull(source);

        (source[indexB], source[indexA]) = (source[indexA], source[indexB]);
      }
    }
  }
}
