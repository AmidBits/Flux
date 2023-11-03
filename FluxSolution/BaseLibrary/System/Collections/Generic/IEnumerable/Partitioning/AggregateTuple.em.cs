namespace Flux
{
  public static partial class ExtensionMethodsIEnumerableT
  {
    /// <summary>Creates a sequence of staggered (by one element) n-tuples.</summary>
    /// <param name="tupleSize">The number of elements in each tuple.</param>
    /// <param name="tupleWrap">The number of staggered wrap-around tuples to return, beyond the last element in the source sequence.</param>
    /// <param name="resultSelector">Allows the result of each tuple to be processed.</param>
    /// <returns>A sequence of n-tuples staggered by one element, optionally extending the sequence by the specified overflow.</returns>
    /// <exception cref="System.ArgumentNullException"/>
    /// <see cref="https://en.wikipedia.org/wiki/Tuple"/>
    public static TResult AggregateTuple<TSource, TAccumulate, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, TAccumulate seed, int tupleSize, int tupleWrap, System.Func<TAccumulate, System.Collections.Generic.IReadOnlyList<TSource>, int, TAccumulate> accumulator, System.Func<TAccumulate, int, TResult> resultSelector)
    {
      if (accumulator is null) throw new System.ArgumentNullException(nameof(accumulator));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      if (tupleSize < 2) throw new System.ArgumentOutOfRangeException(nameof(tupleSize));
      if (tupleWrap < 0 || tupleWrap >= tupleSize) throw new System.ArgumentException($@"A {tupleSize}-tuple can only wrap up to {tupleSize - 1} elements.");

      using var e = source.ThrowOnNull().GetEnumerator();

      var start = new System.Collections.Generic.List<TSource>();

      if (e.MoveNext())
      {
        do
        {
          start.Add(e.Current);
        }
        while (start.Count < tupleSize && e.MoveNext());

        if (start.Count == tupleSize)
        {
          var store = new System.Collections.Generic.List<TSource>(start);

          var index = 0;

          var value = accumulator(seed, store, index++);

          while (e.MoveNext())
          {
            store.RemoveAt(0);
            store.Add(e.Current);
            value = accumulator(value, store, index++);
          }

          while (tupleWrap-- > 0)
          {
            store.RemoveAt(0);
            store.Add(start[0]);
            start.RemoveAt(0);
            value = accumulator(value, store, index++);
          }

          return resultSelector(value, index);
        }
        else throw new System.ArgumentException($@"The sequence has only {start.Count} elements.", nameof(source));
      }
      else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
    }

    /// <summary>Creates a sequence of staggered (by one element) 2-tuple elements.</summary>
    /// <returns>A sequence of 2-tuples staggered by one element, optionally extending the sequence by the specified number of wraps.</returns>
    /// <exception cref="System.ArgumentException"/>
    /// <exception cref="System.ArgumentNullException"/>
    /// <see cref="https://en.wikipedia.org/wiki/Tuple"/>
    public static TResult AggregateTuple2<TSource, TAccumulate, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, TAccumulate seed, bool tupleWrap, System.Func<TAccumulate, TSource, TSource, int, TAccumulate> aggregateComputor, System.Func<TAccumulate, int, TResult> resultSelector)
    {
      if (aggregateComputor is null) throw new System.ArgumentNullException(nameof(aggregateComputor));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

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
    /// <see cref="https://en.wikipedia.org/wiki/Tuple"/>
    public static TResult AggregateTuple3<TSource, TAccumulate, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, TAccumulate seed, int tupleWrap, System.Func<TAccumulate, TSource, TSource, TSource, int, TAccumulate> aggregateComputor, System.Func<TAccumulate, int, TResult> resultSelector)
    {
      if (aggregateComputor is null) throw new System.ArgumentNullException(nameof(aggregateComputor));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

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

    ///// <summary>Creates a sequence of staggered (by one element) 4-tuple elements.</summary>
    ///// <returns>A sequence of 4-tuple elements staggered by one element, optionally extending the sequence by the specified number of wraps.</returns>
    ///// <see cref="https://en.wikipedia.org/wiki/Tuple"/>
    //public static TResult AggregateTuple4<TSource, TAccumulate, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, TAccumulate seed, int wrap, System.Func<TAccumulate, TSource, TSource, TSource, TSource, int, TAccumulate> aggregateComputor, System.Func<TAccumulate, int, TResult> resultSelector)
    //{
    //  if (source is null) throw new System.ArgumentNullException(nameof(source));
    //  if (aggregateComputor is null) throw new System.ArgumentNullException(nameof(aggregateComputor));
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

    //            seed = aggregateComputor(seed, back3, back2, back1, current, index++);

    //            back3 = back2;
    //            back2 = back1;
    //            back1 = current;
    //          }
    //          while (e.MoveNext());

    //          if (wrap >= 1)
    //            seed = aggregateComputor(seed, back3, back2, back1, item1, index++);
    //          if (wrap >= 2)
    //            seed = aggregateComputor(seed, back2, back1, item1, item2, index++);
    //          if (wrap == 3)
    //            seed = aggregateComputor(seed, back1, item1, item2, item3, index++);

    //          return resultSelector(seed, index);
    //        }
    //        else throw new System.ArgumentException($@"The sequence has only 3 elements.", nameof(source));
    //      else throw new System.ArgumentException($@"The sequence has only 2 elements.", nameof(source));
    //    else throw new System.ArgumentException($@"The sequence has only 1 element.", nameof(source));
    //  else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
    //}

    ///// <summary>Creates a sequence of staggered (by one element) 5-tuple elements.</summary>
    ///// <returns>A sequence of 5-tuple elements staggered by one element, optionally extending the sequence by the specified number of wraps.</returns>
    ///// <see cref="https://en.wikipedia.org/wiki/Tuple"/>
    //public static TResult AggregateTuple5<TSource, TAccumulate, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, TAccumulate seed, int wrap, System.Func<TAccumulate, TSource, TSource, TSource, TSource, TSource, int, TAccumulate> aggregateComputor, System.Func<TAccumulate, int, TResult> resultSelector)
    //{
    //  if (source is null) throw new System.ArgumentNullException(nameof(source));
    //  if (aggregateComputor is null) throw new System.ArgumentNullException(nameof(aggregateComputor));
    //  if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

    //  if (wrap < 0 || wrap > 4) throw new System.ArgumentException(@"A 5-tuple can only wrap 0, 1, 2, 3 or 4 elements.", nameof(wrap));

    //  using var e = source.GetEnumerator();

    //  if (e.MoveNext() && e.Current is var item1 && item1 is var back4)
    //    if (e.MoveNext() && e.Current is var item2 && item2 is var back3)
    //      if (e.MoveNext() && e.Current is var item3 && item3 is var back2)
    //        if (e.MoveNext() && e.Current is var item4 && item4 is var back1)
    //          if (e.MoveNext())
    //          {
    //            var index = 0;

    //            do
    //            {
    //              var current = e.Current;

    //              seed = aggregateComputor(seed, back4, back3, back2, back1, current, index++);

    //              back4 = back3;
    //              back3 = back2;
    //              back2 = back1;
    //              back1 = current;
    //            }
    //            while (e.MoveNext());

    //            if (wrap >= 1)
    //              seed = aggregateComputor(seed, back4, back3, back2, back1, item1, index++);
    //            if (wrap >= 2)
    //              seed = aggregateComputor(seed, back3, back2, back1, item1, item2, index++);
    //            if (wrap >= 3)
    //              seed = aggregateComputor(seed, back2, back1, item1, item2, item3, index++);
    //            if (wrap == 4)
    //              seed = aggregateComputor(seed, back1, item1, item2, item3, item4, index++);

    //            return resultSelector(seed, index);
    //          }
    //          else throw new System.ArgumentException($@"The sequence has only 4 elements.", nameof(source));
    //        else throw new System.ArgumentException($@"The sequence has only 3 elements.", nameof(source));
    //      else throw new System.ArgumentException($@"The sequence has only 2 elements.", nameof(source));
    //    else throw new System.ArgumentException($@"The sequence has only 1 element.", nameof(source));
    //  else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
    //}
  }
}
