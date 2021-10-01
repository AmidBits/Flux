namespace Flux
{
  public static partial class SetOps
  {
    /// <summary>Determines whether the source set is a subset of the specified target set.</summary>
    public static bool IsSubsetOf<T>(System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target)
      => SetOps.ComputeCounts(source, target, false) is var (uniqueCount, unfoundCount) && unfoundCount >= 0 && uniqueCount == source.Count;
  }
}
