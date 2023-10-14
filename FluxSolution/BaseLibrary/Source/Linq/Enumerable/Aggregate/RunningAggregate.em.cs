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

      foreach (var item in source.ThrowOnNull())
      {
        aggregate = accumulator(aggregate, item, index);

        yield return (aggregate, item, index);

        index++;
      }
    }
  }
}
