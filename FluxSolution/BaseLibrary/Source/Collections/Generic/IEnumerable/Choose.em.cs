namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Return a new sequence of elements based on the selector (with indexed parameter).</summary>
    public static System.Collections.Generic.IEnumerable<TResult> Choose<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, (bool chosen, TResult result)> resultSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      var index = 0;

      foreach (var element in source)
      {
        var (chosen, result) = resultSelector(element, index++);

        if (chosen) yield return result;
      }
    }
    /// <summary>Return a new sequence of elements based on the selector (without indexed parameter).</summary>
    public static System.Collections.Generic.IEnumerable<TResult> Choose<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, (bool, TResult)> ifAndResultSelector)
      => Choose(source, (element, index) => ifAndResultSelector(element));
  }
}
