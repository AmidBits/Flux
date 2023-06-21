namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Creates a new sequence by taking elements from the sequence until the predicate is satisfied, and also takes the first element that satisfies the predicate. This version also passes the source index into the predicate.</summary>
    /// <remarks>Although this method is of the same nature as the built-in enumerable method <see cref="System.Linq.Enumerable.TakeWhile{TSource}(IEnumerable{TSource}, Func{TSource, int, bool})"/>, it differs on taking a single one of the trigger elements, if found.</remarks>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<TSource> TakeUntil<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      using var e = source.ThrowOnNull().GetEnumerator();

      var index = 0;

      while (e.MoveNext())
      {
        yield return e.Current;

        if (predicate(e.Current, index++))
          break;
      }
    }
  }
}
