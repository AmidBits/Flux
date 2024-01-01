namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Yields a new sequence of elements from <paramref name="source"/> based on <paramref name="predicate"/> and <paramref name="selector"/>.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<TResult> SelectWhere<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, bool> predicate, System.Func<TSource, int, TResult> selector)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));
      if (selector is null) throw new System.ArgumentNullException(nameof(selector));

      var index = 0;

      foreach (var item in source)
      {
        if (predicate(item, index))
          yield return selector(item, index);

        index++;
      }
    }
  }
}
