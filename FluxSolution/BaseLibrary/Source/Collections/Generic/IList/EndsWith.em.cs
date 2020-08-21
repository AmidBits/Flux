namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Determines whether the source sequence ends with the target sequence. Uses the specified equality comparer.</summary>
    public static bool EndsWith<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (target is null) throw new System.ArgumentNullException(nameof(target));

      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var sourceIndex = source.Count;
      var targetIndex = target.Count;

      if (sourceIndex < targetIndex) return false;

      while (--sourceIndex >= 0 && --targetIndex >= 0)
        if (!comparer.Equals(source[sourceIndex], target[targetIndex]))
          return false;

      return true;
    }
    /// <summary>Determines whether the source sequence ends with the target sequence. Uses the specified equality comparer.</summary>
    public static bool EndsWith<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target)
      => EndsWith(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
