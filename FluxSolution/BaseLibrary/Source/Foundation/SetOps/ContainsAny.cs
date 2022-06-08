namespace Flux
{
  public static partial class SetOps
  {
    /// <summary>Determines whether the source collection contains any of the elements in the specified target sequence.</summary>
    public static bool ContainsAny<T>(System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
    {
      if (source is System.Collections.Generic.ICollection<T> sc && sc.Count == 0)
        return false; // If source is empty, it cannot contain any, so the result is false.

      var ths = new System.Collections.Generic.HashSet<T>(target);

      if (ths.Count == 0) // If target is empty, there is nothing to contain, so the result is false.
        return false;

      return source.Any(s => ths.Contains(s));
    }
  }
}
