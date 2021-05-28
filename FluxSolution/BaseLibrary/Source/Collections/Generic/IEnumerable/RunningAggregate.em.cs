namespace Flux
{
  public static partial class SystemCollectionsGenericIEnumerableEm
  {
    public static System.Collections.Generic.IEnumerable<(TAccumulate cumulative, TSource element, int index)> RunningAggregate<TSource, TAccumulate>(this System.Collections.Generic.IEnumerable<TSource> source, TAccumulate initial, System.Func<TAccumulate, TSource, int, TAccumulate> aggregator)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (aggregator is null) throw new System.ArgumentNullException(nameof(aggregator));

      var cumulative = initial;

      var index = 0;

      foreach (var item in source)
      {
        cumulative = aggregator(cumulative, item, index);

        yield return (cumulative, item, index);

        index++;
      }
    }

    public static int RunningAggregate<TSource, TAccumulate>(this System.Collections.Generic.IEnumerable<TSource> source, TAccumulate initial, System.Func<TAccumulate, TSource, int, TAccumulate> aggregator, out TAccumulate cumulative)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (aggregator is null) throw new System.ArgumentNullException(nameof(aggregator));

      cumulative = initial;

      var index = 0;

      foreach (var item in source)
      {
        cumulative = aggregator(cumulative, item, index);

        index++;
      }

      return index;
    }
  }
}
