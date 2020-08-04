namespace Flux
{
  public static partial class XtensionsCollections
  {
    /// <summary>Returns whether a sub-sequence of elements in the source equal the target elements.</summary>
    public static bool SliceEquals<T>(this System.Collections.Generic.IEnumerable<T> source, int sourceStartAt, System.Collections.Generic.IEnumerable<T> target, int targetStartAt, int length, System.Collections.Generic.IEqualityComparer<T>? comparer)
    {
      if (source is null || target is null || sourceStartAt < 0 || targetStartAt < 0 || length <= 0) return false;

      using var es = source!.GetEnumerator();
      var sourceIndex = 0;
      while (sourceIndex < sourceStartAt && es.MoveNext())
        sourceIndex++;
      if (sourceIndex != sourceStartAt)
        return false;

      using var et = target!.GetEnumerator();
      var targetIndex = 0;
      while (targetIndex < targetStartAt && et.MoveNext())
        targetIndex++;
      if (targetIndex != targetStartAt)
        return false;

      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      while (length-- > 0 && es.MoveNext() && et.MoveNext())
      {
        if (!comparer.Equals(es.Current, et.Current))
          return false;
      }

      return true;
    }
  }
}
