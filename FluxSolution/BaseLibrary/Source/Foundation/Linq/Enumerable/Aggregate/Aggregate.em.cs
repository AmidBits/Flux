namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Applies an accumulator over a sequence. The specified seed value is used as the inital accumulator value, and the specified functions are used to select the accumulated value and result value, respectively.</summary>
    /// <remarks>Unlike the LINQ versions, this one also includes the ordinal index of the element while aggregating.</remarks>
    public static TResult Aggregate<TSource, TAccumulate, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TAccumulate> initiator, System.Func<TAccumulate, TSource, int, TAccumulate> aggregator, System.Func<TAccumulate, int, TResult> resultSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (initiator is null) throw new System.ArgumentNullException(nameof(initiator));
      if (aggregator is null) throw new System.ArgumentNullException(nameof(aggregator));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      var index = 0;

      var aggregate = initiator();

      using var e = source.GetEnumerator();

      while (e.MoveNext())
      {
        aggregate = aggregator(aggregate, e.Current, index);

        index++;
      }

      return resultSelector(aggregate, index);
    }
  }
}
