namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Returns the number of equal elements in the sequences at the start. Using the specified equality comparer.</summary>
    public static int CountEqualAtStart<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (target is null) throw new System.ArgumentNullException(nameof(target));

      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var sourceCount = source.Count;
      var targetCount = target.Count;

      var minCount = sourceCount < targetCount ? sourceCount : targetCount;

      for (var atStart = 0; atStart < minCount; atStart++)
        if (!comparer.Equals(source[atStart], target[atStart]))
          return atStart;

      return minCount;
    }
    /// <summary>Returns the number of equal elements in the sequences at the start. Using the specified equality comparer.</summary>
    public static int CountEqualAtStart<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target)
      => CountEqualAtStart(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
