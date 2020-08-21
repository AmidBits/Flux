namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Returns the number of equal elements at the end of the sequences. Uses the specified equality comparer.</summary>
    public static int CountEqualAtEnd<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (target is null) throw new System.ArgumentNullException(nameof(target));

      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var sourceIndex = source.Count;
      var targetIndex = target.Count;

      var minLength = sourceIndex < targetIndex ? sourceIndex : targetIndex;

      for (var atEnd = 0; --sourceIndex >= 0 && --targetIndex >= 0; atEnd++)
        if (!comparer.Equals(source[sourceIndex], target[targetIndex]))
          return atEnd;

      return minLength;
    }
    /// <summary>Returns the number of equal elements at the end of the sequences. Uses the default equality comparer.</summary>
    public static int CountEqualAtEnd<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target)
      => CountEqualAtEnd(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
