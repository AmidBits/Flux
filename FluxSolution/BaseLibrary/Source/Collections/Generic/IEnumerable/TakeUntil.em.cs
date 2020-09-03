namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Creates a new sequence with all from the sequence up until the predicate is satisfied.</summary>
    public static System.Collections.Generic.IEnumerable<TSource> TakeUntil<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, bool> predicate)
    {
      if (source == null) throw new System.ArgumentNullException(nameof(source));
      if (predicate == null) throw new System.ArgumentNullException(nameof(predicate));

      foreach (var item in source)
      {
        yield return item;

        if (predicate(item))
          yield break;
      }
    }
  }
}
