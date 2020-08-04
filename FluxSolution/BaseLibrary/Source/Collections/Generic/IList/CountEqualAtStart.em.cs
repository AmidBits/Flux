namespace Flux
{
  public static partial class XtensionsCollections
  {
    /// <summary>Determines whether the beginning of the first sequence is equivalent to the second sequence, using the specified element equality comparer.</summary>
    public static int CountEqualAtStart<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      var sourceCount = source.Count;
      var targetCount = target.Count;

      var minCount = sourceCount < targetCount ? sourceCount : targetCount;

      for (var atStart = 0; atStart < minCount; atStart++)
        if (!comparer.Equals(source[atStart], target[atStart]))
          return atStart;

      return minCount;
    }
    public static int CountEqualAtStart<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target)
      => CountEqualAtStart(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
