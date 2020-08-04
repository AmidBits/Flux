namespace Flux
{
  public static partial class XtensionsCollections
  {
    public static System.Collections.Generic.IEnumerable<(TAccumulate cumulative, TSource element, int index)> RunningAggregate<TSource, TAccumulate>(this System.Collections.Generic.IEnumerable<TSource> source, TAccumulate initial, System.Func<TAccumulate, TSource, int, TAccumulate> aggregator)
    {
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
