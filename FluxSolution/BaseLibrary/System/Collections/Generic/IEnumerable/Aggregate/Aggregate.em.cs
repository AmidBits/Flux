namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Applies an accumulator over a sequence. The specified seed value is used as the inital accumulator value, and the specified functions are used to select the accumulated value and result value, respectively.</summary>
    /// <remarks>Unlike the <see cref="System.Linq.Enumerable"/> versions, this one also includes the ordinal index of the element while aggregating.</remarks>
    public static TResult Aggregate<TSource, TAccumulate, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TAccumulate> seedFactory, System.Func<TAccumulate, TSource, int, TAccumulate> aggregator, System.Func<TAccumulate, int, TResult> resultSelector)
    {
      System.ArgumentNullException.ThrowIfNull(seedFactory);
      System.ArgumentNullException.ThrowIfNull(aggregator);
      System.ArgumentNullException.ThrowIfNull(resultSelector);

      var index = 0;

      var result = seedFactory();

      foreach (var item in source.ThrowOnNull())
      {
        result = aggregator(result, item, index);

        index++;
      }

      return resultSelector(result, index);
    }
  }
}
