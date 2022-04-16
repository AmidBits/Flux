namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Creates a sequence of staggered (by one element) n-tuples.</summary>
    /// <param name="tupleSize">The number of elements in each tuple.</param>
    /// <param name="tupleWrap">The number of staggered wrap-around tuples to return, beyond the last element in the source sequence.</param>
    /// <param name="resultSelector">Allows the result of each tuple to be processed.</param>
    /// <returns>A sequence of n-tuples staggered by one element, optionally extending the sequence by the specified overflow.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Tuple"/>
    public static System.Collections.Generic.IEnumerable<TResult> PartitionTuple<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, int tupleSize, int tupleWrap, System.Func<System.Collections.Generic.IReadOnlyList<TSource>, int, TResult> resultSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      if (tupleSize < 2) throw new System.ArgumentOutOfRangeException(nameof(tupleSize));
      if (tupleWrap < 0 || tupleWrap >= tupleSize) throw new System.ArgumentException($@"A {tupleSize}-tuple can only wrap up to {tupleSize - 1} elements.");

      using var e = source.GetEnumerator();

      var start = new System.Collections.Generic.List<TSource>(tupleSize);

      if (e.MoveNext())
      {
        do
        {
          start.Add(e.Current);
        }
        while (start.Count < tupleSize && e.MoveNext());

        if (start.Count == tupleSize)
        {
          var tuple = new System.Collections.Generic.List<TSource>(start);

          var index = 0;

          yield return resultSelector(tuple, index++);

          while (e.MoveNext())
          {
            tuple.RemoveAt(0);
            tuple.Add(e.Current);
            yield return resultSelector(tuple, index++);
          }

          while (tupleWrap-- > 0)
          {
            tuple.RemoveAt(0);
            tuple.Add(start[0]);
            start.RemoveAt(0);
            yield return resultSelector(tuple, index++);
          }
        }
        else throw new System.ArgumentException($@"The sequence has only {start.Count} elements.", nameof(source));
      }
      else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
    }

    /// <summary>Create a new sequence of 2-tuples, project with a <paramref name="resultSelector"/>, and optionally overlap by wrap-around to the first element.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Tuple"/>
    public static System.Collections.Generic.IEnumerable<TResult> PartitionTuple2<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, bool wrap, System.Func<TSource, TSource, int, TResult> resultSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      using var e = source.GetEnumerator();

      if (e.MoveNext() && e.Current is var item1 && item1 is var back1)
        if (e.MoveNext())
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
        else throw new System.ArgumentException(@"The sequence has only 1 element.", nameof(source));
      else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
    }

    /// <summary>Create a new sequence of 3-tuples, project with a <paramref name="resultSelector"/>, and optionally overlap by wrap-around to the first or the second element.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Tuple"/>
    public static System.Collections.Generic.IEnumerable<TResult> PartitionTuple3<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, int wrap, System.Func<TSource, TSource, TSource, int, TResult> resultSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      if (wrap < 0 || wrap > 2) throw new System.ArgumentException(@"A 3-tuple can only wrap 0, 1 or 2 elements.", nameof(wrap));

      using var e = source.GetEnumerator();

      if (e.MoveNext() && e.Current is var item1 && item1 is var back2)
        if (e.MoveNext() && e.Current is var item2 && item2 is var back1)
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
          else throw new System.ArgumentException(@"The sequence has only 2 elements.", nameof(source));
        else throw new System.ArgumentException(@"The sequence has only 1 element.", nameof(source));
      else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
    }

    /// <summary>Create a new sequence of 4-tuples, project with a <paramref name="resultSelector"/>, and optionally overlap by wrap-around up to the third element.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Tuple"/>
    public static System.Collections.Generic.IEnumerable<TResult> PartitionTuple4<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, int wrap, System.Func<TSource, TSource, TSource, TSource, int, TResult> resultSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      if (wrap < 0 || wrap > 3) throw new System.ArgumentException(@"A 4-tuple can only wrap 0, 1, 2 or 3 elements.", nameof(wrap));

      using var e = source.GetEnumerator();

      if (e.MoveNext() && e.Current is var item1 && item1 is var back3)
        if (e.MoveNext() && e.Current is var item2 && item2 is var back2)
          if (e.MoveNext() && e.Current is var item3 && item3 is var back1)
            if (e.MoveNext())
            {
              var index = 0;

              do
              {
                var current = e.Current;

                yield return resultSelector(back3, back2, back1, current, index++);

                back3 = back2;
                back2 = back1;
                back1 = current;
              }
              while (e.MoveNext());

              if (wrap >= 1) yield return resultSelector(back3, back2, back1, item1, index++);
              if (wrap >= 2) yield return resultSelector(back2, back1, item1, item2, index++);
              if (wrap == 3) yield return resultSelector(back1, item1, item2, item3, index++);
            }
            else throw new System.ArgumentException(@"The sequence has only 3 elements.", nameof(source));
          else throw new System.ArgumentException(@"The sequence has only 2 elements.", nameof(source));
        else throw new System.ArgumentException(@"The sequence has only 1 element.", nameof(source));
      else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
    }

    /// <summary>Create a new sequence of 5-tuples, project with a <paramref name="resultSelector"/>, and optionally overlap by wrap-around up to the fourth element.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Tuple"/>
    public static System.Collections.Generic.IEnumerable<TResult> PartitionTuple5<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, int wrap, System.Func<TSource, TSource, TSource, TSource, TSource, int, TResult> resultSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      if (wrap < 0 || wrap > 4) throw new System.ArgumentException(@"A 5-tuple can only wrap 0, 1, 2, 3 or 4 elements.", nameof(wrap));

      using var e = source.GetEnumerator();

      if (e.MoveNext() && e.Current is TSource item1 && item1 is var back4)
        if (e.MoveNext() && e.Current is TSource item2 && item2 is var back3)
          if (e.MoveNext() && e.Current is TSource item3 && item3 is var back2)
            if (e.MoveNext() && e.Current is TSource item4 && item4 is var back1)
              if (e.MoveNext())
              {
                var index = 0;

                do
                {
                  var current = e.Current;

                  yield return resultSelector(back4, back3, back2, back1, current, index++);

                  back4 = back3;
                  back3 = back2;
                  back2 = back1;
                  back1 = current;
                }
                while (e.MoveNext());

                if (wrap >= 1) yield return resultSelector(back4, back3, back2, back1, item1, index++);
                if (wrap >= 2) yield return resultSelector(back3, back2, back1, item1, item2, index++);
                if (wrap >= 3) yield return resultSelector(back2, back1, item1, item2, item3, index++);
                if (wrap == 4) yield return resultSelector(back1, item1, item2, item3, item4, index++);
              }
              else throw new System.ArgumentException(@"The sequence has only 4 elements.", nameof(source));
            else throw new System.ArgumentException(@"The sequence has only 3 elements.", nameof(source));
          else throw new System.ArgumentException(@"The sequence has only 2 elements.", nameof(source));
        else throw new System.ArgumentException(@"The sequence has only 1 element.", nameof(source));
      else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
    }

    /// <summary>Create a new sequence of 5-tuples, project with a <paramref name="resultSelector"/>, and optionally overlap by wrap-around up to the fourth element.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Tuple"/>
    public static System.Collections.Generic.IEnumerable<TResult> PartitionTuple6<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, int wrap, System.Func<TSource, TSource, TSource, TSource, TSource, TSource, int, TResult> resultSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      if (wrap < 0 || wrap > 5) throw new System.ArgumentException(@"A 6-tuple can only wrap 0, 1, 2, 3, 4 or 5 elements.", nameof(wrap));

      using var e = source.GetEnumerator();

      if (e.MoveNext() && e.Current is TSource item1 && item1 is var back5)
        if (e.MoveNext() && e.Current is TSource item2 && item2 is var back4)
          if (e.MoveNext() && e.Current is TSource item3 && item3 is var back3)
            if (e.MoveNext() && e.Current is TSource item4 && item4 is var back2)
              if (e.MoveNext() && e.Current is TSource item5 && item5 is var back1)
                if (e.MoveNext())
                {
                  var index = 0;

                  do
                  {
                    var current = e.Current;

                    yield return resultSelector(back5, back4, back3, back2, back1, current, index++);

                    back5 = back4;
                    back4 = back3;
                    back3 = back2;
                    back2 = back1;
                    back1 = current;
                  }
                  while (e.MoveNext());

                  if (wrap >= 1) yield return resultSelector(back5, back4, back3, back2, back1, item1, index++);
                  if (wrap >= 2) yield return resultSelector(back4, back3, back2, back1, item1, item2, index++);
                  if (wrap >= 3) yield return resultSelector(back3, back2, back1, item1, item2, item3, index++);
                  if (wrap >= 4) yield return resultSelector(back2, back1, item1, item2, item3, item4, index++);
                  if (wrap == 5) yield return resultSelector(back1, item1, item2, item3, item4, item5, index++);
                }
                else throw new System.ArgumentException(@"The sequence has only 5 elements.", nameof(source));
              else throw new System.ArgumentException(@"The sequence has only 4 elements.", nameof(source));
            else throw new System.ArgumentException(@"The sequence has only 3 elements.", nameof(source));
          else throw new System.ArgumentException(@"The sequence has only 2 elements.", nameof(source));
        else throw new System.ArgumentException(@"The sequence has only 1 element.", nameof(source));
      else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
    }
  }
}
