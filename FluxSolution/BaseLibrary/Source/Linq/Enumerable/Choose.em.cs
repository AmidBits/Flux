namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Return a new sequence of elements based on the selector (with indexed parameter).</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<TResult> Choose<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, bool> predicate, System.Func<TSource, int, TResult> resultSelector)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      var index = 0;

      foreach (var item in source)
      {
        if (predicate(item, index))
          yield return resultSelector(item, index);

        index++;
      }
    }
  }
}
