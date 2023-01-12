namespace Flux
{
  public static partial class Enumerable
  {
    public static System.Collections.Generic.IEnumerable<(TAccumulate aggregate, TSource element, int index)> RunningAggregate<TSource, TAccumulate>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TAccumulate> initiator, System.Func<TAccumulate, TSource, int, TAccumulate> accumulator)
    {
      if (initiator is null) throw new System.ArgumentNullException(nameof(initiator));
      if (accumulator is null) throw new System.ArgumentNullException(nameof(accumulator));

      var index = 0;

      var aggregate = initiator();

      using var e = source.ThrowIfNull().GetEnumerator();

      while (e.MoveNext())
      {
        aggregate = accumulator(aggregate, e.Current, index);

        yield return (aggregate, e.Current, index);

        index++;
      }
    }
  }
}
