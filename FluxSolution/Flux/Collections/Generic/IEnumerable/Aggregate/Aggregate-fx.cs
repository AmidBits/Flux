namespace Flux
{
  public static partial class IEnumerables
  {
    /// <summary>
    /// <para>Applies an accumulator over a sequence. The specified seed value is used as the inital accumulator value, and the specified functions are used to select the accumulated value and result value, respectively.</para>
    /// </summary>
    /// <remarks>
    /// <para>Unlike the <see cref="System.Linq.Enumerable"/> versions, this one also includes the ordinal index of the element while aggregating.</para>
    /// </remarks>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TAccumulate"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source"></param>
    /// <param name="seed"></param>
    /// <param name="func"></param>
    /// <param name="resultSelector"></param>
    /// <returns></returns>
    public static TResult Aggregate<TSource, TAccumulate, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, TAccumulate seed, System.Func<TAccumulate, TSource, int, TAccumulate> func, System.Func<TAccumulate, int, TResult> resultSelector)
    {
      System.ArgumentNullException.ThrowIfNull(seed);
      System.ArgumentNullException.ThrowIfNull(func);
      System.ArgumentNullException.ThrowIfNull(resultSelector);

      var index = -1;

      var accumulator = seed;

      foreach (var item in source.ThrowOnNull())
        accumulator = func(accumulator, item, ++index);

      return resultSelector(accumulator, index);
    }
  }
}
