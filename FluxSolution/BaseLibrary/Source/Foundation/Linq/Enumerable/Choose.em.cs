namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Return a new sequence of elements based on the selector (with indexed parameter).</summary>
    public static System.Collections.Generic.IEnumerable<TResult> Choose<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, bool> predicate, System.Func<TSource, int, TResult> resultSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      var index = 0;

      using var e = source.GetEnumerator();

      while (e.MoveNext())
      {
        var current = e.Current;

        if (predicate(current, index))
          yield return resultSelector(current, index);

        index++;
      }
    }
  }
}
