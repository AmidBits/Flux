namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Creates a new sequence by taking elements from the sequence until the predicate is satisfied, and also takes the first element that satisfies the predicate. This version also passes the source index into the predicate.</summary>
    public static System.Collections.Generic.IEnumerable<TSource> TakeUntil<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, bool> predicate)
    {
      if (source == null) throw new System.ArgumentNullException(nameof(source));
      if (predicate == null) throw new System.ArgumentNullException(nameof(predicate));

      var index = 0;

      using var e = source.GetEnumerator();

      while (e.MoveNext())
      {
        var current = e.Current;

        yield return current;

        if (predicate(current, index++))
          yield break;
      }
    }
  }
}
