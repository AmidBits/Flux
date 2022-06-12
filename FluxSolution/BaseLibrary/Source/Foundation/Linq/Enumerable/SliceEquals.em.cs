namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Returns whether a sub-sequence of elements in the source equal the target elements. Uses the specified equality comparer.</summary>
    public static bool SliceEquals<T>(this System.Collections.Generic.IEnumerable<T> source, int sourceStartAt, System.Collections.Generic.IEnumerable<T> target, int targetStartAt, int length, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (target is null) throw new System.ArgumentNullException(nameof(target));
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      if (source is null || target is null || sourceStartAt < 0 || targetStartAt < 0 || length <= 0) return false;

      using var se = source!.GetEnumerator();
      var sourceIndex = 0;
      while (sourceIndex < sourceStartAt && se.MoveNext())
        sourceIndex++;
      if (sourceIndex != sourceStartAt)
        return false;

      using var te = target!.GetEnumerator();
      var targetIndex = 0;
      while (targetIndex < targetStartAt && te.MoveNext())
        targetIndex++;
      if (targetIndex != targetStartAt)
        return false;

      while (length-- > 0 && se.MoveNext() && te.MoveNext())
        if (!equalityComparer.Equals(se.Current, te.Current))
          return false;

      return true;
    }
    /// <summary>Returns whether a sub-sequence of elements in the source equal the target elements. Uses the default equality comparer.</summary>
    public static bool SliceEquals<T>(this System.Collections.Generic.IEnumerable<T> source, int sourceStartAt, System.Collections.Generic.IEnumerable<T> target, int targetStartAt, int length)
      => SliceEquals(source, sourceStartAt, target, targetStartAt, length, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
