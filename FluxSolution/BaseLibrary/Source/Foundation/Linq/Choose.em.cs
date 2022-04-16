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

      foreach (var element in source)
      {
        var chosen = predicate(element, index);

        if (chosen)
          yield return resultSelector(element, index);

        index++;
      }
    }
    /// <summary>Return a new sequence of elements based on the selector (without indexed parameter).</summary>
    public static System.Collections.Generic.IEnumerable<TResult> Choose<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, bool> predicate, System.Func<TSource, TResult> resultSelector)
      => Choose(source, (e, i) => predicate(e), (e, i) => resultSelector(e));
  }
}
