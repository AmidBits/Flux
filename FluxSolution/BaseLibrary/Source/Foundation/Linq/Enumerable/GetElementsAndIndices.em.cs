namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Creates a seqeuence of indices for elements when the predicate is met.</summary>
    public static System.Collections.Generic.IEnumerable<(T element, int index)> GetElementsAndIndices<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, int, bool> predicate)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var index = 0;

      using var e = source.GetEnumerator();

      while (e.MoveNext())
      {
        if (predicate(e.Current, index))
          yield return (e.Current, index);

        index++;
      }
    }

    /// <summary>Creates a seqeuence of indices for elements when the predicate is met. 64-bit indices for very large sequences.</summary>
    public static System.Collections.Generic.IEnumerable<(T element, long index)> GetElementsAndIndicesInt64<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, long, bool> predicate)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var index = 0L;

      using var e = source.GetEnumerator();

      while (e.MoveNext())
      {
        if (predicate(e.Current, index))
          yield return (e.Current, index);

        index++;
      }
    }
  }
}
