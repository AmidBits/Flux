namespace Flux
{
  public static partial class Fx
  {
    public static System.Collections.Generic.IEnumerable<(TAccumulate aggregate, TSource element, int index)> RunningAggregate<TSource, TAccumulate>(this System.Collections.Generic.IEnumerable<TSource> source, TAccumulate seed, System.Func<TAccumulate, TSource, int, TAccumulate> func)
    {
      System.ArgumentNullException.ThrowIfNull(seed);
      System.ArgumentNullException.ThrowIfNull(func);

      var index = 0;

      var result = seed;

      foreach (var item in source.ThrowOnNull())
      {
        result = func(result, item, index);

        yield return (result, item, index);

        index++;
      }
    }
  }
}
