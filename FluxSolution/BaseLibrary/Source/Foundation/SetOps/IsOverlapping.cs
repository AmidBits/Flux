namespace Flux
{
  public static partial class SetOps
  {
    /// <summary>Determines whether the source collection overlaps with the specified target sequence.</summary>
    public static bool IsOverlapping<T>(System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target)
      => ContainsAny(source, target);
  }
}
