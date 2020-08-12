namespace Flux
{
  public static partial class XtensionsCollections
  {
    /// <summary>Reports the length (or count) of equal.</summary>
    public static bool EndsWith<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, System.Collections.Generic.IEqualityComparer<T>? comparer = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (target is null) throw new System.ArgumentNullException(nameof(target));

      var sourceIndex = source.Count;
      var targetIndex = target.Count;

      if (sourceIndex < targetIndex) return false;

      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      while (--sourceIndex >= 0 && --targetIndex >= 0)
        if (!comparer.Equals(source[sourceIndex], target[targetIndex]))
          return false;

      return true;
    }
  }
}
