namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Creates a seqeuence of indices for elements when the predicate is met.</summary>
    public static System.Collections.Generic.IEnumerable<int> GetIndicesInt32<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, int, bool> predicate)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var index = 0;

      using var e = source.GetEnumerator();

      while (e.MoveNext())
      {
        if (predicate(e.Current, index))
          yield return index;

        index++;
      }
    }
    /// <summary>Creates a seqeuence of indices for elements when the predicate is met.</summary>
    public static System.Collections.Generic.IEnumerable<int> GetIndicesInt32<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, bool> predicate)
      => GetIndicesInt32(source, (e, i) => predicate(e));
  }
}