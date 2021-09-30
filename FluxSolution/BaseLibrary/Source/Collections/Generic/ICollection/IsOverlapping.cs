namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Determines whether the collection overlaps with the specified sequence.</summary>
    public static bool IsOverlapping<T>(this System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target)
      => ContainsAny(source, target);
  }
}
