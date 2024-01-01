namespace Flux
{
  public static partial class Fx
  {
    public static System.Collections.Generic.IEnumerable<(TAccumulate aggregate, TSource element, int index)> RunningAggregate<TSource, TAccumulate>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TAccumulate> seedFactory, System.Func<TAccumulate, TSource, int, TAccumulate> accumulator)
    {
      System.ArgumentNullException.ThrowIfNull(seedFactory);
      System.ArgumentNullException.ThrowIfNull(accumulator);

      var index = 0;

      var result = seedFactory();

      foreach (var item in source.ThrowOnNull())
      {
        result = accumulator(result, item, index);

        yield return (result, item, index);

        index++;
      }
    }
  }
}
