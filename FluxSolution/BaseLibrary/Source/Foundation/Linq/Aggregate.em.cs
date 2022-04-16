namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Applies an accumulator over a sequence. The specified seed value is used as the inital accumulator value, and the specified functions are used to select the accumulated value and result value, respectively.</summary>
    /// <remarks>Unlike the LINQ versions, this one also includes the ordinal index of the element while aggregating.</remarks>
    public static TResult Aggregate<TSource, TAccumulate, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, TAccumulate seed, System.Func<TAccumulate, TSource, int, TAccumulate> func, System.Func<TAccumulate, int, TResult> resultSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (func is null) throw new System.ArgumentNullException(nameof(func));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      using var e = source.GetEnumerator();

      var index = 0;

      while (e.MoveNext())
        seed = func(seed, e.Current, index++);

      return resultSelector(seed, index);
    }
  }
}
