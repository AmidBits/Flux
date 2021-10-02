namespace Flux
{
  public static partial class SetOps
  {
    /// <summary>Determines whether the source set is a proper (strict) subset of a specified target set.</summary>
    public static bool IsProperSubsetOf<T>(System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target)
      => source.Count == 0 && target is System.Collections.Generic.ICollection<T> ic
      ? (ic.Count > 0) // An empty set is a proper subset of anything but the empty set.
      : SetOps.ComputeCounts(source, target, false) is var (uniqueCount, unfoundCount) && unfoundCount > 0 && uniqueCount == source.Count;
  }
}
