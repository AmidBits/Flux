namespace Flux
{
  public static partial class SetOps
  {
    /// <summary>Determines whether the source set is a subset of the specified target set.</summary>
    public static bool IsSetSubsetOf<T>(System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target)
      => source.Count == 0 // An empty set is a subset of any set.
      || SetOps.SetCounts(source, target, false) is var (unfoundCount, uniqueCount) && unfoundCount >= 0 && uniqueCount == source.Count;
  }
}
