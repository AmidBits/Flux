namespace Flux
{
  public static partial class XtendCollections
  {
    public static System.Collections.Generic.IEnumerable<TResult> Choose<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, (bool, TResult)> ifAndResultSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (ifAndResultSelector is null) throw new System.ArgumentNullException(nameof(ifAndResultSelector));

      var index = 0;

      foreach (var element in source)
      {
        var (chosen, result) = ifAndResultSelector(element, index++);

        if (chosen) yield return result;
      }
    }
    public static System.Collections.Generic.IEnumerable<TResult> Choose<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, (bool, TResult)> ifAndResultSelector)
      => Choose(source, (element, index) => ifAndResultSelector(element));
  }
}
