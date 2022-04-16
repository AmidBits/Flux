namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Creates a new sequence by skipping elements in the sequence until the predicate is satisfied, and also skips the first element that satisfies the predicate. This version also passes the source index into the predicate.</summary>
    public static System.Collections.Generic.IEnumerable<TSource> SkipUntil<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, bool> predicate)
    {
      if (source == null) throw new System.ArgumentNullException(nameof(source));
      if (predicate == null) throw new System.ArgumentNullException(nameof(predicate));

      using var e = source.GetEnumerator();

      var index = 0;

      do
      {
        if (!e.MoveNext()) yield break;
      }
      while (!predicate(e.Current, index++));

      while (e.MoveNext())
        yield return e.Current;
    }
    /// <summary>Creates a new sequence by skipping elements in the sequence until the predicate is satisfied, and also skips the first element that satisfies the predicate.</summary>
    public static System.Collections.Generic.IEnumerable<TSource> SkipUntil<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, bool> predicate)
      => SkipUntil(source, (e, i) => predicate(e));
  }
}
