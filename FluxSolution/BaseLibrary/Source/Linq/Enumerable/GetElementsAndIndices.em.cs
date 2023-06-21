namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Creates a seqeuence of indices for elements when the predicate is met.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<(T element, int index)> GetElementsAndIndices<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, int, bool>? predicate = null)
    {
      predicate ??= (e, i) => true;

      using var e = source.GetEnumerator();

      var index = 0;

      while (e.MoveNext())
      {
        if (predicate(e.Current, index))
          yield return (e.Current, index);

        index++;
      }
    }

    /// <summary>Creates a seqeuence of indices for elements when the predicate is met. 64-bit indices for very large sequences.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<(T element, long index)> GetElementsAndIndicesInt64<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, long, bool>? predicate = null)
    {
      predicate ??= (e, i) => true;

      using var e = source.GetEnumerator();

      var index = 0L;

      while (e.MoveNext())
      {
        if (predicate(e.Current, index))
          yield return (e.Current, index);

        index++;
      }
    }
  }
}
