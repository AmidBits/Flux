namespace Flux
{
  public static partial class XtensionsCollections
  {
    /// <summary>Creates a sequence of staggered (by one element) n-tuples.</summary>
    /// <param name="n">The number of elements in each tuple.</param>
    /// <param name="overlap">The number of staggered wrap-around tuples to return, beyond the last element in the source sequence.</param>
    /// <param name="resultSelector">Allows the result of each tuple to be processed.</param>
    /// <returns>A sequence of n-tuples staggered by one element, optionally extending the sequence by the specified overflow.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Tuple"/>
    public static System.Collections.Generic.IEnumerable<TResult> PartitionTuple<T, TResult>(this System.Collections.Generic.IEnumerable<T> source, int n, int overlap, System.Func<System.Collections.Generic.IEnumerable<T>, int, TResult> resultSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      if (n < 2) throw new System.ArgumentOutOfRangeException(nameof(overlap));
      if (overlap < 0 || overlap >= n) throw new System.ArgumentOutOfRangeException(nameof(overlap));

      using var e = source.GetEnumerator();

      var index = 0;

      var start = new System.Collections.Generic.Queue<T>(n);

      if (e.MoveNext())
      {
        do
        {
          start.Enqueue(e.Current);
        }
        while (start.Count < n && e.MoveNext());

        if (start.Count == n)
        {
          var tuple = new System.Collections.Generic.Queue<T>(start);

          yield return resultSelector(tuple, index++);

          while (e.MoveNext())
          {
            tuple.Dequeue();
            tuple.Enqueue(e.Current);
            yield return resultSelector(tuple, index++);
          }

          while (overlap-- > 0)
          {
            tuple.Dequeue();
            tuple.Enqueue(start.Dequeue());
            yield return resultSelector(tuple, index++);
          }
        }
        else throw new System.ArgumentException(@"Insufficient number of elements.", nameof(source));
      }
      else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));

      //while (--n > 0)
      //{
      //  if (e.MoveNext())
      //  {
      //    start.Enqueue(e.Current);
      //  }
      //  else throw new System.ArgumentException(start.Count == 0 ? @"The sequence is empty." : $"Insufficient number of elements.", nameof(source));
      //}

      //var tuple = new System.Collections.Generic.Queue<T>(start);

      //if (e.MoveNext())
      //{
      //  do
      //  {
      //    tuple.Enqueue(e.Current);
      //    yield return resultSelector(tuple, index++);
      //    tuple.Dequeue();
      //  }
      //  while (e.MoveNext());
      //}

      //while (overlap-- > 0)
      //{
      //  tuple.Enqueue(start.Dequeue());
      //  yield return resultSelector(tuple, index++);
      //  tuple.Dequeue();
      //}
    }

    //[System.Obsolete()]
    //public static System.Collections.Generic.IEnumerable<(T leading, T trailing)> PartitionPairs<T>(this System.Collections.Generic.IEnumerable<T> source, bool includeLastAndFirstPair)
    //  => source.PartitionTuple(includeLastAndFirstPair, (a, b, i) => (a, b));
    /// <summary>Create a new sequence of 2-tuples, project with a <paramref name="resultSelector"/>, and optionally overlap by wrap-around to the first element.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Tuple"/>
    public static System.Collections.Generic.IEnumerable<TResult> PartitionTuple<T, TResult>(this System.Collections.Generic.IEnumerable<T> source, bool overlap, System.Func<T, T, int, TResult> resultSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      using var e = source.GetEnumerator();

      var index = 0;

      if (e.MoveNext() && e.Current is var t1st)
      {
        var t1 = t1st;

        if (e.MoveNext())
        {
          do
          {
            var tc = e.Current;

            yield return resultSelector(t1, tc, index++);

            t1 = tc;
          }
          while (e.MoveNext());

          if (overlap) yield return resultSelector(t1, t1st, index++);
        }
        else throw new System.ArgumentException(@"The sequence has only one element.", nameof(source));
      }
      else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
    }

    //[System.Obsolete()]
    //public static System.Collections.Generic.IEnumerable<(T leading, T midling, T trailing)> PartitionTriplets<T>(this System.Collections.Generic.IEnumerable<T> source, int overlap)
    //  => source.PartitionTuple(overlap, (a, b, c, i) => (a, b, c));
    /// <summary>Create a new sequence of 3-tuples, project with a <paramref name="resultSelector"/>, and optionally overlap by wrap-around to the first or the second element.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Tuple"/>
    public static System.Collections.Generic.IEnumerable<TResult> PartitionTuple<T, TResult>(this System.Collections.Generic.IEnumerable<T> source, int overlap, System.Func<T, T, T, int, TResult> resultSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      if (overlap >= 0 && overlap <= 2)
      {
        using var e = source.GetEnumerator();

        var index = 0;

        if (e.MoveNext() && e.Current is T t1st)
        {
          var t1 = t1st;

          if (e.MoveNext() && e.Current is T t2nd)
          {
            var t2 = t2nd;

            if (e.MoveNext())
            {
              do
              {
                var tc = e.Current;

                yield return resultSelector(t1, t2, tc, index++);

                t1 = t2;
                t2 = tc;
              }
              while (e.MoveNext());

              if (overlap >= 1) yield return resultSelector(t1, t2, t1st, index++);
              if (overlap == 2) yield return resultSelector(t2, t1st, t2nd, index++);
            }
            else throw new System.ArgumentException(@"The sequence has only two elements.", nameof(source));
          }
          else throw new System.ArgumentException(@"The sequence has only one element.", nameof(source));
        }
        else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
      }
      else throw new System.ArgumentException(@"A 3-tuple can only overlap 0, 1 or 2 elements.", nameof(overlap));
    }

    /// <summary>Create a new sequence of 4-tuples, project with a <paramref name="resultSelector"/>, and optionally overlap by wrap-around up to the third element.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Tuple"/>
    public static System.Collections.Generic.IEnumerable<TResult> PartitionTuple<T, TResult>(this System.Collections.Generic.IEnumerable<T> source, int overlap, System.Func<T, T, T, T, int, TResult> resultSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      if (overlap >= 0 && overlap <= 3)
      {
        using var e = source.GetEnumerator();

        var index = 0;

        if (e.MoveNext() && e.Current is T t1st)
        {
          var t1 = t1st;

          if (e.MoveNext() && e.Current is T t2nd)
          {
            var t2 = t2nd;

            if (e.MoveNext() && e.Current is T t3rd)
            {
              var t3 = t3rd;

              if (e.MoveNext())
              {
                do
                {
                  var tc = e.Current;

                  yield return resultSelector(t1, t2, t3, tc, index++);

                  t1 = t2;
                  t2 = t3;
                  t3 = tc;
                }
                while (e.MoveNext());

                if (overlap >= 1) yield return resultSelector(t1, t2, t3, t1st, index++);
                if (overlap >= 2) yield return resultSelector(t2, t3, t1st, t2nd, index++);
                if (overlap >= 3) yield return resultSelector(t3, t1st, t2nd, t3rd, index++);
              }
              else throw new System.ArgumentException(@"The sequence has only three elements.", nameof(source));
            }
            else throw new System.ArgumentException(@"The sequence has only two elements.", nameof(source));
          }
          else throw new System.ArgumentException(@"The sequence has only one element.", nameof(source));
        }
        else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
      }
      else throw new System.ArgumentException(@"A 4-tuple can only overlap up to 3 elements.", nameof(overlap));
    }

    /// <summary>Create a new sequence of 5-tuples, project with a <paramref name="resultSelector"/>, and optionally overlap by wrap-around up to the fourth element.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Tuple"/>
    public static System.Collections.Generic.IEnumerable<TResult> PartitionTuple<T, TResult>(this System.Collections.Generic.IEnumerable<T> source, int overlap, System.Func<T, T, T, T, T, int, TResult> resultSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      if (overlap >= 0 && overlap <= 4)
      {
        using var e = source.GetEnumerator();

        var index = 0;

        if (e.MoveNext() && e.Current is T t1st)
        {
          var t1 = t1st;

          if (e.MoveNext() && e.Current is T t2nd)
          {
            var t2 = t2nd;

            if (e.MoveNext() && e.Current is T t3rd)
            {
              var t3 = t3rd;

              if (e.MoveNext() && e.Current is T t4th)
              {
                var t4 = t4th;

                if (e.MoveNext())
                {
                  do
                  {
                    var tc = e.Current;

                    yield return resultSelector(t1, t2, t3, t4, tc, index++);

                    t1 = t2;
                    t2 = t3;
                    t3 = t4;
                    t4 = tc;
                  }
                  while (e.MoveNext());

                  if (overlap >= 1) yield return resultSelector(t1, t2, t3, t4, t1st, index++);
                  if (overlap >= 2) yield return resultSelector(t2, t3, t4, t1st, t2nd, index++);
                  if (overlap >= 3) yield return resultSelector(t3, t4, t1st, t2nd, t3rd, index++);
                  if (overlap == 4) yield return resultSelector(t4, t1st, t2nd, t3rd, t4th, index++);
                }
                else throw new System.ArgumentException(@"The sequence has only four elements.", nameof(source));
              }
              else throw new System.ArgumentException(@"The sequence has only three elements.", nameof(source));
            }
            else throw new System.ArgumentException(@"The sequence has only two elements.", nameof(source));
          }
          else throw new System.ArgumentException(@"The sequence has only one element.", nameof(source));
        }
        else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
      }
      else throw new System.ArgumentException(@"A 5-tuple can only overlap up to 4 elements.", nameof(overlap));
    }

    /// <summary>Create a new sequence of 6-tuples, project with a <paramref name="resultSelector"/>, and optionally overlap by wrap-around up to the fifth element.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Tuple"/>
    public static System.Collections.Generic.IEnumerable<TResult> PartitionTuple<T, TResult>(this System.Collections.Generic.IEnumerable<T> source, int overlap, System.Func<T, T, T, T, T, T, int, TResult> resultSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      if (overlap >= 0 && overlap <= 5)
      {
        using var e = source.GetEnumerator();

        var index = 0;

        if (e.MoveNext() && e.Current is T t1st)
        {
          var t1 = t1st;

          if (e.MoveNext() && e.Current is T t2nd)
          {
            var t2 = t2nd;

            if (e.MoveNext() && e.Current is T t3rd)
            {
              var t3 = t3rd;

              if (e.MoveNext() && e.Current is T t4th)
              {
                var t4 = t4th;

                if (e.MoveNext() && e.Current is T t5th)
                {
                  var t5 = t5th;

                  if (e.MoveNext())
                  {
                    do
                    {
                      var tc = e.Current;

                      yield return resultSelector(t1, t2, t3, t4, t5, tc, index++);

                      t1 = t2;
                      t2 = t3;
                      t3 = t4;
                      t4 = t5;
                      t5 = tc;
                    }
                    while (e.MoveNext());

                    if (overlap >= 1) yield return resultSelector(t1, t2, t3, t4, t5, t1st, index++);
                    if (overlap >= 2) yield return resultSelector(t2, t3, t4, t5, t1st, t2nd, index++);
                    if (overlap >= 3) yield return resultSelector(t3, t4, t5, t1st, t2nd, t3rd, index++);
                    if (overlap >= 4) yield return resultSelector(t4, t5, t1st, t2nd, t3rd, t4th, index++);
                    if (overlap == 5) yield return resultSelector(t5, t1st, t2nd, t3rd, t4th, t5th, index++);
                  }
                  else throw new System.ArgumentException(@"The sequence has only five elements.", nameof(source));
                }
                else throw new System.ArgumentException(@"The sequence has only four elements.", nameof(source));
              }
              else throw new System.ArgumentException(@"The sequence has only three elements.", nameof(source));
            }
            else throw new System.ArgumentException(@"The sequence has only two elements.", nameof(source));
          }
          else throw new System.ArgumentException(@"The sequence has only one element.", nameof(source));
        }
        else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
      }
      else throw new System.ArgumentException(@"A 6-tuple can only overlap up to 5 elements.", nameof(overlap));
    }

    /// <summary>Create a new sequence of 7-tuples, project with a <paramref name="resultSelector"/>, and optionally overlap by wrap-around up to the sixth element.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Tuple"/>
    public static System.Collections.Generic.IEnumerable<TResult> PartitionTuple<T, TResult>(this System.Collections.Generic.IEnumerable<T> source, int overlap, System.Func<T, T, T, T, T, T, T, int, TResult> resultSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      if (overlap >= 0 && overlap <= 6)
      {
        using var e = source.GetEnumerator();

        var index = 0;

        if (e.MoveNext() && e.Current is T t1st)
        {
          var t1 = t1st;

          if (e.MoveNext() && e.Current is T t2nd)
          {
            var t2 = t2nd;

            if (e.MoveNext() && e.Current is T t3rd)
            {
              var t3 = t3rd;

              if (e.MoveNext() && e.Current is T t4th)
              {
                var t4 = t4th;

                if (e.MoveNext() && e.Current is T t5th)
                {
                  var t5 = t5th;

                  if (e.MoveNext() && e.Current is T t6th)
                  {
                    var t6 = t6th;

                    if (e.MoveNext())
                    {
                      do
                      {
                        var tc = e.Current;

                        yield return resultSelector(t1, t2, t3, t4, t5, t6, tc, index++);

                        t1 = t2;
                        t2 = t3;
                        t3 = t4;
                        t4 = t5;
                        t5 = t6;
                        t6 = tc;
                      }
                      while (e.MoveNext());

                      if (overlap >= 1) yield return resultSelector(t1, t2, t3, t4, t5, t6, t1st, index++);
                      if (overlap >= 2) yield return resultSelector(t2, t3, t4, t5, t6, t1st, t2nd, index++);
                      if (overlap >= 3) yield return resultSelector(t3, t4, t5, t6, t1st, t2nd, t3rd, index++);
                      if (overlap >= 4) yield return resultSelector(t4, t5, t6, t1st, t2nd, t3rd, t4th, index++);
                      if (overlap >= 5) yield return resultSelector(t5, t6, t1st, t2nd, t3rd, t4th, t5th, index++);
                      if (overlap == 6) yield return resultSelector(t6, t1st, t2nd, t3rd, t4th, t5th, t6th, index++);
                    }
                    else throw new System.ArgumentException(@"The sequence has only six elements.", nameof(source));
                  }
                  else throw new System.ArgumentException(@"The sequence has only five elements.", nameof(source));
                }
                else throw new System.ArgumentException(@"The sequence has only four elements.", nameof(source));
              }
              else throw new System.ArgumentException(@"The sequence has only three elements.", nameof(source));
            }
            else throw new System.ArgumentException(@"The sequence has only two elements.", nameof(source));
          }
          else throw new System.ArgumentException(@"The sequence has only one element.", nameof(source));
        }
        else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
      }
      else throw new System.ArgumentException(@"A 7-tuple can only overlap up to 6 elements.", nameof(overlap));
    }
  }
}
