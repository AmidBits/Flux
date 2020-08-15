namespace Flux
{
  public static partial class XtendCollections
  {
    public static int CountEqualAtEnd<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, System.Collections.Generic.IEqualityComparer<T>? comparer = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (target is null) throw new System.ArgumentNullException(nameof(target));

      var sourceIndex = source.Count;
      var targetIndex = target.Count;

      var minLength = sourceIndex < targetIndex ? sourceIndex : targetIndex;

      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      for (var atEnd = 0; --sourceIndex >= 0 && --targetIndex >= 0; atEnd++)
        if (!comparer.Equals(source[sourceIndex], target[targetIndex]))
          return atEnd;

      return minLength;
    }
  }
}
