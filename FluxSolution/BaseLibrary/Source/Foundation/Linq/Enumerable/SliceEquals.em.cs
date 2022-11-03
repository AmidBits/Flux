namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Returns whether a sub-sequence of elements in the source equal the target elements. Uses the specified equality comparer.</summary>
    public static bool SliceEquals<T>(this System.Collections.Generic.IEnumerable<T> source, int sourceStartAt, System.Collections.Generic.IEnumerable<T> target, int targetStartAt, int length, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (target is null) throw new System.ArgumentNullException(nameof(target));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      if (source is null || target is null || sourceStartAt < 0 || targetStartAt < 0 || length <= 0) return false;

      using var se = source!.GetEnumerator();
      while (sourceStartAt > 0 && se.MoveNext())
        sourceStartAt--;
      if (sourceStartAt > 0)
        return false;

      using var te = target!.GetEnumerator();
      while (targetStartAt > 0 && te.MoveNext())
        targetStartAt--;
      if (targetStartAt > 0)
        return false;

      while (length-- > 0 && se.MoveNext() && te.MoveNext())
        if (!equalityComparer.Equals(se.Current, te.Current))
          return false;

      return true;
    }
  }
}
