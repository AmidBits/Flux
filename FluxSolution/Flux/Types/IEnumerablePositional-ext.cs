namespace Flux
{
  public static partial class IEnumerablePositional
  {
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
    public static (T item, int index) ElementAtOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, T value, int index)
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(index);

      using var e = source.GetEnumerator();

      for (var i = 0; e.MoveNext(); i++)
        if (i == index)
          return (e.Current, i);

      return (value, -1);
    }

    #endregion

    #region FirstOrValue

    /// <summary>
    /// <para>Returns the a tuple with the first element and index from <paramref name="source"/> that satisfies the <paramref name="predicate"/>, otherwise <paramref name="value"/> and index = -1.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <param name="predicate">If null then predicate is ignored.</param>
    /// <returns></returns>
    public static (T Item, int Index) FirstOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, T value, System.Func<T, int, bool>? predicate = null)
    {
      predicate ??= (e, i) => true;

      var index = 0;

      foreach (var element in source)
        if (predicate(element, index)) return (element, index);
        else index++;

      return (value, -1);
    }

    public static (T Item, int Index) FirstOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, T value)
      => FirstOrValue(source, value, (e, i) => i == 0);

    #endregion

    #region GetExtremum

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
    public static (int MinIndex, TSource? MinItem, TValue? MinValue, int MaxIndex, TSource? MaxItem, TValue? MaxValue) GetExtremum<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> valueSelector, System.Collections.Generic.IComparer<TValue>? comparer = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentNullException.ThrowIfNull(valueSelector);

      comparer ??= System.Collections.Generic.Comparer<TValue>.Default;

      var minIndex = -1;
      var minItem = default(TSource);
      var minValue = default(TValue);

      var maxIndex = -1;
      var maxItem = default(TSource);
      var maxValue = default(TValue);

      var index = 0;

      foreach (var item in source)
      {
        var value = valueSelector(item);

        if (minIndex < 0 || comparer.Compare(value, minValue) < 0)
        {
          minIndex = index;
          minItem = item;
          minValue = value;
        }

        if (maxIndex < 0 || comparer.Compare(value, maxValue) > 0)
        {
          maxIndex = index;
          maxItem = item;
          maxValue = value;
        }

        index++;
      }

      return (minIndex, minItem, minValue, maxIndex, maxItem, maxValue);
    }

    #endregion

    #region GetIndexMap

    /// <summary>Creates a new dictionary with all indices of all target occurences in the source. Uses the specified <paramref name="equalityComparer"/> (default if null).</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IDictionary<TKey, System.Collections.Generic.List<int>> GetIndexMap<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Collections.Generic.IEqualityComparer<TKey>? equalityComparer = null)
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

    #region GetInfimumAndSupremum

    /// <summary>
    /// <para>Gets the nearest ("less-than" and "greater-than", optionally with "-or-equal") elements and indices to the singleton set {<paramref name="referenceValue"/>}, as evaluated by the <paramref name="valueSelector"/>, in <paramref name="source"/>. Uses the specified <paramref name="comparer"/> (default if null).</para>
    /// <para>The infimum of a (singleton in this case) subset <paramref name="referenceValue"/> of a set <paramref name="source"/> is the greatest element in <paramref name="source"/> that is less-than-or-equal <paramref name="referenceValue"/>. If <paramref name="proper"/> is <see langword="true"/> then infimum will never be equal.</para>
    /// <para>The supremum of a (singleton in this case) subset <paramref name="referenceValue"/> of a set <paramref name="source"/> is the least element in <paramref name="source"/> that is greater-than-or-equal <paramref name="referenceValue"/>. If <paramref name="proper"/> is <see langword="true"/> then supremum will never be equal.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Infimum_and_supremum"/></para>
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="source">This is the set P.</param>
    /// <param name="valueSelector"></param>
    /// <param name="referenceValue">This is the subset S.</param>
    /// <param name="proper">If <paramref name="proper"/> is <see langword="true"/> then infimum and supremum will never be equal, otherwise it may be equal.</param>
    /// <param name="comparer">Uses the specified comparer, or default if null.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException"/>
    public static (int InfimumIndex, TSource? InfimumItem, TValue? InfimumValue, int SupremumIndex, TSource? SupremumItem, TValue? SupremumValue) GetInfimumAndSupremum<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> valueSelector, TValue referenceValue, bool proper, System.Collections.Generic.IComparer<TValue>? comparer = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentNullException.ThrowIfNull(valueSelector);

      comparer ??= System.Collections.Generic.Comparer<TValue>.Default;

      var infIndex = -1;
      var infItem = default(TSource);
      var infValue = referenceValue;

      var supIndex = -1;
      var supItem = default(TSource);
      var supValue = referenceValue;

      var index = 0;

      foreach (var item in source)
      {
        var value = valueSelector(item);

        var cmp = comparer.Compare(value, referenceValue);

        if ((!proper ? cmp <= 0 : cmp < 0) && (infIndex < 0 || comparer.Compare(value, infValue) > 0))
        {
          infIndex = index;
          infItem = item;
          infValue = value;
        }

        if ((!proper ? cmp >= 0 : cmp > 0) && (supIndex < 0 || comparer.Compare(value, supValue) < 0))
        {
          supIndex = index;
          supItem = item;
          supValue = value;
        }

        index++;
      }

      return (infIndex, infItem, infValue, supIndex, supItem, supValue);
    }

    #endregion

    #region LastOrValue

    /// <summary>
    /// <para>Returns the last element and its index in <paramref name="source"/> that satisfies the <paramref name="predicate"/>, or <paramref name="value"/> if none is found (with index = -1).</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static (T item, int index) LastOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, T value, System.Func<T, int, bool>? predicate = null)
    {
      predicate ??= (e, i) => true;

      var result = (value, -1);

      var index = -1;

      foreach (var item in source)
        if (predicate(item, ++index))
          result = (item, index);

      return result;
    }

    #endregion

    #region Random

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
    public static System.Collections.Generic.IEnumerable<T> GetRandomElements<T>(this System.Collections.Generic.IEnumerable<T> source, double probability, System.Random? rng = null, int maxCount = int.MaxValue)
    {
      if (maxCount < 1) throw new System.ArgumentOutOfRangeException(nameof(maxCount));

      Units.Probability.AssertMember(probability, IntervalNotation.HalfOpenLeft); // Cannot be zero, but can be one.

      rng ??= System.Random.Shared;

      var count = 0;

      foreach (var item in source)
        if (rng.NextDouble() < probability)
        {
          yield return item;

          if (++count >= maxCount) break;
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
    public static T Random<T>(this System.Collections.Generic.IEnumerable<T> source, System.Random? rng = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var (item, index) = source.RandomOrValue(default!, rng);

      if (index > -1)
        return item;

      throw new System.ArgumentOutOfRangeException(nameof(source));
    }

    /// <summary>
    /// <para>Randomize an element and its index in <paramref name="source"/>, or <paramref name="value"/> if none is found (with index = -1). Uses the specified <paramref name="rng"/> (shared if null).</para>
    /// <para><seealso href="http://stackoverflow.com/questions/648196/random-row-from-linq-to-sql/648240#648240"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <param name="rng">The random number generator to use.</param>
    /// <returns></returns>
    public static (T Item, int Index) RandomOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, T value, System.Random? rng = null)
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
    public static bool TryRandom<T>(this System.Collections.Generic.IEnumerable<T> source, out T result, System.Random? rng = null)
    {
      try
      {
        result = source.Random(rng);
        return true;
      }
      catch { }

      result = default!;
      return false;
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
    public static System.Collections.Generic.IEnumerable<TResult> SelectWhere<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, TResult> selector, System.Func<TSource, int, TResult, bool> predicate)
    {
      System.ArgumentNullException.ThrowIfNull(predicate);
      System.ArgumentNullException.ThrowIfNull(selector);

      var index = 0;

      foreach (var item in source)
        if (selector(item, index) is var select && predicate(item, index++, select))
          yield return select;
    }

    #endregion

    #region SequenceHashCode

    /// <summary>
    /// <para>Computes a hash code, representing all elements in <paramref name="source"/>, using the .NET built-in <see cref="System.HashCode"/> functionality.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static int SequenceHashCode<T>(this System.Collections.Generic.IEnumerable<T> source)
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
    /// <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    /// <exception cref="System.InvalidOperationException"></exception>
    public static (T Item, int Index) SingleOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, T value, System.Func<T, int, bool>? predicate = null)
    {
      predicate ??= (e, i) => true;

      var item = value;
      var index = -1;

      foreach (var element in source)
      {
        if (predicate(element, index))
        {
          index++;
          item = element;

          if (index > 0) throw new System.InvalidOperationException("The sequence has more than one element.");
        }
      }

      return (item, index);
    }

    #endregion

    #region SkipEvery

    /// <summary>Creates a new sequence by skipping the <paramref name="option"/> at every <paramref name="interval"/> from <paramref name="source"/>.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<T> SkipEvery<T>(this System.Collections.Generic.IEnumerable<T> source, int index, int interval)
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
    public static System.Collections.Generic.IEnumerable<T> SkipLastWhile<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, int, bool> predicate)
    {
      System.ArgumentNullException.ThrowIfNull(predicate);

      var buffer = new System.Collections.Generic.Queue<T>();

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
    public static System.Collections.Generic.IEnumerable<T> TakeEvery<T>(this System.Collections.Generic.IEnumerable<T> source, int index, int interval)
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
    public static System.Collections.Generic.IEnumerable<T> TakeLastWhile<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, int, bool> predicate)
    {
      System.ArgumentNullException.ThrowIfNull(predicate);

      var buffer = new System.Collections.Generic.List<T>();

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
    public static System.Collections.Generic.SortedDictionary<TKey, TValue> ToSortedDictionary<TSource, TKey, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, TKey> keySelector, System.Func<TSource, int, TValue> valueSelector, System.Collections.Generic.IComparer<TKey>? comparer = null)
      where TKey : notnull
    {
      System.ArgumentNullException.ThrowIfNull(keySelector);
      System.ArgumentNullException.ThrowIfNull(valueSelector);

      var sd = new System.Collections.Generic.SortedDictionary<TKey, TValue>(comparer ?? System.Collections.Generic.Comparer<TKey>.Default);

      using var e = source.GetEnumerator();

      for (var index = 0; e.MoveNext(); index++)
        sd.Add(keySelector(e.Current, index), valueSelector(e.Current, index));

      return sd;
    }

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
      => source.ToSortedDictionary((e, i) => e.Key, (e, i) => e.Value, comparer);

    #region ToSpanBuilder

    public static SpanBuilder<T> ToSpanBuilder<T>(this System.Collections.Generic.IEnumerable<T> source)
    {
      var sb = new SpanBuilder<T>();
      foreach (var item in source)
        sb.Append(item);
      return sb;
    }

    #endregion

    /// <summary>
    /// <para>Creates a new two-dimensional array with the specified sizes, and then fills the target (from the source) in a 'dimension 0'-major order.</para>
    /// </summary>
    /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
    /// <exception cref="System.ArgumentNullException"/>
    public static T[,] ToTwoDimensionalArray<T>(this System.Collections.Generic.IEnumerable<T> source, int length0, int length1)
    {
      using var e = source.ThrowOnNull().GetEnumerator();

      var target = new T[length0, length1];

      for (var i0 = 0; i0 < length0; i0++)
        for (var i1 = 0; i1 < length1; i1++)
          target[i0, i1] = e.MoveNext() ? e.Current : default!;

      return target;
    }

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
  }
}
