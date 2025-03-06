namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Applies an running accumulator over a sequence. I.e. statistics is computed as the aggregator is running its course.</para>
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TAccumulate"></typeparam>
    /// <param name="source"></param>
    /// <param name="seed"></param>
    /// <param name="func"></param>
    /// <returns></returns>
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
