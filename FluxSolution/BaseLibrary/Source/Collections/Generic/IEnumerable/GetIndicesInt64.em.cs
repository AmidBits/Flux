namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Creates a seqeuence of indices for elements when the predicate is met.</summary>
    public static System.Collections.Generic.IEnumerable<long> GetIndicesInt64<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, long, bool> predicate)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var index = 0L;

      foreach (var element in source)
      {
        if (predicate(element, index))
          yield return index;

        index++;
      }
    }
    /// <summary>Creates a seqeuence of indices for elements when the predicate is met.</summary>
    public static System.Collections.Generic.IEnumerable<long> GetIndicesInt64<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, bool> predicate)
      => GetIndicesInt64(source, (e, i) => predicate(e));
  }
}