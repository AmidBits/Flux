namespace Flux
{
  public static partial class SetOps
  {
    /// <summary>Determines whether the source collection overlaps with the specified target sequence.</summary>
    public static bool IsSetOverlapping<T>(System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target)
      => !(source.Count == 0 || (target is System.Collections.Generic.ICollection<T> ic && ic.Count == 0)) // If either set is empty, there can be no overlap.
      && SetContainsAny(source, target);
  }
}