namespace Flux
{
  public static partial class IEnumerableExtensions
  {
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

    #region ToDataTable

    ///// <summary>Create a new data table from the specified sequence.</summary>
    ///// <param name="source">The source sequence.</param>
    ///// <param name="tableName">The name of the data table.</param>
    ///// <param name="namesSelector">The column names selector to use.</param>
    ///// <param name="typesSelector">The column types selector to use.</param>
    ///// <param name="valuesSelector">A array selector used to extract the data for each row in the data table.</param>
    ///// <exception cref="System.ArgumentNullException"/>
    public static System.Data.DataTable ToDataTable(this System.Collections.Generic.IEnumerable<object[]> source, bool hasFieldNames = false, bool adoptFieldTypes = false, string? tableName = null)
    {
      var dt = new System.Data.DataTable(tableName);

      using var e = source.ThrowOnNull().GetEnumerator();

      if (e.MoveNext() is var movedNext && movedNext)
      {
        var columnNames = e.Current.Length.ToMultipleOrdinalColumnNames();

        if (hasFieldNames) // If has-field-names let's use those for columnNames.
          columnNames = e.Current.Select((e, i) => e?.ToString() ?? columnNames[i]).ToArray();

        var columnTypes = columnNames.Select(cn => typeof(object)).ToArray(); // Default to System.Object for columnTypes.

        if (hasFieldNames) // The first row has field-names..
        {
          movedNext = e.MoveNext(); // ..which may be of other types than the data, so let's move to the data.

          if (movedNext && adoptFieldTypes)
            columnTypes = columnNames.Select((cn, i) => e.Current[i].GetType()).ToArray();
        }

        for (var columnIndex = 0; columnIndex < columnNames.Length; columnIndex++)
          dt.Columns.Add(columnNames[columnIndex].ToString(), columnTypes[columnIndex]);

        if (movedNext)
          do
          {
            dt.Rows.Add(e.Current);
          }
          while (e.MoveNext());
      }

      return dt;
    }

    #endregion

    #region ToSortedDictionary

    /// <summary>
    /// <para>Creates a new <see cref="SortedDictionary{TKey, TValue}"/> with all key-value-pairs from <paramref name="source"/>.</para>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="source"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static System.Collections.Generic.SortedDictionary<TKey, TValue> ToSortedDictionary<TKey, TValue>(this System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> source, System.Collections.Generic.IComparer<TKey>? comparer = null)
      where TKey : notnull
      => source.ToSortedDictionary(e => e.Key, e => e.Value, comparer);

    #endregion

    #region Flatten/Unflatten (KeyValuePairs)

    /// <summary>Flattens a sequence of objects into a sequence of key/value pairs based on the specified keySelector and valuesSelector.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> Flatten<TSource, TKey, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Func<TSource, System.Collections.Generic.IEnumerable<TValue>> valuesSelector)
      => source.SelectMany(e => valuesSelector(e).Select(v => new System.Collections.Generic.KeyValuePair<TKey, TValue>(keySelector(e), v)));

    /// <summary>Unflattens a sequence of objects into a sequence of based on the specified keySelector and valuesSelector.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, System.Collections.Generic.List<TValue>>> Unflatten<TSource, TKey, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Func<TSource, TValue> valueSelector)
      where TKey : System.IEquatable<TKey>
    {
      var list = source.ToList();

      return list.Select(t => keySelector(t)).Distinct().Select(k => new System.Collections.Generic.KeyValuePair<TKey, System.Collections.Generic.List<TValue>>(k, list.Where(t => keySelector(t).Equals(k)).Select(t => valueSelector(t)).ToList()));
    }

    #endregion

    extension<TInteger>(System.Collections.Generic.IEnumerable<TInteger> source)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      #region GetMissingRanges

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
      public System.Collections.Generic.IEnumerable<(TInteger Number, TInteger Count)> GetMissingRanges()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        return source.Order().PartitionTuple2(false, (a, b, i) => (Number: a + TInteger.One, Count: (b - a) - TInteger.One)).Where(t2 => t2.Count > TInteger.One);
      }

      #endregion

      #region ToPrimeFactorString

      /// <summary>
      /// <para>Creates a new string with all <paramref name="source"/> each as a single value or with an exponent if multiple.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="source"></param>
      /// <returns></returns>
      public string ToPrimeFactorString()
        => string.Join('\u00d7', source.Order().GroupAdjacent(pf => pf).Select(g => $"{g.Key}{g.Count().ToSuperscriptString(10)}"));

