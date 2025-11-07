namespace Flux
{
  public static partial class IEnumerablePartitioning
  {
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
    public static TResult AggregateTuple2<TSource, TAccumulate, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, TAccumulate seed, bool tupleWrap, System.Func<TAccumulate, TSource, TSource, int, TAccumulate> aggregateComputor, System.Func<TAccumulate, int, TResult> resultSelector)
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
    public static TResult AggregateTuple3<TSource, TAccumulate, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, TAccumulate seed, int tupleWrap, System.Func<TAccumulate, TSource, TSource, TSource, int, TAccumulate> aggregateComputor, System.Func<TAccumulate, int, TResult> resultSelector)
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

    public static System.Collections.Generic.IEnumerable<(TSource, TTarget)> CartesianProductFor2<TSource, TTarget>(this System.Collections.Generic.IEnumerable<TSource> source, System.Collections.Generic.IEnumerable<TTarget> target)
      => source.SelectMany(s => target, (s, t) => (s, t));

    /// <summary>
    /// <para>Creates a new sequence with the cartesian product of all elements in the specified sequences.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="targets"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> CartesianProduct<T>(this System.Collections.Generic.IEnumerable<T> source, params System.Collections.Generic.IEnumerable<T>[] targets)
    {
      System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> emptyProduct = [[]];

      return System.Linq.Enumerable.Aggregate(
        new System.Collections.Generic.IEnumerable<T>[] { source }.Concat(targets)
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
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> CartesianProduct<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> targets)
    {
      System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> emptyProduct = new[] { System.Linq.Enumerable.Empty<T>() };

      return System.Linq.Enumerable.Aggregate(
        new System.Collections.Generic.IEnumerable<T>[] { source }.Concat(targets)
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

    #region GroupAdjacent

    /// <summary>
    /// <para>Creates a new sequence of equal (based on the specified keySelector) adjacent (consecutive) items grouped together as a key and a list. Uses the specified equality comparer.</para>
    /// </summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<System.Linq.IGrouping<TKey, TSource>> GroupAdjacent<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Collections.Generic.IEqualityComparer<TKey>? equalityComparer = null)
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
    public static System.Collections.Generic.IEnumerable<TResult> PartitionNgram<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, int size, System.Func<System.Collections.Generic.IEnumerable<TSource>, int, TResult> resultSelector)
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
    public static System.Collections.Generic.IEnumerable<TResult> PartitionTuple<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, int tupleSize, int tupleWrap, System.Func<System.Collections.Generic.IReadOnlyList<TSource>, int, TResult> resultSelector)
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
    public static System.Collections.Generic.IEnumerable<TResult> PartitionTuple2<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, bool wrap, System.Func<TSource, TSource, int, TResult> resultSelector)
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

    /// <summary>
    /// <para>Create a new sequence of 3-tuples, project with a <paramref name="resultSelector"/>, and optionally overlap by wrap-around to the first or the second element.</para>
    /// <see href="https://en.wikipedia.org/wiki/Tuple"/>
    /// </summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<TResult> PartitionTuple3<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, int wrap, System.Func<TSource, TSource, TSource, int, TResult> resultSelector)
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
    public static System.Collections.Generic.IEnumerable<TResult> PartitionWindowed<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, int size, int step, bool includeTrailing, System.Func<System.Collections.Generic.List<TSource>, TResult> resultSelector)
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

    #region ZipEx

    /// <summary>This version of Zip runs over all elements in all sequences.</summary>
    public static System.Collections.Generic.IEnumerable<TResult> ZipEx<T, TResult>(System.Func<T[], TResult> resultSelector, params System.Collections.Generic.IEnumerable<T>[] sequences)
    {
      System.ArgumentNullException.ThrowIfNull(resultSelector);

      var e = new System.Collections.Generic.IEnumerator<T>[sequences.Length];

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

  /// <summary>Class used to group adjacent elements in a sequence. Derives from System.Linq.IGrouping<TKey, TSource>.</summary>
  public sealed class Grouping<TKey, TElement>
    : System.Linq.IGrouping<TKey, TElement>
    , System.Collections.Generic.IList<TElement>
    , System.Collections.Generic.IEnumerable<TElement>
    where TKey : notnull
  {
    private readonly TKey m_key;
    private readonly System.Collections.Generic.List<TElement> m_elements = new();

    public Grouping(TKey key) => m_key = key;
    public Grouping(TKey key, TElement source) : this(key) => m_elements.Add(source);

    #region Implemented interfaces

    // IGrouping
    public TKey Key => m_key;

    // IList
    public TElement this[int index] { get => m_elements[index]; set => m_elements[index] = value; }
    public void Add(TElement item) => m_elements.Add(item);
    public void Clear() => m_elements.Clear();
    public bool Contains(TElement item) => m_elements.Contains(item);
    public void CopyTo(TElement[] array, int arrayIndex) => m_elements.CopyTo(array, arrayIndex);
    public int Count => m_elements.Count;
    public int IndexOf(TElement item) => m_elements.IndexOf(item);
    public void Insert(int index, TElement item) => m_elements.Insert(index, item);
    public bool IsReadOnly => false;
    public bool Remove(TElement item) => m_elements.Remove(item);
    public void RemoveAt(int index) => m_elements.RemoveAt(index);

    // IEnumerable
    public System.Collections.Generic.IEnumerator<TElement> GetEnumerator() => m_elements.GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => m_elements.GetEnumerator();

    #endregion // Implemented interfaces
  }
}
