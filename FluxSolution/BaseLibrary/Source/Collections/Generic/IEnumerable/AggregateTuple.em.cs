namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Creates a sequence of staggered (by one element) n-tuples.</summary>
    /// <param name="tupleSize">The number of elements in each tuple.</param>
    /// <param name="tupleWrap">The number of staggered wrap-around tuples to return, beyond the last element in the source sequence.</param>
    /// <param name="resultSelector">Allows the result of each tuple to be processed.</param>
    /// <returns>A sequence of n-tuples staggered by one element, optionally extending the sequence by the specified overflow.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Tuple"/>
    public static TResult AggregateTuple<TSource, TAccumulate, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, TAccumulate seed, int tupleSize, int tupleWrap, System.Func<TAccumulate, System.Collections.Generic.IReadOnlyList<TSource>, int, TAccumulate> computor, System.Func<TAccumulate, int, TResult> resultSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (computor is null) throw new System.ArgumentNullException(nameof(computor));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      if (tupleSize < 2) throw new System.ArgumentOutOfRangeException(nameof(tupleSize));
      if (tupleWrap < 0 || tupleWrap >= tupleSize) throw new System.ArgumentException($@"A {tupleSize}-tuple can only wrap up to {tupleSize - 1} elements.");

      using var e = source.GetEnumerator();

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

          var value = computor(seed, store, index++);

          while (e.MoveNext())
          {
            store.RemoveAt(0);
            store.Add(e.Current);
            value = computor(value, store, index++);
          }

          while (tupleWrap-- > 0)
          {
            store.RemoveAt(0);
            store.Add(start[0]);
            start.RemoveAt(0);
            value = computor(value, store, index++);
          }

          return resultSelector(value, index);
        }
        else throw new System.ArgumentException($@"The sequence has only {start.Count} elements.", nameof(source));
      }
      else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
    }
  }
}
