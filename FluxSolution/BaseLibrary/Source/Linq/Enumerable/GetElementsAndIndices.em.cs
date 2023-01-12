namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Creates a seqeuence of indices for elements when the predicate is met.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<(T element, int index)> GetElementsAndIndices<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, int, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var index = 0;

      foreach (var item in source)
      {
        if (predicate(item, index))
          yield return (item, index);

        index++;
      }
    }

    /// <summary>Creates a seqeuence of indices for elements when the predicate is met. 64-bit indices for very large sequences.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<(T element, long index)> GetElementsAndIndicesInt64<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, long, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var index = 0L;

      foreach (var item in source)
      {
        if (predicate(item, index))
          yield return (item, index);

        index++;
      }
    }
  }
}
