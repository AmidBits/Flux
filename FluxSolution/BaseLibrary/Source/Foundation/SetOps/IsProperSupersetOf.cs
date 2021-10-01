namespace Flux
{
  public static partial class SetOps
  {
    /// <summary>Determines whether the source set is a proper (strict) superset of a specified target set.</summary>
    public static bool IsProperSupersetOf<T>(System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target)
      => SetOps.ComputeCounts(source, target, true) is var (uniqueCount, unfoundCount) && unfoundCount == 0 && uniqueCount < source.Count;
  }
}
