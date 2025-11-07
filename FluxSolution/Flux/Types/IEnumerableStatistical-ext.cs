namespace Flux
{
  public static partial class IEnumerableStatistical
  {
    //    /// <summary>Given an array of positive integers. All numbers occur even number of times except one number which occurs odd number of times. Find the number in O(n) time & constant space.</summary>
    //    public static int GetOddOccurrence(System.Collections.Generic.IList<int> source)
    //    {
    //      if (source is null) throw new System.ArgumentNullException(nameof(source));

    //      var sourceCount = source.Count;

    //      for (var i = 0; i < sourceCount; i++)
    //      {
    //        var sourceValue = source[i];

    //        var count = 0;

    //        for (var j = 0; j < sourceCount; j++)
    //          if (sourceValue == source[j])
    //            count++;

    //        if (count % 2 != 0)
    //          return sourceValue;
    //      }

    //      return -1;
    //    }

    //    public static System.Collections.Generic.IEnumerable<int> SequenceFindMissings(System.Collections.Generic.IEnumerable<int> sequence)
    //      => sequence.Zip(sequence.Skip(1), (a, b) => System.Linq.Enumerable.Range(a + 1, (b - a) - 1)).SelectMany(s => s);

    //    public static bool IsSequenceBroken(System.Collections.Generic.IEnumerable<int> sequence)
    //      => sequence.Zip(sequence.Skip(1), (a, b) => b - a).Any(gap => gap != 1);

    //    public static System.Collections.Generic.IEnumerable<int> FindMissingIntegers(System.Collections.Generic.IEnumerable<int> source)
    //    {
    //      if (source is null) throw new System.ArgumentNullException(nameof(source));

    //      return source.Zip(source.Skip(1), (a, b) => System.Linq.Enumerable.Range(a + 1, (b - a) - 1)).SelectMany(s => s);
    //    }
    public static System.Collections.Generic.IEnumerable<(TInteger Number, TInteger Count)> GetMissingRanges<TInteger>(this System.Collections.Generic.IEnumerable<TInteger> source)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.PartitionTuple2(false, (a, b, i) => (Number: a + TInteger.One, Count: (b - a) - TInteger.One)).Where(t2 => t2.Count > TInteger.One);
    }

    extension<TNumber>(System.Collections.Generic.IEnumerable<TNumber> source)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      /// <summary>
      /// <para>Compute the <paramref name="mean"/> of all elements in <paramref name="source"/>, also return the <paramref name="count"/> and the <paramref name="sum"/> of elements as output parameters.</para>
      /// <para><see href="http://en.wikipedia.org/wiki/Mean"/></para>
      /// </summary>
      /// <typeparam name="TNumberBase"></typeparam>
      /// <typeparam name="TResult"></typeparam>
      /// <param name="source"></param>
      /// <param name="mean"></param>
      /// <param name="sum"></param>
      /// <param name="count"></param>
      public double Mean(out TNumber sum, out int count)
      {
        sum = TNumber.Zero;
        count = 0;

        foreach (var item in source)
        {
          sum += item;
          count++;
        }

        return count > 0 ? double.CreateChecked(sum) / count : 0;
      }

      /// <summary>
      /// <para>Compute the mean, median and mode of all elements in <paramref name="source"/>. This version uses a <see cref="Statistics.OnlineMeanMedianMode{TSelf}"/> with double heaps and a histogram.</para>
      /// <para><see href="http://en.wikipedia.org/wiki/Median"/></para>
      /// </summary>
      /// <typeparam name="TSelf"></typeparam>
      /// <param name="source"></param>
      public (double Mean, double Median, System.Collections.Generic.KeyValuePair<TNumber, int> Mode) MeanMedianMode()
      {
        var mmo = new Statistics.OnlineMeanMedianMode<TNumber>(source);

        return (mmo.Mean, mmo.Median, mmo.Mode);
      }

      /// <summary>
      /// <para>Compute the product of all numbers in <paramref name="source"/>.</para>
      /// </summary>
      /// <typeparam name="TSelf"></typeparam>
      /// <param name="source"></param>
      /// <returns></returns>
      public TNumber Product()
        => source.Aggregate(TNumber.Zero, (a, e) => a * e);

      public TNumber Product(System.Func<TNumber, TNumber> selector)
        => source.Aggregate(TNumber.Zero, (a, e) => a * selector(e));

      /// <summary>
      /// <para>Compute the sum of all numbers in <paramref name="source"/>.</para>
      /// </summary>
      /// <typeparam name="TNumberBase"></typeparam>
      /// <param name="source"></param>
      /// <returns></returns>
      public TNumber Sum()
        => source.Aggregate(TNumber.Zero, (a, e) => a + e);

      public TNumber Sum(System.Func<TNumber, TNumber> selector)
        => source.Aggregate(TNumber.Zero, (a, e) => a + selector(e));
    }

    /// <summary>
    /// <para>Creates a new sequence consisting of 'moded' (i.e. sorted by the most frequent or common) elements in <paramref name="source"/>.</para>
    /// <para><see href="http://en.wikipedia.org/wiki/Mode"/></para>
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.Linq.IOrderedEnumerable<System.Collections.Generic.KeyValuePair<TSource, int>> Mode<TSource>(this System.Collections.Generic.IEnumerable<TSource> source)
      => source.GroupBy(t => t).Select(g => new System.Collections.Generic.KeyValuePair<TSource, int>(g.Key, g.Count())).OrderByDescending(kvp => kvp.Value);

    #region PearsonCorrelationCoefficient

    /// <summary>Pearson correlation coefficient (PCC) is a measure of linear correlation between two sets of data. It is the ratio between the covariance of two variables and the product of their standard deviations. Thus, it is essentially a normalized measurement of the covariance, such that the result always has a value between -1 and 1.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    /// <see href="https://en.wikipedia.org/wiki/Pearson_correlation_coefficient"/>
    public static (TNumber correlation, TNumber covariance) PearsonCorrelationCoefficient<TValueX, TValueY, TNumber>(this System.Collections.Generic.IEnumerable<TValueX> setX, System.Func<TValueX, TNumber> valueSelectorX, System.Collections.Generic.IEnumerable<TValueY> setY, System.Func<TValueY, TNumber> valueSelectorY)
      where TNumber : System.Numerics.INumber<TNumber>, System.Numerics.IRootFunctions<TNumber>
    {
      var ex = setX.ThrowOnNullOrEmpty().GetEnumerator();
      var ey = setY.ThrowOnNullOrEmpty().GetEnumerator();

      var sumX = TNumber.Zero;
      var sumX2 = TNumber.Zero;
      var sumY = TNumber.Zero;
      var sumY2 = TNumber.Zero;
      var sumXY = TNumber.Zero;
      var count = TNumber.Zero;

      while (ex.MoveNext() && ey.MoveNext())
      {
        var vx = valueSelectorX(ex.Current);
        var vy = valueSelectorY(ey.Current);

        sumX += vx;
        sumX2 += vx * vx;
        sumY += vy;
        sumY2 += vy * vy;
        sumXY += vx * vy;
        count++;
      }

      var countP2 = count * count;

      var stdX = TNumber.Sqrt(sumX2 / count - sumX * sumX / countP2);
      var stdY = TNumber.Sqrt(sumY2 / count - sumY * sumY / countP2);

      var covariance = (sumXY / count - sumX * sumY / countP2);

      return (covariance / stdX / stdY, covariance);
    }

    #endregion
  }
}
