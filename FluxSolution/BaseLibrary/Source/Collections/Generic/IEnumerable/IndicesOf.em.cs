namespace Flux
{
  public static partial class XtensionsCollections
  {
    /// <summary>Creates a seqeuence of indices for elements when the predicate is met.</summary>
    public static System.Collections.Generic.IEnumerable<int> IndicesOf<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, bool> predicate)
    {
      var index = 0;
      foreach (var element in source ?? throw new System.ArgumentNullException(nameof(source)))
      {
        if (predicate(element)) yield return index;

        index++;
      }
    }
    /// <summary>Creates a seqeuence of indices for elements when the predicate is met.</summary>
    public static System.Collections.Generic.IEnumerable<int> IndicesOf<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, int, bool> predicate)
    {
      var index = 0;
      foreach (var element in source ?? throw new System.ArgumentNullException(nameof(source)))
      {
        if (predicate(element, index)) yield return index;

        index++;
      }
    }
  }
}
