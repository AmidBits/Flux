namespace Flux
{
  public static partial class ISet
  {
    /// <summary>Determines whether the source collection contains any of the elements in the specified target sequence.</summary>
    public static bool ContainsAny<T>(this System.Collections.Generic.ISet<T> source, System.Collections.Generic.IEnumerable<T> target)
    {
      if (!source.Any())
        return false; // If source is empty, it cannot contain any, so the result is false.

      if (target is System.Collections.Generic.ICollection<T> tc && !tc.Any()) // If target is empty, there is nothing to contain, so the result is false.
        return false;

      return target.Any(t => source.Contains(t));
    }
  }
}
