namespace Flux
{
  public static partial class SetOps
  {
    /// <summary>Determines whether the source collection overlaps with the specified target sequence.</summary>
    public static bool IsOverlapping<T>(this System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target)
      => !(
        (source is System.Collections.Generic.ICollection<T> sc && sc.Count == 0) || 
        (target is System.Collections.Generic.ICollection<T> tc && tc.Count == 0)
      ) // If either set is empty, there can be no overlap.
      && ContainsAny(source, target);
  }
}
