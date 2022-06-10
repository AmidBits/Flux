namespace Flux
{
  public static partial class ISet
  {
    /// <summary>Determines whether the source set is a proper (strict) subset of a specified target set.</summary>
    public static bool IsProperSubsetOf<T>(this System.Collections.Generic.ISet<T> source, System.Collections.Generic.IEnumerable<T> target)
      => !source.Any()
      ? target.Any() // An empty set is a proper subset of anything but the empty set.
      : (source.Counts(target, false) is var (unfoundCount, uniqueCount) && unfoundCount > 0 && uniqueCount == source.Count);
  }
}