      #endregion
    }

    extension<TNumber>(System.Collections.Generic.IEnumerable<TNumber> source)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      #region Mean

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

      #endregion

      #region MeanMedianMode

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

      #endregion

      #region Product

      /// <summary>
      /// <para>Compute the product of all numbers in a sequence.</para>
      /// </summary>
      /// <param name="count"></param>
      /// <returns></returns>
      public TNumber Product(out int count)
      {
        using var e = source.GetEnumerator();

        var product = TNumber.Zero;

        count = 0;

        if (e.MoveNext())
        {
          product = e.Current;

          count++;

          while (e.MoveNext())
          {
            product = checked(product * e.Current);

            count++;
          }
        }

        return product;
      }

      public TNumber Product()
        => Product(source, out var _);

      #endregion

      #region Sum

      public TNumber Sum()
        => source.Aggregate(TNumber.Zero, (a, n) => checked(a + n));

      /// <summary>
      /// <para>Compute the sum of all numbers in <paramref name="source"/>.</para>
      /// </summary>
      /// <typeparam name="TNumberBase"></typeparam>
      /// <param name="source"></param>
      /// <returns></returns>
      public TNumber Sum(out int count)
      {
        var sum = TNumber.Zero;

        count = 0;

        foreach (var n in source)
        {
          sum = checked(sum + n);

          count++;
        }

        return sum;
      }

      #endregion
    }

    extension<TSource>(System.Collections.Generic.IEnumerable<TSource> source)
    {
      #region Aggregate

      /// <summary>
      /// <para>Applies an accumulator over a sequence. The specified seed value is used as the inital accumulator value, and the specified functions are used to select the accumulated value and result value, respectively.</para>
      /// </summary>
      /// <remarks>
      /// <para>Unlike the <see cref="System.Linq.Enumerable"/> versions, this one also includes the ordinal index of the element while aggregating.</para>
      /// </remarks>
      /// <typeparam name="TSource"></typeparam>
      /// <typeparam name="TAccumulate"></typeparam>
      /// <typeparam name="TResult"></typeparam>
      /// <param name="source"></param>
      /// <param name="seed"></param>
      /// <param name="func"></param>
      /// <param name="resultSelector"></param>
      /// <returns></returns>
      public TResult Aggregate<TAccumulate, TResult>(TAccumulate seed, System.Func<TAccumulate, TSource, int, TAccumulate> func, System.Func<TAccumulate, int, TResult> resultSelector)
      {
        System.ArgumentNullException.ThrowIfNull(seed);
        System.ArgumentNullException.ThrowIfNull(func);
        System.ArgumentNullException.ThrowIfNull(resultSelector);

        var index = -1;

        var accumulator = seed;

        foreach (var item in source)
          accumulator = func(accumulator, item, ++index);

        return resultSelector(accumulator, index);
      }

      #endregion

      #region AggregateTuple

      //    /// <summary>Creates a sequence of staggered (by one element) n-tuples.</summary>
      //    /// <param name="tupleSize">The number of elements in each tuple.</param>
      //    /// <param name="tupleWrap">The number of staggered wrap-around tuples to return, beyond the last element in the source sequence.</param>
      //    /// <param name="resultSelector">Allows the result of each tuple to be processed.</param>
      //    /// <returns>A sequence of n-tuples staggered by one element, optionally extending the sequence by the specified overflow.</returns>
      //    /// <exception cref="System.ArgumentNullException"/>
      //    /// <see href="https://en.wikipedia.org/wiki/Tuple"/>
      //    public static TResult AggregateTuple<TSource, TAccumulate, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, TAccumulate seed, int tupleSize, int tupleWrap, System.Func<TAccumulate, System.Collections.Generic.IReadOnlyList<TSource>, int, TAccumulate> accumulator, System.Func<TAccumulate, int, TResult> resultSelector)
      //    {
      //      System.ArgumentNullException.ThrowIfNull(accumulator);
      //      System.ArgumentNullException.ThrowIfNull(resultSelector);

      //      if (tupleSize < 2) throw new System.ArgumentOutOfRangeException(nameof(tupleSize));
      //      if (tupleWrap < 0 || tupleWrap >= tupleSize) throw new System.ArgumentException($@"A {tupleSize}-tuple can only wrap up to {tupleSize - 1} elements.");

      //      using var e = source.ThrowOnNull().GetEnumerator();

      //      var start = new System.Collections.Generic.List<TSource>();

      //      if (e.MoveNext())
      //      {
      //        do
      //        {
      //          start.Add(e.Current);
      //        }
      //        while (start.Count < tupleSize && e.MoveNext());

      //        if (start.Count == tupleSize)
      //        {
      //          var store = new System.Collections.Generic.List<TSource>(start);

      //          var index = 0;

      //          var value = accumulator(seed, store, index++);

      //          while (e.MoveNext())
      //          {
      //            store.RemoveAt(0);
      //            store.Add(e.Current);
      //            value = accumulator(value, store, index++);
      //          }

      //          while (tupleWrap-- > 0)
      //          {
      //            store.RemoveAt(0);
      //            store.Add(start[0]);
      //            start.RemoveAt(0);
      //            value = accumulator(value, store, index++);
      //          }

      //          return resultSelector(value, index);
      //        }
      //        else throw new System.ArgumentException($@"The sequence has only {start.Count} elements.", nameof(source));
      //      }
      //      else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
      //    }

      /// <summary>Creates a sequence of staggered (by one element) 2-tuple elements.</summary>
      /// <returns>A sequence of 2-tuples staggered by one element, optionally extending the sequence by the specified number of wraps.</returns>
      /// <exception cref="System.ArgumentException"/>
      /// <exception cref="System.ArgumentNullException"/>
      /// <see href="https://en.wikipedia.org/wiki/Tuple"/>
      public TResult AggregateTuple2<TAccumulate, TResult>(TAccumulate seed, bool tupleWrap, System.Func<TAccumulate, TSource, TSource, int, TAccumulate> aggregateComputor, System.Func<TAccumulate, int, TResult> resultSelector)
      {
        System.ArgumentNullException.ThrowIfNull(aggregateComputor);
        System.ArgumentNullException.ThrowIfNull(resultSelector);

        using var e = source.ThrowOnNull().GetEnumerator();

        if (e.MoveNext() && e.Current is var item1 && item1 is var back1)
          if (e.MoveNext())
          {
            var index = 0;

            do
            {
              var current = e.Current;

              seed = aggregateComputor(seed, back1, current, index++);

              back1 = current;
            }
            while (e.MoveNext());

            if (tupleWrap)
              seed = aggregateComputor(seed, back1, item1, index++);

            return resultSelector(seed, index);
          }
          else throw new System.ArgumentException($@"The sequence has only 1 element.", nameof(source));
        else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
      }

      /// <summary>Creates a sequence of staggered (by one element) 3-tuple elements.</summary>
      /// <returns>A sequence of 3-tuple elements staggered by one element, optionally extending the sequence by the specified number of wraps.</returns>
      /// <exception cref="System.ArgumentException"/>
      /// <exception cref="System.ArgumentNullException"/>
      /// <see href="https://en.wikipedia.org/wiki/Tuple"/>
      public TResult AggregateTuple3<TAccumulate, TResult>(TAccumulate seed, int tupleWrap, System.Func<TAccumulate, TSource, TSource, TSource, int, TAccumulate> aggregateComputor, System.Func<TAccumulate, int, TResult> resultSelector)
      {
        System.ArgumentNullException.ThrowIfNull(aggregateComputor);
        System.ArgumentNullException.ThrowIfNull(resultSelector);

        if (tupleWrap < 0 || tupleWrap > 2) throw new System.ArgumentException(@"A 3-tuple can only wrap 0, 1 or 2 elements.", nameof(tupleWrap));

        using var e = source.ThrowOnNull().GetEnumerator();

        if (e.MoveNext() && e.Current is var item1 && item1 is var back2)
          if (e.MoveNext() && e.Current is var item2 && item2 is var back1)
            if (e.MoveNext())
            {
              var index = 0;

              do
              {
                var current = e.Current;

                seed = aggregateComputor(seed, back2, back1, current, index++);

                back2 = back1;
                back1 = current;
              }
              while (e.MoveNext());

              if (tupleWrap >= 1)
                seed = aggregateComputor(seed, back2, back1, item1, index++);
              if (tupleWrap == 2)
                seed = aggregateComputor(seed, back1, item1, item2, index++);

              return resultSelector(seed, index);
            }
            else throw new System.ArgumentException($@"The sequence has only 2 elements.", nameof(source));
          else throw new System.ArgumentException($@"The sequence has only 1 element.", nameof(source));
        else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
      }

      //    ///// <summary>Creates a sequence of staggered (by one element) 4-tuple elements.</summary>
      //    ///// <returns>A sequence of 4-tuple elements staggered by one element, optionally extending the sequence by the specified number of wraps.</returns>
      //    ///// <see href="https://en.wikipedia.org/wiki/Tuple"/>
      //    //public static TResult AggregateTuple4<TSource, TAccumulate, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, TAccumulate seed, int wrap, System.Func<TAccumulate, TSource, TSource, TSource, TSource, int, TAccumulate> aggregateComputor, System.Func<TAccumulate, int, TResult> resultSelector)
      //    //{
      //    //  if (source is null) throw new System.ArgumentNullException(nameof(source));
      //    //  if (aggregateComputor is null) throw new System.ArgumentNullException(nameof(aggregateComputor));
      //    //  if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      //    //  if (wrap < 0 || wrap > 3) throw new System.ArgumentException(@"A 4-tuple can only wrap 0, 1, 2 or 3 elements.", nameof(wrap));

      //    //  using var e = source.GetEnumerator();

      //    //  if (e.MoveNext() && e.Current is var item1 && item1 is var back3)
      //    //    if (e.MoveNext() && e.Current is var item2 && item2 is var back2)
      //    //      if (e.MoveNext() && e.Current is var item3 && item3 is var back1)
      //    //        if (e.MoveNext())
      //    //        {
      //    //          var index = 0;

      //    //          do
      //    //          {
      //    //            var current = e.Current;

      //    //            seed = aggregateComputor(seed, back3, back2, back1, current, index++);

      //    //            back3 = back2;
      //    //            back2 = back1;
      //    //            back1 = current;
      //    //          }
      //    //          while (e.MoveNext());

      //    //          if (wrap >= 1)
      //    //            seed = aggregateComputor(seed, back3, back2, back1, item1, index++);
      //    //          if (wrap >= 2)
      //    //            seed = aggregateComputor(seed, back2, back1, item1, item2, index++);
      //    //          if (wrap == 3)
      //    //            seed = aggregateComputor(seed, back1, item1, item2, item3, index++);

      //    //          return resultSelector(seed, index);
      //    //        }
      //    //        else throw new System.ArgumentException($@"The sequence has only 3 elements.", nameof(source));
      //    //      else throw new System.ArgumentException($@"The sequence has only 2 elements.", nameof(source));
      //    //    else throw new System.ArgumentException($@"The sequence has only 1 element.", nameof(source));
      //    //  else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
      //    //}

      //    ///// <summary>Creates a sequence of staggered (by one element) 5-tuple elements.</summary>
      //    ///// <returns>A sequence of 5-tuple elements staggered by one element, optionally extending the sequence by the specified number of wraps.</returns>
      //    ///// <see href="https://en.wikipedia.org/wiki/Tuple"/>
      //    //public static TResult AggregateTuple5<TSource, TAccumulate, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, TAccumulate seed, int wrap, System.Func<TAccumulate, TSource, TSource, TSource, TSource, TSource, int, TAccumulate> aggregateComputor, System.Func<TAccumulate, int, TResult> resultSelector)
      //    //{
      //    //  if (source is null) throw new System.ArgumentNullException(nameof(source));
      //    //  if (aggregateComputor is null) throw new System.ArgumentNullException(nameof(aggregateComputor));
      //    //  if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      //    //  if (wrap < 0 || wrap > 4) throw new System.ArgumentException(@"A 5-tuple can only wrap 0, 1, 2, 3 or 4 elements.", nameof(wrap));

      //    //  using var e = source.GetEnumerator();

      //    //  if (e.MoveNext() && e.Current is var item1 && item1 is var back4)
      //    //    if (e.MoveNext() && e.Current is var item2 && item2 is var back3)
      //    //      if (e.MoveNext() && e.Current is var item3 && item3 is var back2)
      //    //        if (e.MoveNext() && e.Current is var item4 && item4 is var back1)
      //    //          if (e.MoveNext())
      //    //          {
      //    //            var index = 0;

      //    //            do
      //    //            {
      //    //              var current = e.Current;

      //    //              seed = aggregateComputor(seed, back4, back3, back2, back1, current, index++);

      //    //              back4 = back3;
      //    //              back3 = back2;
      //    //              back2 = back1;
      //    //              back1 = current;
      //    //            }
      //    //            while (e.MoveNext());

      //    //            if (wrap >= 1)
      //    //              seed = aggregateComputor(seed, back4, back3, back2, back1, item1, index++);
      //    //            if (wrap >= 2)
      //    //              seed = aggregateComputor(seed, back3, back2, back1, item1, item2, index++);
      //    //            if (wrap >= 3)
      //    //              seed = aggregateComputor(seed, back2, back1, item1, item2, item3, index++);
      //    //            if (wrap == 4)
      //    //              seed = aggregateComputor(seed, back1, item1, item2, item3, item4, index++);

      //    //            return resultSelector(seed, index);
      //    //          }
      //    //          else throw new System.ArgumentException($@"The sequence has only 4 elements.", nameof(source));
      //    //        else throw new System.ArgumentException($@"The sequence has only 3 elements.", nameof(source));
      //    //      else throw new System.ArgumentException($@"The sequence has only 2 elements.", nameof(source));
      //    //    else throw new System.ArgumentException($@"The sequence has only 1 element.", nameof(source));
      //    //  else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
      //    //}

      #endregion

      #region CartesianProduct

      public System.Collections.Generic.IEnumerable<(TSource, TTarget)> CartesianProductFor2<TTarget>(System.Collections.Generic.IEnumerable<TTarget> target)
        => source.SelectMany(s => target, (s, t) => (s, t));

      /// <summary>
      /// <para>Creates a new sequence with the cartesian product of all elements in the specified sequences.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="targets"></param>
      /// <returns></returns>
      public System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<TSource>> CartesianProduct(params System.Collections.Generic.IEnumerable<TSource>[] targets)
      {
        System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<TSource>> emptyProduct = [[]];

        return System.Linq.Enumerable.Aggregate(
          new System.Collections.Generic.IEnumerable<TSource>[] { source }.Concat(targets)
          , emptyProduct
          , (accumulator, sequence) =>
          from accumulatorSequence
          in accumulator
          from item
          in sequence
          select accumulatorSequence.Concat([item])
        );
      }

      /// <summary>
      /// <para>Creates a new sequence with the cartesian product of all elements in the specified sequences.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="targets"></param>
      /// <returns></returns>
      public System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<TSource>> CartesianProduct(System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<TSource>> targets)
      {
        System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<TSource>> emptyProduct = new[] { System.Linq.Enumerable.Empty<TSource>() };

        return System.Linq.Enumerable.Aggregate(
          new System.Collections.Generic.IEnumerable<TSource>[] { source }.Concat(targets)
          , emptyProduct
          , (accumulator, sequence) =>
          from accumulatorSequence
          in accumulator
          from item
          in sequence
          select accumulatorSequence.Concat(new[] { item })
        );
      }

      #endregion

      #region ..CommonPrefix..

      /// <summary>Determines whether the beginning of the first sequence is equivalent to the second sequence. Uses the specified equality comparer.</summary>
      /// <exception cref="System.ArgumentNullException"/>
      public int CommonPrefixLength(System.Collections.Generic.IEnumerable<TSource> target, System.Collections.Generic.IEqualityComparer<TSource>? equalityComparer = null, int maxLength = int.MaxValue)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<TSource>.Default;

        return source.Zip(target).TakeWhile((e, i) => equalityComparer.Equals(e.First, e.Second) && i < maxLength).Count();
      }

      /// <summary>
      /// <para>Determines whether the source sequence begins with the target sequence. Uses the specified equality comparer.</para>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentNullException"/>
      public bool IsCommonPrefix(System.Collections.Generic.IEnumerable<TSource> target, int length, System.Collections.Generic.IEqualityComparer<TSource>? equalityComparer = null)
        => CommonPrefixLength(source, target, equalityComparer, length) == length;

      #endregion

      #region CompareCount

      /// <summary>
      /// <para>Compares the number of elements in <paramref name="source"/> that satisfies the <paramref name="predicate"/> (all elements if null) against the specified <paramref name="count"/>.</para>
      /// </summary>
      /// <returns>Depending on <paramref name="source"/> count: -1 when less than, 0 when equal to, or 1 when greater than, the specified <paramref name="count"/>.</returns>
      /// <exception cref="System.ArgumentOutOfRangeException"/>
      public int CompareCount(int count, System.Func<TSource, int, bool> predicate)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(count);
        System.ArgumentNullException.ThrowIfNull(predicate);

        var index = 0;
        var counter = 0;

        foreach (var item in source)
          if (predicate(item, index++))
            if (++counter > count)
              break;

        return counter > count ? 1 : counter < count ? -1 : 0;
      }

      public int CompareCount(int count, System.Func<TSource, bool> predicate) => CompareCount(source, count, (e, i) => predicate(e));

      #endregion

      #region Counts

      public (int Count, int DistinctCount, bool IsDistinct, bool AllEqual) Counts<TValue>(System.Func<TSource, TValue> valueSelector, System.Collections.Generic.IEqualityComparer<TValue>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<TValue>.Default;

        var count = 0;

        var distinct = new System.Collections.Generic.HashSet<TValue>(equalityComparer);

        foreach (var item in source)
        {
          count++;

          distinct.Add(valueSelector(item));
        }

        var distinctCount = distinct.Count;

        return (count, distinctCount, distinctCount == count, distinctCount == 1);
      }

      #endregion

      #region ElementAtOrValue

      /// <summary>
      /// <para>Returns a tuple with the item and <paramref name="index"/> if available, otherwise <paramref name="value"/> and index = -1.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="value"></param>
      /// <param name="index"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public (TSource Item, int Index) ElementAtOrValue(TSource value, int index)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(index);

        using var e = source.GetEnumerator();

        for (var i = 0; e.MoveNext(); i++)
          if (i == index)
            return (e.Current, i);

        return (value, -1);
      }

      #endregion

      #region Extremum

      /// <summary>
      /// <para>Locate the minimum/maximum elements and indices, as evaluated by the <paramref name="valueSelector"/>, in <paramref name="source"/>. Uses the specified <paramref name="comparer"/> (default if null).</para>
      /// </summary>
      /// <typeparam name="TSource"></typeparam>
      /// <typeparam name="TValue"></typeparam>
      /// <param name="source"></param>
      /// <param name="valueSelector"></param>
      /// <param name="comparer"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentNullException"></exception>
      /// <exception cref="System.ArgumentException"></exception>
      public (TSource? MinElement, int MinIndex, TValue? MinValue, TSource? MaxElement, int MaxIndex, TValue? MaxValue) Extremum<TValue>(System.Func<TSource, TValue> valueSelector, System.Collections.Generic.IComparer<TValue>? comparer = null)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentNullException.ThrowIfNull(valueSelector);

        comparer ??= System.Collections.Generic.Comparer<TValue>.Default;

        var minItem = default(TSource);
        var minIndex = -1;
        var minValue = default(TValue);

        var maxItem = default(TSource);
        var maxIndex = -1;
        var maxValue = default(TValue);

        var index = 0;

        foreach (var item in source)
        {
          var value = valueSelector(item);

          if (minIndex < 0 || comparer.Compare(value, minValue) < 0)
          {
            minItem = item;
            minIndex = index;
            minValue = value;
          }

          if (maxIndex < 0 || comparer.Compare(value, maxValue) > 0)
          {
            maxItem = item;
            maxIndex = index;
            maxValue = value;
          }

          index++;
        }

        return (minItem, minIndex, minValue, maxItem, maxIndex, maxValue);
      }

      #endregion

      #region FirstOrValue

      /// <summary>
      /// <para>Returns the first predicated element, or (<paramref name="value"/>, -1) if none is found.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="value"></param>
      /// <param name="predicate">If null then predicate is ignored.</param>
      /// <returns></returns>
      public (TSource Item, int Index) FirstOrValue(TSource value, System.Func<TSource, int, bool> predicate)
      {
        System.ArgumentNullException.ThrowIfNull(predicate);

        var index = 0;

        foreach (var element in source)
          if (predicate(element, index)) return (element, index);
          else index++;

        return (value, -1);
      }

      /// <summary>
      /// <para>Returns the first predicated element, or (<paramref name="value"/>, -1) if none is found.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="value"></param>
      /// <param name="predicate"></param>
      /// <returns></returns>
      public (TSource Item, int Index) FirstOrValue(TSource value, System.Func<TSource, bool> predicate)
        => FirstOrValue(source, value, (e, i) => predicate(e));

      /// <summary>
      /// <para>Returns the first element, or (<paramref name="value"/>, -1) if none is found.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="value"></param>
      /// <returns></returns>
      public (TSource Item, int Index) FirstOrValue(TSource value)
        => FirstOrValue(source, value, (e, i) => true);

      #endregion

      #region GetIndexMap

      /// <summary>Creates a new dictionary with all indices of all target occurences in the source. Uses the specified <paramref name="equalityComparer"/> (default if null).</summary>
      /// <exception cref="System.ArgumentNullException"/>
      public System.Collections.Generic.IDictionary<TKey, System.Collections.Generic.List<int>> GetIndexMap<TKey>(System.Func<TSource, TKey> keySelector, System.Collections.Generic.IEqualityComparer<TKey>? equalityComparer = null)
        where TKey : notnull
      {
        System.ArgumentNullException.ThrowIfNull(keySelector);

        var map = new System.Collections.Generic.Dictionary<TKey, System.Collections.Generic.List<int>>(equalityComparer);

        var index = 0;

        foreach (var item in source)
        {
          var key = keySelector(item);

          if (map.TryGetValue(key, out System.Collections.Generic.List<int>? value))
            value.Add(index);
          else
            map[key] = [index];

          index++;
        }

        return map;
      }

      #endregion

      #region GroupAdjacent

      /// <summary>
      /// <para>Creates a new sequence of equal (based on the specified keySelector) adjacent (consecutive) items grouped together as a key and a list. Uses the specified equality comparer.</para>
      /// </summary>
      /// <exception cref="System.ArgumentNullException"/>
      public System.Collections.Generic.IEnumerable<System.Linq.IGrouping<TKey, TSource>> GroupAdjacent<TKey>(System.Func<TSource, TKey> keySelector, System.Collections.Generic.IEqualityComparer<TKey>? equalityComparer = null)
        where TKey : notnull
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentNullException.ThrowIfNull(keySelector);

        equalityComparer ??= System.Collections.Generic.EqualityComparer<TKey>.Default;

        using var e = source.GetEnumerator();

        if (e.MoveNext())
        {
          var item = e.Current;
          var key = keySelector(item);

          var g = new Flux.Grouping<TKey, TSource>(key, item);

          while (e.MoveNext())
          {
            item = e.Current;
            key = keySelector(item);

            if (!equalityComparer.Equals(g.Key, key))
            {
              yield return g;

              g = new Flux.Grouping<TKey, TSource>(key);
            }

            g.Add(item);
          }

          if (g.Count > 0)
            yield return g;
        }
      }

      #endregion

      #region InfimumSupremum

      /// <summary>
      /// <para>Gets the nearest ("less-than" and "greater-than", optionally with "-or-equal") elements and indices to the singleton set {<paramref name="referenceValue"/>}, as evaluated by the <paramref name="valueSelector"/>, in <paramref name="source"/>. Uses the specified <paramref name="comparer"/> (default if null).</para>
      /// <para>The infimum of a (singleton in this case) subset <paramref name="referenceValue"/> of a set <paramref name="source"/> is the greatest element in <paramref name="source"/> that is less-than-or-equal <paramref name="referenceValue"/>. If <paramref name="proper"/> is <see langword="true"/> then infimum will never be equal to <paramref name="referenceValue"/>.</para>
      /// <para>The supremum of a (singleton in this case) subset <paramref name="referenceValue"/> of a set <paramref name="source"/> is the least element in <paramref name="source"/> that is greater-than-or-equal <paramref name="referenceValue"/>. If <paramref name="proper"/> is <see langword="true"/> then supremum will never be equal to <paramref name="referenceValue"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Infimum_and_supremum"/></para>
      /// </summary>
      /// <typeparam name="TSource"></typeparam>
      /// <typeparam name="TValue"></typeparam>
      /// <param name="source">This is the set P.</param>
      /// <param name="valueSelector"></param>
      /// <param name="referenceValue">This (and the infimum/supremum) is the subset S.</param>
      /// <param name="proper">If <paramref name="proper"/> is <see langword="true"/> then infimum and supremum will never be equal, otherwise it may be equal.</param>
      /// <param name="comparer">Uses the specified comparer, or default if null.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentNullException"/>
      public (TSource? InfimumItem, int InfimumIndex, TValue? InfimumValue, TSource? SupremumItem, int SupremumIndex, TValue? SupremumValue) InfimumSupremum<TValue>(System.Func<TSource, TValue> valueSelector, TValue referenceValue, bool proper, System.Collections.Generic.IComparer<TValue>? comparer = null)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentNullException.ThrowIfNull(valueSelector);

        comparer ??= System.Collections.Generic.Comparer<TValue>.Default;

        var infimumItem = default(TSource);
        var infimumIndex = -1;
        var infimumValue = referenceValue;

        var supremumItem = default(TSource);
        var supremumIndex = -1;
        var supremumValue = referenceValue;

        var index = 0;

        foreach (var item in source)
        {
          var value = valueSelector(item);

          var cmp = comparer.Compare(value, referenceValue);

          if ((!proper ? cmp <= 0 : cmp < 0) && (infimumIndex < 0 || comparer.Compare(value, infimumValue) > 0))
          {
            infimumIndex = index;
            infimumItem = item;
            infimumValue = value;
          }

          if ((!proper ? cmp >= 0 : cmp > 0) && (supremumIndex < 0 || comparer.Compare(value, supremumValue) < 0))
          {
            supremumIndex = index;
            supremumItem = item;
            supremumValue = value;
          }

          index++;
        }

        return (infimumItem, infimumIndex, infimumValue, supremumItem, supremumIndex, supremumValue);
      }

      #endregion

      #region LastOrValue

      /// <summary>
      /// <para>Return the last predicated element, or (<paramref name="value"/>, -1) if none is found.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="value"></param>
      /// <param name="predicate"></param>
      /// <returns></returns>
      public (TSource Item, int Index) LastOrValue(TSource value, System.Func<TSource, int, bool> predicate)
      {
        System.ArgumentNullException.ThrowIfNull(predicate);

        var index = -1;

        var result = (Item: value, Index: index);

        foreach (var element in source)
          if (predicate(element, ++index))
            result = (element, index);

        return result;
      }

      /// <summary>
      /// <para>Return the last predicated element, or (<paramref name="value"/>, -1) if none is found.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="value"></param>
      /// <param name="predicate"></param>
      /// <returns></returns>
      public (TSource Item, int Index) LastOrValue(TSource value, System.Func<TSource, bool> predicate)
        => LastOrValue(source, value, (e, i) => predicate(e));

      /// <summary>
      /// <para>Return the last element, or (<paramref name="value"/>, -1) if none is found.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="value"></param>
      /// <returns></returns>
      public (TSource Item, int Index) LastOrValue(TSource value)
        => LastOrValue(source, value, (e, i) => true);

      #endregion

      #region Mode

      /// <summary>
      /// <para>Creates a new sequence consisting of 'moded' (i.e. sorted by the most frequent or common) elements in <paramref name="source"/>.</para>
      /// <para><see href="http://en.wikipedia.org/wiki/Mode"/></para>
      /// </summary>
      /// <returns></returns>
      public System.Linq.IOrderedEnumerable<(TSource Item, int Count)> Mode()
        => source.GroupBy(t => t).Select(g => (Item: g.Key, Count: g.Count())).OrderByDescending(tuple => tuple.Count);

      #endregion

      #region PartitionNgram

      /// <summary>
      /// <para>Partition the sequence as a new sequence of size elements.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/N-gram"/></para>
      /// </summary>
      /// <typeparam name="TSource"></typeparam>
      /// <typeparam name="TResult"></typeparam>
      /// <param name="source"></param>
      /// <param name="size"></param>
      /// <param name="resultSelector"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      /// <exception cref="System.ArgumentException"></exception>
      public System.Collections.Generic.IEnumerable<TResult> PartitionNgram<TResult>(int size, System.Func<System.Collections.Generic.IEnumerable<TSource>, int, TResult> resultSelector)
      {
        System.ArgumentNullException.ThrowIfNull(resultSelector);

        if (size < 1) throw new System.ArgumentOutOfRangeException(nameof(size), $"Must be greater than or equal to 1 ({size}).");

        using var e = source.ThrowOnNull().GetEnumerator();

        var index = 0;

        var ngram = System.Collections.Immutable.ImmutableQueue<TSource>.Empty;

        while (ngram.Count() < size && e.MoveNext())
          ngram = ngram.Enqueue(e.Current);

        if (ngram.Count() == size)
        {
          yield return resultSelector(ngram, index++);

          while (e.MoveNext())
          {
            ngram = ngram.Dequeue();
            ngram = ngram.Enqueue(e.Current);

            yield return resultSelector(ngram, index++);
          }
        }
        else throw new System.ArgumentException(@"Insufficient number of elements.", nameof(source));
      }

      #endregion

      #region PartitionTuple

      /// <summary>
      /// <para>Creates a sequence of staggered (by one element) n-tuples.</para>
      /// <see href="https://en.wikipedia.org/wiki/Tuple"/>
      /// </summary>
      /// <param name="tupleSize">The number of elements in each tuple.</param>
      /// <param name="tupleWrap">The number of staggered wrap-around tuples to return, beyond the last element in the source sequence.</param>
      /// <param name="resultSelector">Allows the result of each tuple to be processed.</param>
      /// <returns>A sequence of n-tuples staggered by one element, optionally extending the sequence by the specified overflow.</returns>
      /// <exception cref="System.ArgumentNullException"/>
      public System.Collections.Generic.IEnumerable<TResult> PartitionTuple<TResult>(int tupleSize, int tupleWrap, System.Func<System.Collections.Generic.IReadOnlyList<TSource>, int, TResult> resultSelector)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentNullException.ThrowIfNull(resultSelector);

        if (tupleSize < 2) throw new System.ArgumentOutOfRangeException(nameof(tupleSize));
        if (tupleWrap < 0 || tupleWrap >= tupleSize) throw new System.ArgumentException($@"A {tupleSize}-tuple can only wrap from 0 to {tupleSize - 1} elements.");

        using var e = source.GetEnumerator();

        var start = System.Collections.Immutable.ImmutableList<TSource>.Empty;

        while (start.Count < tupleSize && e.MoveNext())
        {
          start = start.Add(e.Current);
        }

        if (start.Count == tupleSize)
        {
          var tuple = start;

          var index = 0;

          yield return resultSelector(tuple, index++);

          while (e.MoveNext())
          {
            tuple = tuple.RemoveAt(0);
            tuple = tuple.Add(e.Current);
            yield return resultSelector(tuple, index++);
          }

          while (tupleWrap-- > 0)
          {
            tuple = tuple.RemoveAt(0);
            tuple = tuple.Add(start[0]);
            start = start.RemoveAt(0);
            yield return resultSelector(tuple, index++);
          }
        }
        else throw new System.ArgumentException($"The sequence has only {start.Count} elements.", nameof(source));
      }

      /// <summary>
      /// <para>Create a new sequence of 2-tuples, project with a <paramref name="resultSelector"/>, and optionally overlap by wrap-around to the first element.</para>
      /// <see href="https://en.wikipedia.org/wiki/Tuple"/>
      /// </summary>
      /// <exception cref="System.ArgumentNullException"/>
      public System.Collections.Generic.IEnumerable<TResult> PartitionTuple2<TResult>(bool wrap, System.Func<TSource, TSource, int, TResult> resultSelector)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentNullException.ThrowIfNull(resultSelector);

        using var e = source.GetEnumerator();

        if (e.MoveNext() && e.Current is var item1 && item1 is var back1 && e.MoveNext())
        {
          var index = 0;

          do
          {
            var current = e.Current;

            yield return resultSelector(back1, current, index++);

            back1 = current;
          }
          while (e.MoveNext());

          if (wrap)
            yield return resultSelector(back1, item1, index++);
        }
        else throw new System.ArgumentException("The sequence has only 1 element.", nameof(source));
      }

      public System.Collections.Generic.IEnumerable<TResult> PartitionTuple2<TResult>(bool wrap, System.Func<TSource, TSource, TResult> resultSelector)
        => PartitionTuple2(source, wrap, (a, b, i) => resultSelector(a, b));

      /// <summary>
      /// <para>Create a new sequence of 3-tuples, project with a <paramref name="resultSelector"/>, and optionally overlap by wrap-around to the first or the second element.</para>
      /// <see href="https://en.wikipedia.org/wiki/Tuple"/>
      /// </summary>
      /// <exception cref="System.ArgumentNullException"/>
      public System.Collections.Generic.IEnumerable<TResult> PartitionTuple3<TResult>(int wrap, System.Func<TSource, TSource, TSource, int, TResult> resultSelector)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentNullException.ThrowIfNull(resultSelector);

        if (wrap < 0 || wrap > 2) throw new System.ArgumentException("A 3-tuple can only wrap 0 (i.e. no-wrap), 1 or 2 elements.", nameof(wrap));

        using var e = source.GetEnumerator();

        if (e.MoveNext() && e.Current is var item1 && item1 is var back2 && e.MoveNext() && e.Current is var item2 && item2 is var back1)
          if (e.MoveNext())
          {
            var index = 0;

            do
            {
              var current = e.Current;

              yield return resultSelector(back2, back1, current, index++);

              back2 = back1;
              back1 = current;
            }
            while (e.MoveNext());

            if (wrap >= 1) yield return resultSelector(back2, back1, item1, index++);
            if (wrap == 2) yield return resultSelector(back1, item1, item2, index++);
          }
          else throw new System.ArgumentException("The sequence has only 2 elements.", nameof(source));
        else throw new System.ArgumentException("The sequence has only 1 element.", nameof(source));
      }

      ///// <summary>Create a new sequence of 4-tuples, project with a <paramref name="resultSelector"/>, and optionally overlap by wrap-around up to the third element.</summary>
      ///// <see href="https://en.wikipedia.org/wiki/Tuple"/>
      //public static System.Collections.Generic.IEnumerable<TResult> PartitionTuple4<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, int wrap, System.Func<TSource, TSource, TSource, TSource, int, TResult> resultSelector)
      //{
      //  if (source is null) throw new System.ArgumentNullException(nameof(source));
      //  if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      //  if (wrap < 0 || wrap > 3) throw new System.ArgumentException(@"A 4-tuple can only wrap 0, 1, 2 or 3 elements.", nameof(wrap));

      //  using var e = source.GetEnumerator();

      //  if (e.MoveNext() && e.Current is var item1 && item1 is var back3)
      //    if (e.MoveNext() && e.Current is var item2 && item2 is var back2)
      //      if (e.MoveNext() && e.Current is var item3 && item3 is var back1)
      //        if (e.MoveNext())
      //        {
      //          var index = 0;

      //          do
      //          {
      //            var current = e.Current;

      //            yield return resultSelector(back3, back2, back1, current, index++);

      //            back3 = back2;
      //            back2 = back1;
      //            back1 = current;
      //          }
      //          while (e.MoveNext());

      //          if (wrap >= 1) yield return resultSelector(back3, back2, back1, item1, index++);
      //          if (wrap >= 2) yield return resultSelector(back2, back1, item1, item2, index++);
      //          if (wrap == 3) yield return resultSelector(back1, item1, item2, item3, index++);
      //        }
      //        else throw new System.ArgumentException(@"The sequence has only 3 elements.", nameof(source));
      //      else throw new System.ArgumentException(@"The sequence has only 2 elements.", nameof(source));
      //    else throw new System.ArgumentException(@"The sequence has only 1 element.", nameof(source));
      //  else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
      //}

      ///// <summary>Create a new sequence of 5-tuples, project with a <paramref name="resultSelector"/>, and optionally overlap by wrap-around up to the fourth element.</summary>
      ///// <see href="https://en.wikipedia.org/wiki/Tuple"/>
      //public static System.Collections.Generic.IEnumerable<TResult> PartitionTuple5<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, int wrap, System.Func<TSource, TSource, TSource, TSource, TSource, int, TResult> resultSelector)
      //{
      //  if (source is null) throw new System.ArgumentNullException(nameof(source));
      //  if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      //  if (wrap < 0 || wrap > 4) throw new System.ArgumentException(@"A 5-tuple can only wrap 0, 1, 2, 3 or 4 elements.", nameof(wrap));

      //  using var e = source.GetEnumerator();

      //  if (e.MoveNext() && e.Current is TSource item1 && item1 is var back4)
      //    if (e.MoveNext() && e.Current is TSource item2 && item2 is var back3)
      //      if (e.MoveNext() && e.Current is TSource item3 && item3 is var back2)
      //        if (e.MoveNext() && e.Current is TSource item4 && item4 is var back1)
      //          if (e.MoveNext())
      //          {
      //            var index = 0;

      //            do
      //            {
      //              var current = e.Current;

      //              yield return resultSelector(back4, back3, back2, back1, current, index++);

      //              back4 = back3;
      //              back3 = back2;
      //              back2 = back1;
      //              back1 = current;
      //            }
      //            while (e.MoveNext());

      //            if (wrap >= 1) yield return resultSelector(back4, back3, back2, back1, item1, index++);
      //            if (wrap >= 2) yield return resultSelector(back3, back2, back1, item1, item2, index++);
      //            if (wrap >= 3) yield return resultSelector(back2, back1, item1, item2, item3, index++);
      //            if (wrap == 4) yield return resultSelector(back1, item1, item2, item3, item4, index++);
      //          }
      //          else throw new System.ArgumentException(@"The sequence has only 4 elements.", nameof(source));
      //        else throw new System.ArgumentException(@"The sequence has only 3 elements.", nameof(source));
      //      else throw new System.ArgumentException(@"The sequence has only 2 elements.", nameof(source));
      //    else throw new System.ArgumentException(@"The sequence has only 1 element.", nameof(source));
      //  else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
      //}

      ///// <summary>Create a new sequence of 5-tuples, project with a <paramref name="resultSelector"/>, and optionally overlap by wrap-around up to the fourth element.</summary>
      ///// <see href="https://en.wikipedia.org/wiki/Tuple"/>
      //public static System.Collections.Generic.IEnumerable<TResult> PartitionTuple6<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, int wrap, System.Func<TSource, TSource, TSource, TSource, TSource, TSource, int, TResult> resultSelector)
      //{
      //  if (source is null) throw new System.ArgumentNullException(nameof(source));
      //  if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      //  if (wrap < 0 || wrap > 5) throw new System.ArgumentException(@"A 6-tuple can only wrap 0, 1, 2, 3, 4 or 5 elements.", nameof(wrap));

      //  using var e = source.GetEnumerator();

      //  if (e.MoveNext() && e.Current is TSource item1 && item1 is var back5)
      //    if (e.MoveNext() && e.Current is TSource item2 && item2 is var back4)
      //      if (e.MoveNext() && e.Current is TSource item3 && item3 is var back3)
      //        if (e.MoveNext() && e.Current is TSource item4 && item4 is var back2)
      //          if (e.MoveNext() && e.Current is TSource item5 && item5 is var back1)
      //            if (e.MoveNext())
      //            {
      //              var index = 0;

      //              do
      //              {
      //                var current = e.Current;

      //                yield return resultSelector(back5, back4, back3, back2, back1, current, index++);

      //                back5 = back4;
      //                back4 = back3;
      //                back3 = back2;
      //                back2 = back1;
      //                back1 = current;
      //              }
      //              while (e.MoveNext());

      //              if (wrap >= 1) yield return resultSelector(back5, back4, back3, back2, back1, item1, index++);
      //              if (wrap >= 2) yield return resultSelector(back4, back3, back2, back1, item1, item2, index++);
      //              if (wrap >= 3) yield return resultSelector(back3, back2, back1, item1, item2, item3, index++);
      //              if (wrap >= 4) yield return resultSelector(back2, back1, item1, item2, item3, item4, index++);
      //              if (wrap == 5) yield return resultSelector(back1, item1, item2, item3, item4, item5, index++);
      //            }
      //            else throw new System.ArgumentException(@"The sequence has only 5 elements.", nameof(source));
      //          else throw new System.ArgumentException(@"The sequence has only 4 elements.", nameof(source));
      //        else throw new System.ArgumentException(@"The sequence has only 3 elements.", nameof(source));
      //      else throw new System.ArgumentException(@"The sequence has only 2 elements.", nameof(source));
      //    else throw new System.ArgumentException(@"The sequence has only 1 element.", nameof(source));
      //  else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
      //}

      #endregion

      #region PartitionWindowed

      /// <summary>
      /// <para>Apportions the sequence into lists of specified size with the specified stepping (or gap) interleave (0 means next in line and a positive number below size means skip that many, from the start of the previous list). Optionally include trailing lists, i.e. lists that could not be filled to size.</para>
      /// </summary>
      /// <typeparam name="TSource"></typeparam>
      /// <typeparam name="TResult"></typeparam>
      /// <param name="source"></param>
      /// <param name="size"></param>
      /// <param name="step"></param>
      /// <param name="includeTrailing"></param>
      /// <param name="resultSelector"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public System.Collections.Generic.IEnumerable<TResult> PartitionWindowed<TResult>(int size, int step, bool includeTrailing, System.Func<System.Collections.Generic.List<TSource>, TResult> resultSelector)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentNullException.ThrowIfNull(resultSelector);

        if (size <= 0) throw new System.ArgumentOutOfRangeException(nameof(size), "Must be greater-than zero.");
        if (step <= 0 || step > size) throw new System.ArgumentOutOfRangeException(nameof(step), "Must be greater-than zero and less-than-or-equal to size.");

        var queue = new System.Collections.Generic.Queue<System.Collections.Generic.List<TSource>>();

        var e = source.GetEnumerator();

        var index = step;

        while (e.MoveNext())
        {
          if (index++ >= step)
          {
            index = 1;

            queue.Enqueue([]);
          }

          foreach (var list in queue)
            list.Add(e.Current);

          if (queue.Peek().Count == size)
            yield return resultSelector(queue.Dequeue());
        }
        ;

        if (includeTrailing)
          while (queue.Count > 0 && queue.Peek().Count > 0)
            yield return resultSelector(queue.Dequeue());
      }

      #endregion

      #region Random..

      /// <summary>
      /// <para>Returns approximately the specified percent (<paramref name="probability"/>) of random elements from <paramref name="source"/> up to <paramref name="maxCount"/> elements. Uses the specified <paramref name="rng"/> (shared if null).</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="probability">Probability as a percent value in the range (0, 1].</param>
      /// <param name="rng">The random-number-generator to use, or <see cref="System.Random.Shared"/> if null.</param>
      /// <param name="maxCount"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public System.Collections.Generic.IEnumerable<TSource> RandomElements(double probability, System.Random? rng = null, int maxCount = int.MaxValue)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxCount);

        Units.Probability.AssertMember(probability, IntervalNotation.HalfOpenLeft); // Cannot be zero, but can be one.

        rng ??= System.Random.Shared;

        var count = 0;

        foreach (var item in source)
          if (rng.NextDouble() < probability)
          {
            yield return item;

            if (++count >= maxCount)
              break;
          }
      }

      /// <summary>
      /// <para>Returns a random element from <paramref name="source"/>. Uses the specified <paramref name="rng"/> (shared if null).</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="rng"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentNullException"></exception>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public TSource GetRandomElement(System.Random? rng = null)
        => source.ThrowOnNullOrEmpty().RandomOrValue(default!, rng).Item;

      /// <summary>
      /// <para>Randomize an element and its index in <paramref name="source"/>, or <paramref name="value"/> if none is found (with index = -1). Uses the specified <paramref name="rng"/> (shared if null).</para>
      /// <para><seealso href="http://stackoverflow.com/questions/648196/random-row-from-linq-to-sql/648240#648240"/></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="value"></param>
      /// <param name="rng">The random number generator to use.</param>
      /// <returns></returns>
      public (TSource Item, int Index) RandomOrValue(TSource value, System.Random? rng = null)
      {
        rng ??= System.Random.Shared;

        var result = (value, -1);

        var index = 0;

        foreach (var item in source)
          if (rng.Next(++index) == 0) // Add one to index before the RNG call, because the upper range is exlusive, so no missing any numbers.
            result = (item, index - 1); // And subtract one for correct index reference.

        return result;
      }

      /// <summary>
      /// <para>Attempts to fetch a random element from <paramref name="source"/> into <paramref name="result"/> and indicates whether successful. Uses the specified <paramref name="rng"/> (shared if null).</para>
      /// <para><seealso cref="http://stackoverflow.com/questions/648196/random-row-from-linq-to-sql/648240#648240"/></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="result"></param>
      /// <param name="rng">The random number generator to use.</param>
      /// <returns></returns>
      public bool TryGetRandomElement(out TSource result, System.Random? rng = null)
      {
        try
        {
          result = source.GetRandomElement(rng);
          return true;
        }
        catch { }

        result = default!;
        return false;
      }

      #endregion

      #region RunLengthEncode

      /// <summary>
      /// <para>Run-length encodes <paramref name="source"/> by converting consecutive instances of the same element into a <c>KeyValuePair{T,int}</c> representing the item and its occurrence count. Uses the specified <paramref name="equalityComparer"/> (default if null).</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      public System.Collections.Generic.IEnumerable<(TSource Item, int Count)> RunLengthEncode(System.Collections.Generic.IEqualityComparer<TSource>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<TSource>.Default;

        using var e = source.ThrowOnNull().GetEnumerator();

        if (e.MoveNext())
        {
          var previous = e.Current;

          var count = 1;

          while (e.MoveNext())
          {
            if (equalityComparer.Equals(previous, e.Current))
              count++;
            else
            {
              yield return (previous, count);

              previous = e.Current;
              count = 1;
            }
          }

          yield return (previous, count);
        }
      }

      #endregion

      #region RunningAggregate

      /// <summary>
      /// <para>Applies an running accumulator over a sequence. I.e. statistics is computed as the aggregator is running its course.</para>
      /// </summary>
      /// <typeparam name="TSource"></typeparam>
      /// <typeparam name="TAccumulate"></typeparam>
      /// <param name="source"></param>
      /// <param name="seed"></param>
      /// <param name="func"></param>
      /// <returns></returns>
      public System.Collections.Generic.IEnumerable<(TAccumulate aggregate, TSource element, int index)> RunningAggregate<TAccumulate>(TAccumulate seed, System.Func<TAccumulate, TSource, int, TAccumulate> func)
      {
        System.ArgumentNullException.ThrowIfNull(seed);
        System.ArgumentNullException.ThrowIfNull(func);

        var index = 0;

        var result = seed;

        foreach (var item in source)
        {
          result = func(result, item, index);

          yield return (result, item, index);

          index++;
        }
      }

      #endregion

      #region SelectWhere

      /// <summary>
      /// <para>Yields a new sequence of elements from <paramref name="source"/> based on <paramref name="selector"/> and <paramref name="predicate"/>.</para>
      /// </summary>
      /// <typeparam name="TSource"></typeparam>
      /// <typeparam name="TResult"></typeparam>
      /// <param name="source"></param>
      /// <param name="selector"></param>
      /// <param name="predicate"></param>
      /// <returns></returns>
      public System.Collections.Generic.IEnumerable<TResult> SelectWhere<TResult>(System.Func<TSource, int, TResult> selector, System.Func<TSource, int, TResult, bool> predicate)
      {
        System.ArgumentNullException.ThrowIfNull(selector);
        System.ArgumentNullException.ThrowIfNull(predicate);

        var index = 0;

        foreach (var element in source)
        {
          if (selector(element, index) is var select && predicate(element, index, select))
            yield return select;

          index++;
        }
      }

      #endregion

      #region SequenceHashCode

      /// <summary>
      /// <para>Computes a hash code, representing all elements in <paramref name="source"/>, using the .NET built-in <see cref="System.HashCode"/> functionality.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <returns></returns>
      public int SequenceHashCode()
        => source.Aggregate(new System.HashCode(), (hc, e) => { hc.Add(e); return hc; }, hc => hc.ToHashCode());

      ///// <summary>
      ///// <para>Computes a hash code, representing all elements in <paramref name="source"/>, by xor'ing the <see cref="{T}.GetHashCode()"/> of the elements.</para>
      ///// </summary>
      ///// <typeparam name="T"></typeparam>
      ///// <param name="source"></param>
      ///// <returns></returns>
      //public static int SequenceHashCodeByXor<T>(this System.Collections.Generic.IEnumerable<T> source)
      //  => source.Aggregate(0, (hc, e) => hc ^ (e?.GetHashCode() ?? 0));

      #endregion

      #region SingleOrValue

      /// <summary>
      /// <para>Return the only single predicated element, or (<paramref name="value"/>, -1).</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="value"></param>
      /// <param name="predicate"></param>
      /// <returns></returns>
      /// <exception cref="System.InvalidOperationException"></exception>
      public (TSource Item, int Index) SingleOrValue(TSource value, System.Func<TSource, int, bool> predicate)
      {
        System.ArgumentNullException.ThrowIfNull(predicate);

        var index = -1;

        var valueOrSingle = (Item: value, Index: index); // If a single value is found, it is returned. Also, if no value is found, this initial (value, -1) is returned.

        foreach (var element in source)
        {
          if (predicate(element, ++index))
          {
            if (valueOrSingle.Index > -1) // If more than a single value is found then (value, -1) is returned.
              return (value, -1);

            valueOrSingle = (element, index);
          }

        }

        return valueOrSingle;
      }

      /// <summary>
      /// <para>Return the only single predicated element, or (<paramref name="value"/>, -1).</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="value"></param>
      /// <param name="predicate"></param>
      /// <returns></returns>
      public (TSource Item, int Index) SingleOrValue(TSource value, System.Func<TSource, bool> predicate)
        => SingleOrValue(source, value, (e, i) => predicate(e));

      /// <summary>
      /// <para>Return the only single predicated element, or (<paramref name="value"/>, -1).</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="value"></param>
      /// <returns></returns>
      public (TSource Item, int Index) SingleOrValue(TSource value)
        => SingleOrValue(source, value, (e, i) => true);

      #endregion

      #region SkipEvery

      /// <summary>Creates a new sequence by skipping the <paramref name="option"/> at every <paramref name="interval"/> from <paramref name="source"/>.</summary>
      /// <exception cref="System.ArgumentNullException"/>
      public System.Collections.Generic.IEnumerable<TSource> SkipEvery(int index, int interval)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(index);
        System.ArgumentOutOfRangeException.ThrowIfNegative(interval);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, interval);

        return source.Where((e, i) => i % interval != index);
      }

      #endregion

      #region SkipLastWhile

      /// <summary>Creates a new sequence by skipping the last elements that satisfies the predicate. This version also passes the source index into the predicate.</summary>
      /// <exception cref="System.ArgumentNullException"/>
      public System.Collections.Generic.IEnumerable<TSource> SkipLastWhile(System.Func<TSource, int, bool> predicate)
      {
        System.ArgumentNullException.ThrowIfNull(predicate);

        var buffer = new System.Collections.Generic.Queue<TSource>();

        var counter = 0;

        foreach (var item in source)
        {
          if (predicate(item, counter++))
            buffer.Enqueue(item);
          else
          {
            while (buffer.Count > 0)
              yield return buffer.Dequeue();

            yield return item;
          }
        }
      }

      #endregion

      #region TakeEvery

      /// <summary>Creates a new sequence by taking the <paramref name="option"/> at every <paramref name="interval"/> from <paramref name="source"/>.</summary>
      /// <exception cref="System.ArgumentNullException"/>
      public System.Collections.Generic.IEnumerable<TSource> TakeEvery(int index, int interval)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(index);
        System.ArgumentOutOfRangeException.ThrowIfNegative(interval);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, interval);

        return source.Where((e, i) => i % interval == index);
      }

      #endregion

      #region TakeLastWhile

      /// <summary>Creates a new sequence by taking the last elements of <paramref name="source"/> that satisfies the <paramref name="predicate"/>. This version also passes the source index into the predicate.</summary>
      /// <exception cref="System.ArgumentNullException"/>
      public System.Collections.Generic.IEnumerable<TSource> TakeLastWhile(System.Func<TSource, int, bool> predicate)
      {
        System.ArgumentNullException.ThrowIfNull(predicate);

        var buffer = new System.Collections.Generic.List<TSource>();

        var index = 0;

        foreach (var item in source)
        {
          if (predicate(item, index++))
            buffer.Add(item);
          else
            buffer.Clear();
        }

        return buffer;
      }

      #endregion

      #region ThrowOnNull

      /// <summary>Throws an exception if <paramref name="source"/> is null. Deferred execution.</summary>
      /// <exception cref="System.ArgumentNullException"/>
      public System.Collections.Generic.IEnumerable<TSource> ThrowOnNull(string? paramName = null)
      {
        foreach (var item in source ?? throw new System.ArgumentNullException(paramName ?? nameof(source), "The sequence cannot be null."))
          yield return item;  // Must yield for deferred execution.
      }

      #endregion

      #region ThrowOnNullOrEmpty

      /// <summary>Throws an exception if <paramref name="source"/> is null or the sequence empty. Deferred execution.</summary>
      /// <exception cref="System.ArgumentNullException">The sequence cannot be null.</exception>
      /// <exception cref="System.ArgumentException">The sequence cannot be empty.</exception>
      public System.Collections.Generic.IEnumerable<TSource> ThrowOnNullOrEmpty(string? paramName = null)
      {
        using var e = source.ThrowOnNull(paramName).GetEnumerator();

        if (e.MoveNext())
        {
          do
            yield return e.Current;
          while (e.MoveNext());

          yield break;
        }

        throw new System.ArgumentException("The sequence cannot be empty.", paramName ?? nameof(source));
      }

      #endregion

      #region ToSortedDictionary

      /// <summary>
      /// <para>Creates a new <see cref="SortedDictionary{TKey, TValue}"/> with the specified <paramref name="comparer"/> and all items from <paramref name="source"/> using <paramref name="keySelector"/> and <paramref name="valueSelector"/> for each item.</para>
      /// </summary>
      /// <exception cref="System.ArgumentNullException"/>
      /// <typeparam name="TSource"></typeparam>
      /// <typeparam name="TKey"></typeparam>
      /// <typeparam name="TValue"></typeparam>
      /// <param name="source"></param>
      /// <param name="keySelector"></param>
      /// <param name="valueSelector"></param>
      /// <param name="comparer"></param>
      /// <returns></returns>
      public System.Collections.Generic.SortedDictionary<TKey, TValue> ToSortedDictionary<TKey, TValue>(System.Func<TSource, TKey> keySelector, System.Func<TSource, TValue> valueSelector, System.Collections.Generic.IComparer<TKey>? comparer = null)
        where TKey : notnull
      {
        System.ArgumentNullException.ThrowIfNull(keySelector);
        System.ArgumentNullException.ThrowIfNull(valueSelector);

        var sd = new System.Collections.Generic.SortedDictionary<TKey, TValue>(comparer ?? System.Collections.Generic.Comparer<TKey>.Default);

        foreach (var item in source)
          sd.Add(keySelector(item), valueSelector(item));

        return sd;
      }

      #endregion

      #region ToSpanBuilder

      public SpanBuilder<TSource> ToSpanBuilder()
      {
        var sb = new SpanBuilder<TSource>();
        foreach (var item in source)
          sb.Append(item);
        return sb;
      }

      #endregion

      #region ToTwoDimensionalArray

      /// <summary>
      /// <para>Creates a new two-dimensional array with the specified sizes, and then fills the target (from the source) in a 'dimension 0'-major order.</para>
      /// </summary>
      /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
      /// <exception cref="System.ArgumentNullException"/>
      public TSource[,] ToTwoDimensionalArray(int length0, int length1)
      {
        using var e = source.ThrowOnNull().GetEnumerator();

        var target = new TSource[length0, length1];

        for (var i0 = 0; i0 < length0; i0++)
          for (var i1 = 0; i1 < length1; i1++)
            target[i0, i1] = e.MoveNext() ? e.Current : default!;

        return target;
      }

      #endregion

      #region ZipEx

      /// <summary>This version of Zip runs over all elements in all sequences.</summary>
      public System.Collections.Generic.IEnumerable<TResult> ZipEx<TResult>(System.Func<TSource[], TResult> resultSelector, params System.Collections.Generic.IEnumerable<TSource>[] sequences)
      {
        System.ArgumentNullException.ThrowIfNull(resultSelector);

        var e = new System.Collections.Generic.IEnumerator<TSource>[sequences.Length];

        for (var index = 0; index < sequences.Length; index++)
          e[index] = null!;

        try
        {
          for (var index = 0; index < sequences.Length; index++)
            e[index] = sequences[index].GetEnumerator();

          var b = new bool[sequences.Length];

          while (true)
          {
            for (var index = 0; index < sequences.Length; index++)
              b[index] = e[index]?.MoveNext() ?? false;

            if (b.Any(boolean => boolean))
              yield return resultSelector(e.Select((o, i) => b[i] ? e[i].Current : default!).ToArray());
            else
              yield break;
          }
        }
        finally
        {
          for (var index = 0; index < sequences.Length; index++)
            e[index]?.Dispose();
        }
      }

      #endregion
    }
  }
}
